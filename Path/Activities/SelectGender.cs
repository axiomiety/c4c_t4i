
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
	[Activity(Label = "SelectGender")]
	public class SelectGender : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SelectGender);
			ImageButton btnClass = FindViewById<ImageButton>(Resource.Id.genderNext);
			btnClass.Click += delegate
			{
				StartActivity(typeof(SelectDOB));
			};
		}
	}
}
