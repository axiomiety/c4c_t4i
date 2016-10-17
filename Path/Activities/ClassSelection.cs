using System;

using Android.App;
using Android.OS;
using Android.Widget;
using PathViewModels;
using Autofac;

namespace Path
{
	[Activity(Label = "SelectClassroomActivity", Theme = "@style/MatLightNoActionBar")]
	public class ClassSelection : Activity
	{
        Spinner _spGrade, _spSection;
        ClassSelectionViewModel _model;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ClassSelection);
            _model = App.Container.Resolve<ClassSelectionViewModel>();

            _spGrade = FindViewById<Spinner>(Resource.Id.spGrade);
            _spGrade.ItemSelected += GradeSelected;
            _spGrade.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, _model.Grades);

            _spSection = FindViewById<Spinner>(Resource.Id.spSection);
            _spSection.ItemSelected += SectionSelected;

            ImageButton btnClass = FindViewById<ImageButton>(Resource.Id.btnClass);
            btnClass.Click += ClassSelected;
        }

        private void GradeSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _model.Grade = e.Parent.GetItemAtPosition(e.Position).ToString();
            _spSection.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, _model.Sections);
        }

        private void SectionSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _model.Section = e.Parent.GetItemAtPosition(e.Position).ToString();
        }

        private void ClassSelected(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_model.Errors))
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
            }
        }

    }
}
