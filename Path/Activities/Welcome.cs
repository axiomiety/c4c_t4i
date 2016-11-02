using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Path
{
	[Activity(Label = "Welcome", MainLauncher = false)]
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
			helloUser.Text = "Hello {0}!";

			AppPreferences ap = new AppPreferences(Application.Context);

			if (ap.GetKeyVal("user_name") != null)
			{
				string displayName = ap.GetKeyVal("user_name").Split()[0];
				helloUser.Text = String.Format(helloUser.Text, displayName);
			}
		}
	}
}
