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
using Autofac;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Path
{
	[Activity(Label = "Login", MainLauncher = true)]
	public class GoogleLogin : FragmentActivity, IValueEventListener, GoogleApiClient.IOnConnectionFailedListener,
		View.IOnClickListener, IOnCompleteListener, FirebaseAuth.IAuthStateListener
	{
		private const string Tag = "GoogleLogin";
		private const int RcSignIn = 9001;

		private FirebaseAuth mAuth;
		private DatabaseReference mDatabase;
		private string userId = "";
		private bool SIGNUP;

		private GoogleApiClient mGoogleApiClient;

		ITeacher teacher;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SignIn);

			ImageButton gSignIn = FindViewById<ImageButton>(Resource.Id.loginbutton);
			gSignIn.SetOnClickListener(this);

			// Setup our firebase options then init
			FirebaseOptions o = new FirebaseOptions.Builder()
				.SetApiKey(GetString(Resource.String.ApiKey))
				.SetApplicationId(GetString(Resource.String.ApplicationId))
				.SetDatabaseUrl(GetString(Resource.String.DatabaseUrl))
				.Build();
			FirebaseApp fa = FirebaseApp.InitializeApp(this, o, Application.PackageName);
			mDatabase = FirebaseDatabase.GetInstance(fa).GetReference("");

			// Configure Google Sign In
			GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
					.RequestIdToken(GetString(Resource.String.ServerClientId))
					.RequestId()
					.RequestEmail()
					.Build();

			// Build our api client
			mGoogleApiClient = new GoogleApiClient.Builder(this)
			   .EnableAutoManage(this, this)
			   .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
			   .Build();

			// Get the auth instance so we can add to it
			mAuth = FirebaseAuth.GetInstance(fa);
		}

		public void OnDataChange(DataSnapshot dataSnapshot)
		{
			Log.Debug(Tag, "Database Change: " + dataSnapshot.GetValue(true));
			if (dataSnapshot.HasChild(userId))
				SIGNUP = false;
			else
				SIGNUP = true;
			LoginUser(mAuth.CurrentUser);
		}

		public void OnCancelled(DatabaseError databaseError)
		{
			Log.Debug(Tag, "getUser:onCancelled", databaseError.ToException());
		}

		public void OnAuthStateChanged(FirebaseAuth auth)
		{
			var user = auth.CurrentUser;
			Log.Debug(Tag, "onAuthStateChanged:" + (user != null ? "signed_in:" + user.Uid : "signed_out"));

			if (user != null)
			{
				userId = user.Uid;
				mDatabase.Child("users").AddListenerForSingleValueEvent(this);
				//LoginUser(user);
			}
		}

		private IDictionary<string, object> ObjectToDictionary<T>(T item)
		where T : class
			{
				Type myObjectType = item.GetType();
				IDictionary<string, object> dict = new Dictionary<string, object>();
				var indexer = new object[0];
				PropertyInfo[] properties = myObjectType.GetProperties();
				foreach (var info in properties)
				{
					var value = info.GetValue(item, indexer);
					dict.Add(info.Name, value);
				}
				return dict;
			}

		public class JavaObject<T> : Java.Lang.Object
		{
			public JavaObject(T obj)
			{
				this.Value = obj;
			}

			public T Value { get; private set; }
		}

		private void LoginUser(FirebaseUser user)
		{
			ISchoolService _service = App.Container.Resolve<ISchoolService>();
			// TODO - id of teacher in constructor should not be hard coded
			teacher = new Teacher(_service, 1, user.DisplayName, user.Email, user.Uid);
			_service.Teacher = teacher;

			if (SIGNUP)
			{
				mDatabase.Child("users").Child(user.Uid).SetValue(user);
				//string path = "/teachers/" + user.Uid;
				//IDictionary teacherValues = (IDictionary)teacher;
				//IDictionary<string, IDictionary> dict = new Dictionary<string, IDictionary>();
				//dict.Add(path, teacherValues);
				//mDatabase.UpdateChildren(dict);
				var teacherProp = ObjectToDictionary(teacher);
				// TODO - nested dict
				foreach (var property in teacherProp)
				{
					if (property.Value == null)
						mDatabase.Child("teachers").Child(user.Uid).Child(property.Key).SetValue("");
					else
						mDatabase.Child("teachers").Child(user.Uid).Child(property.Key).SetValue(property.Value.ToString());
				}
				StartActivity(typeof(Welcome));
			}
			else
				StartActivity(typeof(Home));
		}

		protected override void OnStart()
		{
			base.OnStart();
			mAuth.AddAuthStateListener(this);
		}

		protected override void OnStop()
		{
			base.OnStop();
			mAuth.RemoveAuthStateListener(this);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// Result returned from launching the Intent from GoogleSignInApi.getSignInIntent(...);
			if (requestCode == RcSignIn)
			{
				GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
				if (result.IsSuccess)
				{
					// Google Sign In was successful, authenticate with Firebase
					var account = result.SignInAccount;
					FirebaseAuthWithGoogle(account);
				}
				else
				{
					UpdateUi();
				}
			}
		}

		private void FirebaseAuthWithGoogle(GoogleSignInAccount acct)
		{
			Log.Debug(Tag, "FirebaseAuthWithGoogle:" + acct.Id);
			AuthCredential credential = GoogleAuthProvider.GetCredential(acct.IdToken, null);
			mAuth.SignInWithCredential(credential).AddOnCompleteListener(this, this);
		}

		public void OnComplete(Task task)
		{
			Log.Debug(Tag, "SignInWithCredential:OnComplete:" + task.IsSuccessful);

			// If sign in fails, display a message to the user. If sign in succeeds
			// the auth state listener will be notified and logic to handle the
			// signed in user can be handled in the listener.
			if (!task.IsSuccessful)
			{
				Log.Debug(Tag, "SignInWithCredential", task.Exception);
				UpdateUi(msg:"Authentication Failed");
			}
		}

		public void OnConnectionFailed(ConnectionResult result)
		{
			Log.Debug(Tag, "OnConnectionFailed:" + result);
			UpdateUi(msg:"Google Play Services Error.");
		}

		public void OnClick(View v)
		{
			switch (v.Id)
			{
				case Resource.Id.loginbutton: SignIn(); break;
				default:
					Log.Debug(Tag, "OnClick:" + v.Id);
					break;
			}
		}

		private void SignIn()
		{
			Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
			StartActivityForResult(signInIntent, RcSignIn);
		}

		private void SignOut()
		{
			// Firebase sign out
			mAuth.SignOut();

			// Google sign out
			Auth.GoogleSignInApi.SignOut(mGoogleApiClient)
				.SetResultCallback(new ResultCallback<IResult>(delegate
				{
					Log.Debug(Tag, "Auth.GoogleSignInApi.SignOut");
					UpdateUi(msg:"Signed Out");
				}));
		}

		private void RevokeAccess()
		{
			// Firebase sign out
			mAuth.SignOut();

			// Google revoke access
			Auth.GoogleSignInApi.RevokeAccess(mGoogleApiClient)
				.SetResultCallback(new ResultCallback<IResult>(delegate
				{
					Log.Debug(Tag, "Auth.GoogleSignInApi.RevokeAccess");
				}));
		}

		private void UpdateUi(FirebaseUser user = null, string msg = null)
		{
			if (user == null && msg != null)
			{
				Toast.MakeText(this, msg, ToastLength.Long).Show();
			}
			else {
			}
		}
	}
}
