using System;
using System.Collections.Generic;
using System.Text;

namespace SingsteApp
{
    public class Appointment
    {
        private AppointmentType type;
        private DateTime trm_Datum;
        private DateTime trm_zeitanfang;
        private DateTime trm_zeitende;
        private DateTime trm_zeitecht;
        private DateTime trm_modificationtime;
        private string trm_link;
        private string trm_ort;
        private string trm_bezeichnung;
        private string trm_beschreibung;
        private string trm_detailsintern;
        private string trm_anzeige;
        private string trm_typ;
        private string trm_style;
        private int trm_abmeldungtage;
        private int trm_instatistik;
        private string trm_id;
        private bool trm_angemeldet;

        #region gettter
        public AppointmentType Type
        {
            get { return type; }
        }
        public bool Trm_angemeldet
        {
            get { return trm_angemeldet; }
        }
        public DateTime Trm_Datum
        {
            get { return trm_Datum; }
        }
        public DateTime Trm_zeitanfang
        {
            get { return trm_zeitanfang; }
        }
        public DateTime Trm_zeitende
        {
            get { return trm_zeitende; }
        }
        public DateTime Trm_zeitecht
        {
            get { return trm_zeitecht; }
        }
        public DateTime Trm_modificationtime
        {
            get { return trm_modificationtime; }
        }
        public string Trm_link
        {
            get { return trm_link; }
        }
        public string Trm_ort
        {
            get { return trm_ort; }
        }
        public string Trm_bezeichnung
        {
            get { return trm_bezeichnung; }
        }
        public string Trm_beschreibung
        {
            get { return trm_beschreibung; }
        }
        public string Trm_detailsintern
        {
            get { return trm_detailsintern; }
        }
        public string Trm_anzeige
        {
            get { return trm_anzeige; }
        }
        public string Trm_typ
        {
            get { return trm_typ; }
        }
        public string Trm_style
        {
            get { return trm_style; }
        }
        public int Trm_abmeldungtage
        {
            get { return trm_abmeldungtage; }
        }
        public int Trm_instatistik
        {
            get { return trm_instatistik; }
        }
        public string Trm_id
        {
            get { return trm_id; }
        }
        #endregion

        public Appointment(Dictionary<String, String> appointment)
        {
            string temp;
            type = AppointmentType.OverView;
            if (appointment.TryGetValue("trm_datum", out temp))
            { trm_Datum = DateTime.Parse(temp); }
            if (appointment.TryGetValue("trm_zeitanfang", out temp))
            { trm_zeitanfang = DateTime.Parse(temp); }
            if (appointment.TryGetValue("trm_zeitende", out temp))
            { trm_zeitende = DateTime.Parse(temp); type = AppointmentType.Details; }
            if (appointment.TryGetValue("trm_zeitecht", out temp))
            { trm_zeitecht = DateTime.Parse(temp); type = AppointmentType.Details; }
            if (appointment.TryGetValue("trm_modificationtime", out temp))
            { trm_modificationtime = DateTime.Parse(temp); type = AppointmentType.Details;}
            if (appointment.TryGetValue("trm_link", out temp))
            { trm_link = temp;}
            if (appointment.TryGetValue("trm_ort", out temp))
            { trm_ort = temp;}
            if (appointment.TryGetValue("trm_bezeichnung", out temp))
            { trm_bezeichnung = temp;}
            if (appointment.TryGetValue("trm_beschreibung", out temp))
            { trm_beschreibung = temp;}
            if (appointment.TryGetValue("trm_detailsintern", out temp))
            { trm_detailsintern = temp;}
            if (appointment.TryGetValue("trm_anzeige", out temp))
            { trm_anzeige = temp;}
            if (appointment.TryGetValue("trm_typ", out temp))
            { trm_typ = temp;}
            if (appointment.TryGetValue("trm_style", out temp))
            { trm_style = temp;}
            if (appointment.TryGetValue("trm_abmeldungtage", out temp))
            { trm_abmeldungtage = Int32.Parse(temp);}
            if (appointment.TryGetValue("trm_instatistik", out temp))
            { trm_instatistik = Int32.Parse(temp);}
            if (appointment.TryGetValue("trm_id", out temp))
            { trm_id = temp; }
            if (appointment.TryGetValue("abm_status", out temp))
            { trm_angemeldet = temp.Equals("angemeldet"); }
        }

        public enum AppointmentType
        {
            OverView, Details
        }
        public static List<Appointment> CreateAppointmentList(List<Dictionary<String, String>> appointment)
        {
            List<Appointment> result = new List<Appointment>();
            foreach (Dictionary<String, String> dic in appointment)
            {
                result.Add(new Appointment(dic));
            }
            return result;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Termin ID: \t\t" + trm_id + "\r\n");
            sb.Append("\t" + "Statistik:\t\t" + trm_instatistik + "\r\n");
            sb.Append("\t" + "Abm. Tage:\t" + trm_abmeldungtage + "\r\n");
            sb.Append("\t" + "FarbCode:\t" + trm_style + "\r\n");
            sb.Append("\t" + "Type:\t\t" + trm_typ + "\r\n");
            sb.Append("\t" + "Anzeige:\t\t" + trm_anzeige + "\r\n");
            sb.Append("\t" + "Deteils Int.:\t" + trm_detailsintern + "\r\n");
            sb.Append("\t" + "Beschreibung:\t" + trm_beschreibung + "\r\n");
            sb.Append("\t" + "Beszeichnung:\t" + trm_bezeichnung + "\r\n");
            sb.Append("\t" + "Ort:\t\t" + trm_ort + "\r\n");
            sb.Append("\t" + "Link:\t\t" + trm_link + "\r\n");
            sb.Append("\t" + "Zul. geändert:\t" + trm_modificationtime + "\r\n");
            sb.Append("\t" + "Beginn:\t\t" + trm_zeitanfang + "\r\n");
            sb.Append("\t" + "Öff. beginn:\t" + trm_zeitecht + "\r\n");
            sb.Append("\t" + "Ende:\t\t" + trm_zeitende + "\r\n");
            sb.Append("\r\n");
            return sb.ToString();
        }
    }
}
