using System;
using Android.Content;
using Android.Preferences;

namespace Path
{
	public class AppPreferences
	{
		/***
		 * Use to store key-vals to be shared across app
		 * 
		 * USAGE: 
		 * Context mContext = Application.Context;
		 * AppPreferences ap = new AppPreferences(mContext);
		 * ap.saveKeyVal("test", "1");
		 * ap.getKeyVal("test"); --> returns 1
		 *
		 ***/
		private ISharedPreferences mSharedPrefs;
		private ISharedPreferencesEditor mPrefsEditor;
		private Context mContext;

		public AppPreferences(Context context)
		{
			this.mContext = context;
			mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
			mPrefsEditor = mSharedPrefs.Edit();
		}

		public void SaveKeyVal(string key, string val)
		{
			mPrefsEditor.PutString(key, val);
			mPrefsEditor.Commit();
		}

		public string GetKeyVal(string key)
		{
			return mSharedPrefs.GetString(key, "");
		}
	}
}

