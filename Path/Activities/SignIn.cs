using Android.App;
using Android.Widget;
using Android.OS;

namespace Path
{
	[Activity(Label = "Path", MainLauncher = false, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.SignIn);

			// Get our button from the layout resource,
			// and attach an event to it
			ImageButton signIn = FindViewById<ImageButton>(Resource.Id.loginbutton);

			signIn.Click += delegate {
				StartActivity(typeof(Welcome));
			};
		}
	}
}

