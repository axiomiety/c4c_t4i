using Android.App;
using Android.Views;
using Android.Widget;
using DataModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Path
{
    class StudentViewAdapter : BaseAdapter
    {
        private Activity parentActivity;
        private int selectedPosition;
        private StudentView selectedView;
        private ObservableCollection<IStudent> students;

        public StudentViewAdapter(Activity parent, IList<IStudent> students)
        {
            this.parentActivity = parent;
            this.students = students as ObservableCollection<IStudent>;
            this.students.CollectionChanged += StudentsChanged;
        }

        private void StudentsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
        }

        #region Overridden Properties

        public override int Count
        {
            get { return students.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return students[position] as Java.Lang.Object;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        #endregion

        #region Overridden Methods

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var selectedStudent = students[position];
            if (selectedPosition == position)
            {
                var view = parentActivity.LayoutInflater.Inflate(Resource.Layout.StudentListRowLayoutSelected, parent, false);
                selectedView = new StudentView(parentActivity, view, selectedStudent);
                return view;
            }
            else
            {
                var view = this.parentActivity.LayoutInflater.Inflate(Resource.Layout.StudentListRowLayoutB, parent, false);
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
            selectedPosition = position;
            NotifyDataSetChanged();
        }

        #endregion

    }
}