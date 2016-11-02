
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Util;
using Android.Widget;

namespace Path
{
	public class DatePickerFragment : DialogFragment,
								  DatePickerDialog.IOnDateSetListener
	{
		// TAG can be any string of your choice.
		public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

		// Initialize this value to prevent NullReferenceExceptions.
		Action<DateTime> _dateSelectedHandler = delegate { };

		public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
		{
			DatePickerFragment frag = new DatePickerFragment();
			frag._dateSelectedHandler = onDateSelected;
			return frag;
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			DateTime currently = DateTime.Now;
			DatePickerDialog dialog = new DatePickerDialog(Activity,
														   this,
														   currently.Year,
														   currently.Month,
														   currently.Day);
			return dialog;
		}

		public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
		{
			// Note: monthOfYear is a value between 0 and 11, not 1 and 12!
			DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
			Log.Debug(TAG, selectedDate.ToLongDateString());
			_dateSelectedHandler(selectedDate);
		}
	}

	[Activity(Label = "SelectDOB")]
	public class SelectDOB : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SelectDOB);
			Button dateSelectBtn = FindViewById<Button>(Resource.Id.button_select_date);
			dateSelectBtn.Click += (sender, args) =>
			{
				DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
												{
													dateSelectBtn.Text = time.ToLongDateString();
												});
				frag.Show(FragmentManager, DatePickerFragment.TAG);
			};

			ImageButton btnClass = FindViewById<ImageButton>(Resource.Id.dobNext);
			btnClass.Click += delegate
			{
				StartActivity(typeof(SelectContact));
			};
		}
	}
}
