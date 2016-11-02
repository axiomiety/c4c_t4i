
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

using DataModels;
using PathDataModels;
using Autofac;

namespace Path
{
	[Activity(Label = "RegisterTeacherMetaActivity", Theme = "@style/MatLightNoActionBar")]
	public class RegisterTeacherMetaActivity : Activity
	{
		Fragment currFragment;
		RegisterTeacherGenderFrag genderFragment ;
		RegisterTeacherContactFrag contactFragment;
		RegisterTeacherDOBFrag dobFragment ;
		Stack<Fragment> fragStack = new Stack<Fragment>();
		ISchoolService _service = App.Container.Resolve<ISchoolService>();
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.register_teacher_meta_layout);

			genderFragment = new RegisterTeacherGenderFrag();
			contactFragment = new RegisterTeacherContactFrag();
			dobFragment = new RegisterTeacherDOBFrag();

			var trans = this.FragmentManager.BeginTransaction();
			trans.Add(Resource.Id.teacher_meta_frag_container, dobFragment, "dobFragment");
			trans.Hide(dobFragment);
			trans.Add(Resource.Id.teacher_meta_frag_container, contactFragment, "contactFragment");
			trans.Hide(contactFragment);
			trans.Add(Resource.Id.teacher_meta_frag_container, genderFragment, "GenderFragment");
			currFragment = genderFragment;

			trans.Commit();


			ImageButton next = FindViewById<ImageButton>(Resource.Id.metaNext);

			next.Click += (sender, e) => { toNextFragOrActivity(); };
		}

		public override void OnBackPressed()
		{
			if (this.FragmentManager.BackStackEntryCount > 0)
			{
				this.FragmentManager.PopBackStack();
				currFragment = fragStack.Pop();
			}
			else
			{
				base.OnBackPressed();
			}
		}

		private void SwitchToFrag(Fragment f)
		{
			FragmentTransaction trans = this.FragmentManager.BeginTransaction();
			trans.Hide(currFragment);
			trans.Show(f);
			trans.AddToBackStack(null);
			trans.Commit();
			fragStack.Push(currFragment);
			currFragment = f;
		}

		private void toNextFragOrActivity()
		{
			if (currFragment == genderFragment)
			{
				SwitchToFrag(contactFragment);
			}
			else if (currFragment == contactFragment)
			{
				SwitchToFrag(dobFragment);
			}
			else if (currFragment == dobFragment)
			{
				StartActivity(typeof(SchoolSelection));
			}
			
		}

	}
}
