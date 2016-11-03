using System;

using Android.App;
using Android.OS;
using Android.Widget;
using PathViewModels;
using Autofac;
using PathDataModels;
using DataModels;
using System.Linq;

namespace Path
{
    [Activity(Label = "ClassroomStudents", Theme = "@style/MatLightNoActionBar", MainLauncher = true)]
    public class ClassroomStudents : Activity
	{
        StudentsViewModel _model;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            _model = App.Container.Resolve<StudentsViewModel>();
            Teacher teacher = new Teacher(_model.Service, 1, "Test", "Test", "Test");
            _model.Service.Teacher = teacher;
            _model.Service.School = _model.Service.Schools[0];
            _model.Service.Class = (from IClass cls in _model.Service.School.Classes where cls.Grade == "4" && cls.Section == "A" select cls).ToList<IClass>()[0];

            SetContentView(Resource.Layout.ClassroomStudents);
            TextView txtStudentCount = this.FindViewById<TextView>(Resource.Id.txtStudentCount);
            Button btnView = this.FindViewById<Button>(Resource.Id.btnViewStudents);
            btnView.Click += ViewStudents;

            string className = string.Format("{0}{1}", _model.Class.Grade, _model.Class.Section);
            if(_model.Students.Count == 0)
            {
                txtStudentCount.Text = string.Format("You dont have any students in class {0}", className);
                btnView.Text = "Add Students";
            }
            else
            {
                txtStudentCount.Text = string.Format("You have {0} students in class {1}", _model.Students.Count, className);
                btnView.Text = "View Students";
            }
		}

        private void ViewStudents(object sender, EventArgs e)
        {
            StartActivity(typeof(ClassStudentView));
        }
    }
}
