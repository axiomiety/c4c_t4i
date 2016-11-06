
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
using DataModels;
using Autofac;

namespace Path
{
	[Activity(Label = "Home")]
	public class Home : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Home);

			TextView welcomeBack = FindViewById<TextView>(Resource.Id.welcomeBack);
			welcomeBack.Text = "Welcome Back!";

			ISchoolService _service = App.Container.Resolve<ISchoolService>();

			if (_service.Teacher.Name != null)
			{
				string displayName = _service.Teacher.Name.Split()[0];
				welcomeBack.Text = String.Format("Welcome Back {0}!", displayName);
			}
		}
	}
}