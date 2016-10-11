using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Path
{
	[Activity(Label = "Select School Location", Theme = "@style/MatLightNoActionBar")]
	public class SelectSchoolLocation : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SelectSchoolLocation);

			// Auto complete feature trying out
			var autoCompleteOptions = new String[] { "Hello", "Hey", "Heja", "Hi", "Hola", "Bonjour", "Gday", "Goodbye", "Sayonara", "Farewell", "Adios" };
			ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, autoCompleteOptions);
			var autocompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.schoolCountry);
			autocompleteTextView.Adapter = autoCompleteAdapter;

		}
	}
}
