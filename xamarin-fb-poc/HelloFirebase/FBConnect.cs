using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Firebase;
using Firebase.Database;
using Android.Widget;
using System;

namespace HelloFirebase
{
	public class ChildEventListener : IChildEventListener
	{
		private const string Tag = "FBCRUDTest";
		public IntPtr Handle
		{
			get
			{
				return (System.IntPtr)0;
			}
		}

		public void Dispose()
		{
			Log.Debug(Tag, "Database Dispose");
		}

		public void OnCancelled(DatabaseError error)
		{
			Log.Debug(Tag, "Database Error: " + error.Message);
		}

		public void OnChildAdded(DataSnapshot snapshot, string previousChildName)
		{
			Log.Debug(Tag, "OnChildAdded: " + snapshot.Value + " " + previousChildName);
		}

		public void OnChildChanged(DataSnapshot snapshot, string previousChildName)
		{
			Log.Debug(Tag, "OnChildChanged: " + snapshot.Value + " " + previousChildName);
		}

		public void OnChildMoved(DataSnapshot snapshot, string previousChildName)
		{
			Log.Debug(Tag, "OnChildMoved: " + snapshot.Value + " " + previousChildName);
		}

		public void OnChildRemoved(DataSnapshot snapshot)
		{
			Log.Debug(Tag, "OnChildRemoved: " + snapshot.Value);
		}
	}

	[Activity(Label = "CRUD Test", MainLauncher = false)]
	public class DatabaseTest : Activity, IValueEventListener
	{
		private const string Tag = "FBCRUDTest";
		private DatabaseReference mDatabase;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);
			// Button button = FindViewById<Button>(Resource.Id.myButton);

			// Setup our firebase options then init
			FirebaseOptions o = new FirebaseOptions.Builder()
			    .SetApiKey(GetString(Resource.String.ApiKey))
				.SetApplicationId(GetString(Resource.String.ApplicationId))
			    .SetDatabaseUrl(GetString(Resource.String.DatabaseUrl))
				.Build();
			FirebaseApp fa = FirebaseApp.InitializeApp(this, o, Application.PackageName);

			// Get a database reference
			var db = FirebaseDatabase.GetInstance(fa);
			mDatabase = db.GetReference("shwt-test");
			mDatabase.AddListenerForSingleValueEvent(this);

			//ChildEventListener c = new ChildEventListener();
			//mDatabase.AddChildEventListener(new Child() { });

			//mDatabase.SetValue("Hello, Shweta!");
			mDatabase.Child("user").Child("1").SetValue("Shweta");
		}

		private void SubmitPost()
		{
			// string userId = GetUid();
			mDatabase.AddListenerForSingleValueEvent(this);
		}

		public void OnDataChange(DataSnapshot dataSnapshot)
		{
			Log.Debug(Tag, "Database Change: " + dataSnapshot.Value);
		}

		public void OnCancelled(DatabaseError databaseError)
		{
			Log.Debug(Tag, "getUser:onCancelled", databaseError.ToException());
		}
	}
}
