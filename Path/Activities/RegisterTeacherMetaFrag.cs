
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace Path
{

	public class RegisterTeacherGenderFrag : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.register_teacher_gender_frag_layout, container, false);
			Spinner spinner = view.FindViewById<Spinner>(Resource.Id.teacher_gender_dropdown_spinner);
			int initialSpinnerPosition = spinner.SelectedItemPosition;
			return view;
		}

	}

	public class RegisterTeacherContactFrag : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.register_teacher_contact_frag_layout, container, false);
			//EditText phoneText = (EditText)view.FindViewById(Resource.Id.teacher_contact_number);
			return view;
		}
	}

	public class RegisterTeacherDOBFrag : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.register_teacher_dob_frag_layout, container, false);
			Button dateSelectBtn = (Button)view.FindViewById(Resource.Id.button_select_date);
			dateSelectBtn.Click += (sender, args) =>
			{
				DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
																{
																	dateSelectBtn.Text = time.ToLongDateString();
																});
				frag.Show(FragmentManager, DatePickerFragment.TAG);
			};
			return view;
		}
	}

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
}
