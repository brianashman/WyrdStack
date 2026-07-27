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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

#region
builder.Services.AddTransient<IMetricsProvider<RuntimeSystemMetrics>, RuntimeMetricsProvider>();
builder.Services.AddTransient<IMetricsService, MetricsService>();
builder.Services.AddHostedService<MetricsBackgroundWorker>();
#endregion
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
	options
	.SignIn.RequireConfirmedAccount = false;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireDigit = true;
	
	options.User.RequireUniqueEmail = true;
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
}
).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/users");
app.MapHub<MetricsHub>("/api/MetricsHub");
app.MapControllers();

app.Run();
