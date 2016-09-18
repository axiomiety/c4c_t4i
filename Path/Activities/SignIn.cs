using Android.App;
using Android.Widget;
using Android.OS;

namespace Path
{
	[Activity(Label = "Path", MainLauncher = true, Icon = "@mipmap/icon", 
	          Theme = "@android:style/Theme.Material.Light.NoActionBar.Fullscreen")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SignIn);

			ImageButton signIn = FindViewById<ImageButton>(Resource.Id.loginbutton);

			signIn.Click += delegate {
				StartActivity(typeof(Welcome));
			};
		}
	}
}

