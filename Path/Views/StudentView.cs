using Android.App;
using Android.Views;
using Android.Widget;
using DataModels;
using PathViewModels;
using System;

namespace Path
{
    public class StudentView
    {
        private EditText txtRollNo, txtName;
        private Spinner spGender;
        private Activity parentActivity;
        private IStudent student;
        private StudentViewModel model;
        private View view;

        public StudentView(Activity parentActivity, View view, IStudent student = null, ISchoolService service = null)
        {
            this.parentActivity = parentActivity;
            this.view = view;
            this.student = student;
            model = new StudentViewModel(student, service);
            model.PropertyChanged += ModelPropertyChanged;
            RegisterHandlers();
        }

        #region Methods

        private void ModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (view == null) return;
            switch (e.PropertyName)
            {
                case "RollNumberError":
                    txtRollNo.Error = model.RollNumberError;
                    break;
                case "NameError":
                    txtName.Error = model.NameError;
                    break;
            }
        }

        private void RegisterHandlers()
        {
            txtRollNo = view.FindViewById<EditText>(Resource.Id.rollno);
            txtName = view.FindViewById<EditText>(Resource.Id.studentname);
            spGender = view.FindViewById<Spinner>(Resource.Id.gender);
            var updateButtonView = view.FindViewById<ImageButton>(Resource.Id.updateButton);
            updateButtonView.Click += UpdateStudent;
            txtRollNo.TextChanged += RollNoChanged;
            txtName.TextChanged += NameChanged;
            spGender.Adapter = new ArrayAdapter<string>(parentActivity, Android.Resource.Layout.SimpleListItem1, new string[] { "Male", "Female" });
            spGender.ItemSelected += GenderChanged;
            if (student != null)
            {
                txtRollNo.Text = student.RollNumber;
                txtName.Text = student.Name;
                spGender.SetSelection(student.Gender == DataModels.Gender.Male ? 0 : 1);
                var removeButtonView = view.FindViewById<ImageButton>(Resource.Id.removeButton);
                removeButtonView.Click += RemoveStudent;
            }
        }

        private void GenderChanged(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            model.Gender = e.Position == 0 ? "Male" : "Female";
        }

        private void NameChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            model.Name = txtName.Text;
        }

        private void RollNoChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            model.RollNumber = txtRollNo.Text;
        }

        private void UpdateStudent(object sender, EventArgs e)
        {
            if (student == null)
            {
                model.AddStudent(txtRollNo.Text, txtName.Text, spGender.SelectedItem.ToString());
                txtRollNo.Text = "";
                txtName.Text = "";
                spGender.SetSelection(0);
            }
            else {
                model.UpdateStudent(student, txtRollNo.Text, txtName.Text, spGender.SelectedItem.ToString());
                Toast.MakeText(parentActivity, "Student updated", ToastLength.Short).Show();
            }
        }

        private void RemoveStudent(object sender, EventArgs e)
        {
            model.RemoveStudent(student);
            Toast.MakeText(parentActivity, "Student removed", ToastLength.Short).Show();
        }

        #endregion

    }
}