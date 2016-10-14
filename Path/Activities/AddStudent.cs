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

namespace Path.Activities
{
    [Activity(Label = "AddStudent")]
    public class AddStudent : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddStudent);

            var addStudentButton = FindViewById<EditText>(Resource.Id.add_student);
            addStudentButton.Click += HandleAddStuent;
        }

        void HandleAddStudent(object sender, EventArgs ea)
        {
            var rollno = FindViewById<EditText>(Resource.Id.rollno);
            var studentname = FindViewById<EditText>(Resource.Id.student_name);
            var gender = FindViewById<EditText>(Resource.Id.gender).getSelectedItem().toString();
            Toast.MakeText(this, rollno, ToastLength.Long).Show();
        }
    }
}