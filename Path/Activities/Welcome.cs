using Android.App;
using Android.OS;
using Android.Widget;

namespace Path
{
	[Activity(Label = "Welcome", Theme = "@style/MatLightNoActionBar")]
	public class Welcome : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Welcome);

			ImageButton getStartedButton = FindViewById<ImageButton>(Resource.Id.getStartedButton);

			getStartedButton.Click += delegate
			{
				StartActivity(typeof(ChooseLanguage));
			};

			TextView helloUser = FindViewById<TextView>(Resource.Id.helloUser);
			helloUser.Text = "Hello Shweta";
		}
	}
}
