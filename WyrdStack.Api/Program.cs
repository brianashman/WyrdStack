using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WyrdStack.Api.Data;
using WyrdStack.Api.Mappers.UserAuth;
using WyrdStack.Api.Services;
using Microsoft.AspNetCore.SignalR;
using WyrdStack.Api.Hubs;
using WyrdStack.Api.Features.Metrics.Providers;
using WyrdStack.Api.Features.Metrics.Core.Sources;
using WyrdStack.Api.Features.Metrics.Services;
using WyrdStack.Api.Features.Metrics.Background;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

#region Metrics
builder.Services.AddTransient<IMetricsProvider<RuntimeSystemMetrics>, RuntimeMetricsProvider>();
builder.Services.AddTransient<IMetricsService, MetricsService>();
builder.Services.AddHostedService<MetricsBackgroundWorker>();
#endregion

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity API Endpoints setup
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireDigit = true;
	options.User.RequireUniqueEmail = true;
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DataContext>();

// Explicit JWT Bearer Scheme configured to handle SignalR query tokens cleanly
builder.Services.AddAuthentication()
.AddJwtBearer("SignalRJwt", options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "YOUR_SUPER_SECRET_KEY_HERE_MIN_32_BYTES"))
	};

	options.Events = new JwtBearerEvents
	{
		OnMessageReceived = context =>
		{
			var accessToken = context.Request.Query["access_token"];
			var path = context.HttpContext.Request.Path;

			if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/api/metrics"))
			{
				context.Token = accessToken;
			}
			return Task.CompletedTask;
		}
	};
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("SignalRPolicy", policy =>
	{
		policy.AuthenticationSchemes.Clear(); // Clears any default schemes (like Identity)
		policy.AddAuthenticationSchemes("SignalRJwt"); // Forces it to ONLY use your JWT scheme
		policy.RequireAuthenticatedUser();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}
else
{
	// Only use HTTPS redirection in production to prevent local development loops
	app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/users");
app.MapHub<MetricsHub>("/api/metrics");
app.MapControllers();

app.Run();