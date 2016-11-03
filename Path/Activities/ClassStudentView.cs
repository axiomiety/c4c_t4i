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
using PathViewModels;
using Autofac;

namespace Path
{
    class StudentViewAdapter : BaseAdapter
    {
        private Activity _activity;
        private StudentsViewModel _model;
        private int _selectedPosition;
        private View _selectedView;

        public StudentViewAdapter(Activity parent, StudentsViewModel model)
        {
            this._activity = parent;
            _model = model;
        }

        #region Overridden Properties

        public override int Count
        {
            get { return _model.Students.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return _model.Students[position] as Java.Lang.Object;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        #endregion

        #region Overridden Methods

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var selectedStudent = _model.Students[position];
            if (_selectedPosition == position)
            {
                _selectedView = this._activity.LayoutInflater.Inflate(Resource.Layout.StudentListRowLayoutSelected, parent, false);
                var rollnoView = _selectedView.FindViewById<EditText>(Resource.Id.rollno);
                var nameView = _selectedView.FindViewById<EditText>(Resource.Id.studentname);
                var genderView = _selectedView.FindViewById<Spinner>(Resource.Id.gender);
                var updateButtonView = _selectedView.FindViewById<ImageButton>(Resource.Id.updateButton);
                var removeButtonView = _selectedView.FindViewById<ImageButton>(Resource.Id.removeButton);
                rollnoView.Text = selectedStudent.RollNumber;
                nameView.Text = selectedStudent.Name;
                genderView.Adapter = new ArrayAdapter<string>(_activity, Android.Resource.Layout.SimpleListItem1, new string[] { "Male", "Female" });
                genderView.SetSelection(selectedStudent.Gender == DataModels.Gender.Male ? 0 : 1);
                updateButtonView.Click += UpdateStudent;
                removeButtonView.Click += RemoveStudent;
                return _selectedView;
            }
            else
            {
                var view = this._activity.LayoutInflater.Inflate(Resource.Layout.StudentListRowLayoutB, parent, false);
                var rollnoView = view.FindViewById<TextView>(Resource.Id.rollno);
                var nameView = view.FindViewById<TextView>(Resource.Id.studentname);
                var genderView = view.FindViewById<TextView>(Resource.Id.gender);
                rollnoView.Text = selectedStudent.RollNumber;
                nameView.Text = selectedStudent.Name;
                genderView.Text = selectedStudent.Gender.ToString();
                return view;
            }
        }

        #endregion

        #region Methods

        public void SetSelectedPosition(int position)
        {
            _selectedPosition = position;
            NotifyDataSetChanged();
        }

        private void UpdateStudent(object sender, EventArgs e)
        {
            var selectedStudent = _model.Students[_selectedPosition];
            var rollnoView = _selectedView.FindViewById<EditText>(Resource.Id.rollno);
            var nameView = _selectedView.FindViewById<EditText>(Resource.Id.studentname);
            var genderView = _selectedView.FindViewById<Spinner>(Resource.Id.gender);
            _model.UpdateStudent(selectedStudent, rollnoView.Text, nameView.Text, genderView.SelectedItem.ToString());
            Toast.MakeText(_activity, "Student updated", ToastLength.Short).Show();
        }

        private void RemoveStudent(object sender, EventArgs e)
        {
            var selectedStudent = _model.Students[_selectedPosition];
            _model.RemoveStudent(selectedStudent);
            Toast.MakeText(_activity, "Student removed", ToastLength.Short).Show();
        }

        #endregion

    }
    

    [Activity(Label = "ClassStudentView", MainLauncher = false)]
    public class ClassStudentView : Activity
    {
        EditText txtRollNo, txtName;
        TextView txtCount;
        Spinner spGender;
        ListView lsView;
        StudentViewAdapter _adaptor;
        StudentsViewModel _model;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _model = App.Container.Resolve<StudentsViewModel>();
            _model.PropertyChanged += ModelPropertyChanged;
            SetContentView(Resource.Layout.ClassStudentView);

            txtRollNo = FindViewById<EditText>(Resource.Id.rollno);
            txtName = FindViewById<EditText>(Resource.Id.studentname);
            txtCount = FindViewById<TextView>(Resource.Id.numberOfStudents);
            spGender = FindViewById<Spinner>(Resource.Id.gender);
            spGender.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new string[] { "Male", "Female" });

            lsView = FindViewById<ListView>(Resource.Id.studentListView);
            lsView.ItemClick += StudentSelected;
            _adaptor = new StudentViewAdapter(this, _model);
            lsView.Adapter = _adaptor;

            ImageButton btnAdd = FindViewById<ImageButton>(Resource.Id.updateButton);
            btnAdd.Click += AddStudent;
            SetStudents();
        }

        private void StudentSelected(object sender, AdapterView.ItemClickEventArgs e)
        {
            _adaptor.SetSelectedPosition(e.Position);
        }

        private void ModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Students":
                    SetStudents();
                    break;
            }
        }

        private void AddStudent(object sender, EventArgs e)
        {
            _model.AddStudent(txtRollNo.Text, txtName.Text, spGender.SelectedItem.ToString());
            txtRollNo.Text = "";
            txtName.Text = "";
            spGender.SetSelection(0);
        }

        private void SetStudents()
        {
            _adaptor.NotifyDataSetChanged();
            txtCount.Text = _model.StudentNames.Count.ToString();
        }
    }
}