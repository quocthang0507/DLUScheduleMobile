﻿
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using DLUSchedule.Models;
using Xamarin.Forms;

namespace DLUSchedule.Droid
{
	[Activity(Label = "DLU Schedule Manager", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, LaunchMode = LaunchMode.SingleTop)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			LoadApplication(new App());
			CreateNotificationFromIntent(Intent);
		}

		protected override void OnNewIntent(Intent intent)
		{
			CreateNotificationFromIntent(intent);
		}

		void CreateNotificationFromIntent(Intent intent)
		{
			if (intent?.Extras != null)
			{
				string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
				string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
				DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
			}
		}
	}
}