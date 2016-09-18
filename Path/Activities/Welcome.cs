
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Path
{
	[Activity(Label = "Welcome")]
	public class Welcome : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Welcome);

			ImageButton getStartedButton = FindViewById<ImageButton>(Resource.Id.getStartedButton);

			// Create your application here
			getStartedButton.Click += delegate
			{
				StartActivity(typeof(ChooseLanguage));
			};

			TextView helloUser = FindViewById<TextView>(Resource.Id.helloUser);
			helloUser.Text = "Hello Shweta";
		}
	}
}
