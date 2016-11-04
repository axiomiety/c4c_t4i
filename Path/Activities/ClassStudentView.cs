
using Android.App;
using Android.OS;
using Android.Widget;
using Autofac;
using PathViewModels;

namespace Path
{
    [Activity(Label = "ClassStudentView", MainLauncher = false)]
    public class ClassStudentView : Activity
    {
        TextView txtCount;
        ListView lsView;
        StudentViewAdapter _adaptor;
        StudentsViewModel _model;
        StudentView addView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _model = App.Container.Resolve<StudentsViewModel>();
            _model.PropertyChanged += ModelPropertyChanged;
            SetContentView(Resource.Layout.ClassStudentView);

            addView = new StudentView(this, this.FindViewById(Android.Resource.Id.Content), null, _model.Service);
            txtCount = FindViewById<TextView>(Resource.Id.numberOfStudents);
            lsView = FindViewById<ListView>(Resource.Id.studentListView);
            lsView.ItemClick += StudentSelected;
            _adaptor = new StudentViewAdapter(this, _model.Students);
            lsView.Adapter = _adaptor;

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

        private void SetStudents()
        {
            txtCount.Text = _model.StudentNames.Count.ToString();
        }
    }
}