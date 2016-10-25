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
    [Activity(Label = "StudentsList", MainLauncher = false)]
    public class StudentsList : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StudentList);
            var v = FindViewById<ListView>(Resource.Id.studentListView);
            var ad = new CustomListAdapter(this);
            v.Adapter = ad;
            v.ItemClick += ad.OnListItemClick;
        }
    }

    class CustomListAdapter : BaseAdapter
    {
        private Activity mainActivity;
        private List<StudentItem> listData;

        private int selectedPosition { get; set; }
        private View selectedPositionView { get; set; }

        private void SetData()
        {
            listData = new List<StudentItem>();
            listData.Add(
                new StudentItem
                {
                    Name = "John Smith",
                    RollNo = "123",
                    Gender = "M"
                });
            listData.Add(
                new StudentItem
                {
                    Name = "Jane Doe",
                    RollNo = "7777",
                    Gender = "F"
                });
            listData.Add(
                new StudentItem
                {
                    Name = "Wonder Woman",
                    RollNo = "8877",
                    Gender = "F"
                });
            listData.Add(
                new StudentItem
                {
                    Name = "Mario Jr",
                    RollNo = "4898",
                    Gender = "M"
                });
        }

        public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedPosition = e.Position;
            NotifyDataSetChanged();
        }

        public void OnSaveChanges(object sender, EventArgs e)
        {
            var v = selectedPositionView;
            var edittextView = v.FindViewById<EditText>(Resource.Id.studentname);
            var updatedName = edittextView.Text;
            var item = this.listData[selectedPosition];
            item.Name = updatedName;
            NotifyDataSetChanged();
            selectedPosition = -1;
        }

        public CustomListAdapter(Activity mainActivity)
        {
            this.mainActivity = mainActivity;
            SetData();
            selectedPosition = -1; // otherwise it's initialised to 0, which is the first item
        }


        public override int Count
        {
            get { return listData.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            //return listData[position];
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // TODO: we should be able to re-use convertView instead of creating a new one from scratch
            if (selectedPosition == position) // different view for selected item
            {
                var v = this.mainActivity.LayoutInflater.Inflate(Resource.Layout.StudentListRowLayoutSelected, parent, false);
                var editstudentnameView = v.FindViewById<EditText>(Resource.Id.studentname);
                var item = this.listData[position];
                editstudentnameView.Hint = item.Name;
                selectedPositionView = v;
                var buttonView = v.FindViewById<ImageButton>(Resource.Id.updateButton);
                buttonView.Click += OnSaveChanges;
                return v;
            }
            else
            {
                var v = this.mainActivity.LayoutInflater.Inflate(Resource.Layout.StudentListRowLayoutB, parent, false);
                var rollnoView = v.FindViewById<TextView>(Resource.Id.rollno);
                var studentnameView = v.FindViewById<TextView>(Resource.Id.studentname);
                var genderView = v.FindViewById<TextView>(Resource.Id.gender);

                var item = this.listData[position];
                rollnoView.Text = item.RollNo;
                studentnameView.Text = item.Name;
                genderView.Text = item.Gender;

                return v;
            }            
        }        
    }

    public class StudentItem
    {
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
