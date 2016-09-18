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

			//ImageButton getStartedButton = FindViewById<ImageButton>(Resource.Id.getStartedButton);

		}
	}
}
