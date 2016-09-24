
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

namespace Path
{
	[Activity(Label = "SelectClassroomActivity", Theme = "@style/MatLightNoActionBar")]
	public class SelectClassroomActivity : Activity
	{
		Fragment currFrag;
		ClassGradeSectionFragment classGradeSelectionFrag;
		SubjectSelectionFragment subjectSelectionFrag;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.select_classroom);

			classGradeSelectionFrag = new ClassGradeSectionFragment();
			subjectSelectionFrag = new SubjectSelectionFragment();

			var trans = this.FragmentManager.BeginTransaction();
			trans.Add(Resource.Id.classroom_meta_frag_container, subjectSelectionFrag, "subjectSelectionFrag");
			trans.Hide(subjectSelectionFrag);
			trans.Add(Resource.Id.classroom_meta_frag_container, classGradeSelectionFrag, "classGradeSelectionFrag");
			trans.Show(classGradeSelectionFrag);
			currFrag = classGradeSelectionFrag;

			trans.Commit();

		}

		public override void OnBackPressed()
		{
			if (this.currFrag == subjectSelectionFrag)
			{
				this.FragmentManager.PopBackStack();
				currFrag = classGradeSelectionFrag;
			}
			else
			{
				base.OnBackPressed();
			}
		}

		public void SwitchToSubjectFrag()
		{
			FragmentTransaction trans = this.FragmentManager.BeginTransaction();
			trans.Hide(classGradeSelectionFrag);
			trans.Show(subjectSelectionFrag);
			trans.AddToBackStack(null);
			trans.Commit();
			currFrag = subjectSelectionFrag;
		}
	}
}
