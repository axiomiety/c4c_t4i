using System;
using Android.App;
using Android.OS;
using Android.Widget;
using PathViewModels;
using Autofac;
using Android.Views;
using Java.Lang;

namespace Path
{
	[Activity(Label = "Select School", Theme = "@style/MatLightNoActionBar")]
	public class SchoolSelection : Activity
    {
        SchoolSelectionViewModel _model;
        AutoCompleteTextView _avCountry, _avState, _avCity, _avSchool;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            _model = App.Container.Resolve<SchoolSelectionViewModel>();

            SetContentView(Resource.Layout.SchoolSelection);

			ArrayAdapter<string> autoCompleteAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _model.Countries);
            _avCountry = FindViewById<AutoCompleteTextView> (Resource.Id.schoolCountry);
            _avCountry.Adapter = autoCompleteAdapter;
            _avCountry.FocusChange += CountryChanged;

            _avState = FindViewById<AutoCompleteTextView>(Resource.Id.schoolState);
            _avState.FocusChange += StateChanged;

            _avCity = FindViewById<AutoCompleteTextView>(Resource.Id.schoolCity);
            _avCity.FocusChange += CityChanged;

            _avSchool = FindViewById<AutoCompleteTextView>(Resource.Id.school);
            _avSchool.FocusChange += SchoolChanged;

            ImageButton btnSchool = FindViewById<ImageButton>(Resource.Id.btnSchool);
            btnSchool.Click += SchoolSelected;
        }

        private void CountryChanged(object sender, View.FocusChangeEventArgs e)
        {
            if (_model.Country != _avCountry.Text)
            {
                _model.Country = _avCountry.Text;
                SetError(_avCountry, _model.CountryError);
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _model.States);
                _avState.Adapter = adapter;
                _avState.Text = "";
                _avCity.Text = "";
                _avSchool.Text = "";
                ClearError(_avState);
                ClearError(_avCity);
                ClearError(_avSchool);
            }
        }

        private void StateChanged(object sender, View.FocusChangeEventArgs e)
        {
            if (_model.State != _avState.Text)
            {
                _model.State = _avState.Text;
                SetError(_avState, _model.StateError);
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _model.Cities);
                _avCity.Adapter = adapter;
                _avCity.Text = "";
                _avSchool.Text = "";
                ClearError(_avCity);
                ClearError(_avSchool);
            }
        }

        private void CityChanged(object sender, View.FocusChangeEventArgs e)
        {
            if (_model.City != _avCity.Text)
            {
                _model.City = _avCity.Text;
                SetError(_avCity, _model.CityError);
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _model.SchoolNames);
                _avSchool.Adapter = adapter;
                _avSchool.Text = "";
                ClearError(_avSchool);
            }
        }

        private void SchoolChanged(object sender, View.FocusChangeEventArgs e)
        {
            _model.SchoolName = _avSchool.Text;
            SetError(_avSchool, _model.SchoolError);
        }

        private void SchoolSelected(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(_model.Errors))
            {
                AlertDialog.Builder errorDialog = new AlertDialog.Builder(this);
                errorDialog.SetTitle("Errors");
                errorDialog.SetMessage(_model.Errors);
                errorDialog.SetPositiveButton("Ok", (alert, args) => ((Dialog)alert).Cancel());
                Dialog dialog = errorDialog.Create();
                dialog.Show();
            }
            else
            {
                _model.Save();
                StartActivity(typeof(ClassSelection));
            }
        }

        private void SetError(EditText view, string error)
        {
            view.SetError(error, GetDrawable(Resource.Drawable.abc_ic_star_black_36dp));
        }

        private void ClearError(EditText view)
        {
            SetError(view, "");
        }
    }
}
