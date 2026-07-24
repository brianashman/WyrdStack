namespace WyrdStack.Maui.Views.Auth;

public partial class AuthCardComponent : ContentView
{
	public AuthCardComponent()
	{
		InitializeComponent();
		//PointerGestureRecognizer pointerGestureRecognizer = new PointerGestureRecognizer();
		//ActionButton.GestureRecognizers.Add(pointerGestureRecognizer);
		//pointerGestureRecognizer.PointerEntered += (s, e) =>
		//{
		//	ActionButton.BackgroundColor = Color.FromArgb("#1E90FF");
		//};
		//pointerGestureRecognizer.PointerExited += (s, e) =>
		//{
		//	ActionButton.BackgroundColor = Color.FromArgb("#007BFF");

		//};
	}
	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		if(width > 0 && height > 0)
		{
			AuthBorder.WidthRequest = width * 0.8;
			AuthBorder.HeightRequest = height * 0.6;
		}
	}
}