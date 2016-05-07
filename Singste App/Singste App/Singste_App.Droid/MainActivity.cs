using System;
using Singste_App;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Singste_App.Droid
{
    [Activity(Label = "Singste App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            apiConnector.DatabaseController = new DriveManagementAndroid();
            base.OnCreate(bundle);
            if (!apiConnector.UserExists)
            {

                // Set our view from the "main" layout resourcelay
                SetContentView(Resource.Layout.Main);
                
                FindViewById<Button>(Resource.Id.button1).Click += QRToggel;
            }
            else
            {
                Intent newMain = new Intent(this, typeof(MainMenu));
                StartActivity(newMain);
                Finish();
            }
        }

        private async void QRToggel(object sender, EventArgs e)
        {
            ZXing.Mobile.MobileBarcodeScanner tkd = new ZXing.Mobile.MobileBarcodeScanner();
            ZXing.Result rs = await tkd.Scan();
            if(rs != null)
            {
                apiConnector ar = apiConnector.createReader(rs.Text);
                if (ar.CheckLogIN())
                {
                    Intent newMain = new Intent(this, typeof(MainMenu));
                    StartActivity(newMain);
                    Finish();
                }
            }
        }
    }
}


