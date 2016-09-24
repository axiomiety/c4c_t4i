

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Path
{
	public class ClassGradeSectionFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.class_grade_section_frag_layout, container, false);
			ImageButton next = view.FindViewById<ImageButton>(Resource.Id.grade_section_next);
			next.Click += (sender, e) => { ((SelectClassroomActivity)Activity).SwitchToSubjectFrag(); };
			return view;
		}
	}

	public class SubjectSelectionFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.subject_frag_layout, container, false);
			ImageButton finish = view.FindViewById<ImageButton>(Resource.Id.subject_tick);
			finish.Click += (sender, e) =>
			{ //delegate to the next activity
			};
			return view;
		}
	}
}
