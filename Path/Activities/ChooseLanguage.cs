
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
	[Activity(Label = "Choose Language")]
	public class ChooseLanguage : ListActivity
	{
		string[] items;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.ChooseLanguage);

			//var phoneNumbers = Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0];
			//this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);

			items = new string[] { "English", "Hindi", "Tamil" };
			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);

		}
	}
}
