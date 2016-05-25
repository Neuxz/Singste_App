using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Singste_App.Droid
{
    class Termin_Adapter : BaseAdapter<Appointment>
    {
        private List<Appointment> termine;
        private Context context;
        //private MainMenu mainMenu;
        public Termin_Adapter(Context context, List<Appointment> termine)
        {
            this.context = context;
            this.termine = termine;
        }


        public override Appointment this[int position]
        {
            get
            {
                return termine[position];
            }
        }

        public override int Count
        {
            get
            {
                return termine.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.Termin_Uebersicht, null, false);
            }
            row.Tag = position;
            row.FindViewById<CheckBox>(Resource.Id.anmStatus).Tag = position;
            //row.FindViewById<TextView>(SingsteApp.Resource.Id.terminTitle).Click += this.;
            row.FindViewById<TextView>(Resource.Id.terminTitle).Text = termine[position].Trm_bezeichnung;
            row.FindViewById<TextView>(Resource.Id.terminDatum).Text = termine[position].Trm_Datum.ToLongDateString();
            row.FindViewById<TextView>(Resource.Id.terminZeit).Text = termine[position].Trm_zeitanfang.TimeOfDay.Hours.ToString("00") + ":" + termine[position].Trm_zeitanfang.TimeOfDay.Minutes.ToString("00");
            row.FindViewById<CheckBox>(Resource.Id.anmStatus).Checked = termine[position].Trm_angemeldet;
            //row.FindViewById<CheckBox>(Resource.Id.anmStatus).SetBackgroundColor(Android.Graphics.Color.) Set color
            row.FindViewById<CheckBox>(Resource.Id.anmStatus).Click += Anmelde_Status_Click;
            row.Click += Row_Click;
            return row;
        }

        private void Anmelde_Status_Click(object sender, EventArgs e)
        {
            int position = (int)((CheckBox)sender).Tag;
            apiConnector ar = apiConnector.createReader();
            bool outer = false;
            Toast.MakeText(context, ar.Anmelden(((CheckBox)sender).Checked, termine[position].Trm_id, out outer), ToastLength.Short).Show();
            ((CheckBox)sender).Checked = outer;
        }

        private void Row_Click(object sender, EventArgs e)
        {
            int position = (int)((LinearLayout)sender).Tag;
            apiConnector ar = apiConnector.createReader();
        }

    }
}