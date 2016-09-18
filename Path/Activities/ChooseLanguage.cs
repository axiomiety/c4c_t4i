using System;

using Android.App;
using Android.OS;
using Android.Widget;

namespace Path
{
	[Activity(Label = "Choose Language", Theme = "@style/MatLightNoActionBar")]
	public class ChooseLanguage : ListActivity
	{
		string[] items;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.ChooseLanguage);

			items = new string[] { "English", "Hindi", "Tamil" };
			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);

			ImageButton next = FindViewById<ImageButton>(Resource.Id.langNext);

			next.Click += delegate
			{
				StartActivity(typeof(SelectSchoolLocation));
			};
		}
	}
}
