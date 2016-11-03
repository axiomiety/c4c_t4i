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
            _model.PropertyChanged += _model_PropertyChanged;

            SetContentView(Resource.Layout.SchoolSelection);

			ArrayAdapter<string> autoCompleteAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _model.Countries);
            _avCountry = FindViewById<AutoCompleteTextView> (Resource.Id.schoolCountry);
            _avCountry.Adapter = autoCompleteAdapter;
            _avCountry.TextChanged += CountryChanged;

            _avState = FindViewById<AutoCompleteTextView>(Resource.Id.schoolState);
            _avState.TextChanged += StateChanged;

            _avCity = FindViewById<AutoCompleteTextView>(Resource.Id.schoolCity);
            _avCity.TextChanged += CityChanged;

            _avSchool = FindViewById<AutoCompleteTextView>(Resource.Id.school);
            _avSchool.TextChanged += SchoolChanged;

            ImageButton btnSchool = FindViewById<ImageButton>(Resource.Id.btnSchool);
            btnSchool.Click += SchoolSelected;
        }

        #region Property Changed

        private void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
            switch(e.PropertyName)
            {
                case "Country":
                    Model_CountryChanged();
                    break;
                case "States":
                    Model_StatesChanged();
                    break;
                case "State":
                    Model_StateChanged();
                    break;
                case "Cities":
                    Model_CitiesChanged();
                    break;
                case "City":
                    Model_CityChanged();
                    break;
                case "Schools":
                    Model_SchoolsChanged();
                    break;
                case "SchoolName":
                    Model_SchoolChanged();
                    break;
            }
        }

        private void Model_CountryChanged()
        {
            if (_avCountry.Text != _model.Country)
            {
                _avCountry.Text = _model.Country;
            }
            SetError(_avCountry, _model.CountryError);
        }

        private void Model_StatesChanged()
        {
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _model.States);
            _avState.Adapter = adapter;
        }

        private void Model_StateChanged()
        {
            if (_avState.Text != _model.State)
            {
                _avState.Text = _model.State;
            }
            SetError(_avState, _model.StateError);
        }

        private void Model_CitiesChanged()
        {
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _model.Cities);
            _avCity.Adapter = adapter;
        }

        private void Model_CityChanged()
        {
            if (_avCity.Text != _model.City)
            {
                _avCity.Text = _model.City;
            }
            SetError(_avCity, _model.CityError);
        }

        private void Model_SchoolsChanged()
        {
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _model.SchoolNames);
            _avSchool.Adapter = adapter;
        }

        private void Model_SchoolChanged()
        {
            if (_avSchool.Text != _model.SchoolName)
            {
                _avSchool.Text = _model.SchoolName;
            }
            SetError(_avSchool, _model.SchoolError);
        }

        #endregion

        #region Control Change Event Handlers

        private void CountryChanged(object sender, EventArgs e)
        {
            _model.Country = _avCountry.Text;
        }

        private void StateChanged(object sender, EventArgs e)
        {
            _model.State = _avState.Text;
        }

        private void CityChanged(object sender, EventArgs e)
        {
            _model.City = _avCity.Text;
        }

        private void SchoolChanged(object sender, EventArgs e)
        {
            _model.SchoolName = _avSchool.Text;
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

        #endregion

        #region Helper Methods

        private void SetError(EditText view, string error)
        {
            view.Error = string.IsNullOrEmpty(error) ? null : error;
        }

        #endregion
    }
}
