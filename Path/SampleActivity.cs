using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathViewModels;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;

namespace Path
{
    [Activity(Label = "Path", MainLauncher = true, Icon = "@mipmap/icon")]
    public class SampleActivity : Activity
    {
        TeacherViewModel _model;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _model = App.Container.Resolve<TeacherViewModel>();
            SetContentView(Resource.Layout.SampleLayout);

            Spinner lvCountry = FindViewById<Spinner>(Resource.Id.lvCountry);
            lvCountry.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, _model.Countries);
            lvCountry.ItemSelected += LvCountry_ItemSelected;

        }

 
        private void LvCountry_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _model.Country = ((TextView)e.View).Text;

            Spinner lvStates = FindViewById<Spinner>(Resource.Id.lvStates);
            lvStates.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, _model.States);
        }
    }
}