using System;
using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using DataModels;
using PathViewModels;
using Autofac;

namespace Path
{
	[Activity(Label = "Login", MainLauncher = false)]
	public class DatabaseTest : Activity, IValueEventListener
	{
		private const string Tag = "Firebase";
		private DatabaseReference mDatabase;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.FBTest);

			FirebaseOptions o = new FirebaseOptions.Builder()
				.SetApiKey(GetString(Resource.String.ApiKey))
				.SetApplicationId(GetString(Resource.String.ApplicationId))
				.SetDatabaseUrl(GetString(Resource.String.DatabaseUrl))
				.Build();
			FirebaseApp fa = FirebaseApp.InitializeApp(this, o, Application.PackageName);
			mDatabase = FirebaseDatabase.GetInstance(fa).GetReference("");

			Button button = FindViewById<Button>(Resource.Id.fbPush);
			button.Click += Pushed;
		}

		public void OnDataChange(DataSnapshot dataSnapshot)
		{
			Log.Debug(Tag, "Database Change: " + dataSnapshot.GetValue(false));
		}

		public void OnCancelled(DatabaseError databaseError)
		{
			Log.Debug(Tag, "getUser:onCancelled", databaseError.ToException());
		}

		private void Pushed(object sender, EventArgs e)
		{
			TextView phoneNum = this.FindViewById<TextView>(Resource.Id.phone_number);
			Log.Info(Tag, "Phone Number: " + phoneNum.Text);
			mDatabase.Child("shwt-test").AddListenerForSingleValueEvent(this);
			String key = mDatabase.Child("shwt-test").Child("phone-num").Push().Key;
			mDatabase.Child("shwt-test").Child("phone-num").SetValue(phoneNum.Text);
		}
	}
}
