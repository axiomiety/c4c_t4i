using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Path
{
	[Activity(Label = "Choose Language", Theme = "@style/MatLightNoActionBar")]
	public class ChooseLanguage : ListActivity
	{
		string[] langs;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			langs = Resources.GetStringArray(Resource.Array.app_locale);

			SetContentView(Resource.Layout.ChooseLanguage);
			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, langs);

			ImageButton next = FindViewById<ImageButton>(Resource.Id.langNext);

			next.Click += delegate
			{
				StartActivity(typeof(SchoolSelection));
			};
		}

		protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
 			var val = langs[e.Position];
			Console.WriteLine("App Locale Selected " + val);
			// TODO: write to share pref
		}
	}
}
