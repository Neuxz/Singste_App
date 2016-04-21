﻿using System;

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
            base.OnCreate(bundle);
            if (!apiConnector.UserExists)
            {

                // Set our view from the "main" layout resourcelay
                SetContentView(Resource.Layout.Main);
                FindViewById<Button>(Resource.Id.MyButton).Click += delegate
                {
                    if (FindViewById<EditText>(Resource.Id.editText1).Text != String.Empty)
                    {
                        apiConnector ar = apiConnector.createReader(FindViewById<EditText>(Resource.Id.editText1).Text + ";cancarmina.de", this);
                        if (ar.CheckLogIN())
                        {
                            Intent newMain = new Intent(this, typeof(MainMenu));
                            StartActivity(newMain);
                        }
                    }
                    else
                    {
                        new AlertDialog.Builder(this).SetNeutralButton("Ok", delegate { }).SetMessage("Bitte geben sie ihren Benutzercode ein.").SetTitle("Kein Benutzer code.").Show();
                    }
                };
                FindViewById<Button>(Resource.Id.button1).Click += delegate
                {
                    new AlertDialog.Builder(this).SetNeutralButton("Ok", delegate { }).SetMessage("Not Implemented yet!").SetTitle("Debug Error").Show();
                };
            }
            else
            {
                Intent newMain = new Intent(this, typeof(MainMenu));
                StartActivity(newMain);
                Finish();
            }
        }


    }
}

