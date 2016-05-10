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

namespace Singste_App.Droid
{
    [Activity(Label = "Termin Übersicht")]
    class ChoirChose : Activity
    {
        ListView lv;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChoirChoose);
            apiConnector ar = apiConnector.createReader();
            lv = FindViewById<ListView>(Resource.Id.Choirs);
            //Multiple user check
        }
    }
}