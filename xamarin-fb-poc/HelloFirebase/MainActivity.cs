using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Firebase.Xamarin;
using Firebase.Xamarin.Database;

		
namespace HelloFirebase
{
	[Activity(Label = "HelloFirebase", Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			// base.OnCreate(savedInstanceState);
			// SetContentView(Resource.Layout.Main);

			//FirebaseOptions o = new FirebaseOptions.Builder()
			//	.SetApiKey(GetString(Resource.String.ApiKey))
			//	.SetApplicationId(GetString(Resource.String.ApplicationId))
			//	.SetDatabaseUrl(GetString(Resource.String.DatabaseUrl))
			//	.Build();
			
			//var firebase = new FirebaseClient("https://hellofb-b005f.firebaseio.com/");
			//var item = firebase
			//		  .Child("work")
			//		  //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
			//		  .PostAsync(new YourObject());
								
			/*
			 * var config = {
				    apiKey: "AIzaSyBlP8nGeKUpoKCVL7-IPjSdQe1hqnZ-3b0",
				    authDomain: "hellofb-b005f.firebaseapp.com",
				    databaseURL: "https://hellofb-b005f.firebaseio.com",
				    storageBucket: "hellofb-b005f.appspot.com",
				    messagingSenderId: "79941882991"
				  };
			 */
			System.Console.WriteLine("HELLOOO");
			//var firebase = new Firebase.Xamarin.Database.FirebaseClient("https://hellofb-b005f.firebaseio.com/");
			//var items = firebase.Child("work").AsObservable<Work>().Subscribe(d => System.Console.Write(d.Key));
			////.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
			//.LimitToFirst(2)
			//.OnceAsync<YourObject>();
			//items.O
			//foreach (var item in items)
			//{
			//	System.Console.WriteLine($"{item.Key} name is {item.Object.Name}");
			//}

			//foreach (var item in items)
			//{
			//	System.Console.WriteLine($"{item.Key} name is {item.Object.Name}");
			//}

			//base.OnCreate(savedInstanceState);

			//// Set our view from the "main" layout resource
			//SetContentView(Resource.Layout.Main);

			//// Get our button from the layout resource,
			//// and attach an event to it
			//Button button = FindViewById<Button>(Resource.Id.myButton);

			//button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
		}
	}
}

