using System;
using Android.App;
using Android.OS;
using Android.Widget;
using DataModels;
using Autofac;

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
			helloUser.Text = "Hello!";

			ISchoolService _service = App.Container.Resolve<ISchoolService>();

			if (_service.Teacher.Name != null)
			{
				string displayName = _service.Teacher.Name.Split()[0];
				helloUser.Text = String.Format("Hello {0}!", displayName);
			}
		}
	}
}
