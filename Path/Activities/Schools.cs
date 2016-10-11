using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System;
using Android.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using System.Linq;
using DataModels;
using PathViewModels;
using Autofac;

namespace Path
{
    public class SchoolViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public SchoolViewHolder(View v) : base(v)
        {
            NameView = v.FindViewById<TextView>(Resource.Id.txtName);
            CityView = v.FindViewById<TextView>(Resource.Id.txtCity);
            CountryView = v.FindViewById<TextView>(Resource.Id.txtCountry);
            var cardView = v.FindViewById<CardView>(Resource.Id.cardView);
            cardView.SetOnClickListener(this);
        }

        public TextView NameView { get; private set; }
        public TextView CityView { get; private set; }
        public TextView CountryView { get; private set; }

        public void OnClick(View v)
        {
            Toast.MakeText(v.Context, NameView.Text, ToastLength.Short).Show();
        }
    }

    public class SchoolsViewAdapter : RecyclerView.Adapter, Android.Support.V7.Widget.SearchView.IOnQueryTextListener
    {
        ObservableCollection<ISchool> col;
        ObservableCollection<ISchool> current;

        public SchoolsViewAdapter(IEnumerable<ISchool> data)
        {
            col = new ObservableCollection<ISchool>(data);
            current = col;
        }

        public override int ItemCount
        {
            get
            {
                return current.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ISchool school = current[position];
            SchoolViewHolder viewHolder = (SchoolViewHolder)holder;
            viewHolder.NameView.SetText(school.Name, TextView.BufferType.Normal);
            viewHolder.CityView.SetText(school.City, TextView.BufferType.Normal);
            viewHolder.CountryView.SetText(school.Country, TextView.BufferType.Normal);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SchoolsRecycler, parent, false);
            return new SchoolViewHolder(v);
        }

        public bool OnQueryTextChange(string newText)
        {
            var filter = from a in col
                         where a.Name.StartsWith(newText, true, System.Globalization.CultureInfo.CurrentCulture)
                         select a;
            current = new ObservableCollection<ISchool>(filter.ToList());
            this.NotifyDataSetChanged();
            return true;
        }

        public bool OnQueryTextSubmit(string query)
        {
            return true;
        }
    }

    [Activity(Label = "Schools", MainLauncher = true, Theme = "@style/Theme.AppCompat")]
    public class SchoolsActivity : AppCompatActivity
    {
        RecyclerView view;
        SchoolsViewAdapter _adapter;
        TeacherViewModel _model;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _model = App.Container.Resolve<TeacherViewModel>();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Schools);

            view = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            view.SetLayoutManager(new LinearLayoutManager(this));

            _adapter = new SchoolsViewAdapter(_model.AllSchools);
            view.SetAdapter(_adapter);

            FloatingActionButton addButton = FindViewById<FloatingActionButton>(Resource.Id.addSchoolButton);
            addButton.Click += AddSchool;
        }

        private void AddSchool(object sender, EventArgs e)
        {
            Toast.MakeText(this.view.Context, "Add school", ToastLength.Short).Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SchoolsSearch, menu);
            var item = menu.FindItem(Resource.Id.action_search);
            var searchView = (Android.Support.V7.Widget.SearchView)MenuItemCompat.GetActionView(item);
            searchView.SetOnQueryTextListener(_adapter);
            return true;
        }

    }
}

