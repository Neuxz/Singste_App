using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Singste_App
{
    public class apiConnector
    {
        public enum debug{
            Debug, NONdebug};
        private const string api = "/?m=api&c=";
        private const string apiTrmIDIS = "&t=";
        private const string apiAnAbML = "&a=";
        private const string apiAnML = "n";
        private const string apiAbML = "b";
        private static DriveManagement dm;
        private static bool userExists;
        private User curent;

        public static bool UserExists
        {
            get
            {
                return (dm.getDatabase().phrase != null) ;
            }
        }

        //Create help[]
        private apiConnector()
        {
#if __ANDROID__
            dm = DriveManagement();
#endif
            curent = new User();
        }
        private apiConnector(User curent)
        {
            this.curent = curent;
        }
        public apiConnector createReader()
        {
            return new apiConnector(dm.getDatabase());
        }
        public apiConnector createReader(string qrResult)
        {

            string[] signs = qrResult.Split(';');
            User temp = new User(signs[0], signs[1]);
            apiConnector apiConn = new apiConnector();
            if (!dm.createDatabase(temp))
            {
                apiConn.curent = temp;
            }
            else
            {
                apiConn.curent = dm.getDatabase();
            }
            return apiConn;
        }

        //Methods
        public bool CheckLogIN()
        {
            bool returner = false;
            try
            {
                System.Threading.Tasks.Task<string> resi = getStringResponse((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.phrase));
                resi.Start();
                resi.Wait();
                returner = bool.Parse(resi.Result);
            }
            catch (Exception e)
            {
            }
            return returner;
        }
        public string Anmelden(bool anmelden,string trmIF, out bool anmeldung)
        {
            System.Threading.Tasks.Task < string > resi = getStringResponse((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.phrase + apiTrmIDIS + trmIF + apiAnAbML + (anmelden?apiAnML:apiAbML)));
            resi.Start();
            resi.Wait();
            string result = resi.Result;

            if (result.Contains("Neuer Status: angemeldet. Danke!"))
            {
                anmeldung = true;
                return "Du wurdest angemeldet";
            }
            else if (result.Contains("Neuer Status: abgemeldet. Danke!"))
            {
                anmeldung = false;
                return "Du wurdest abgemeldet";
            }
            else if (result.Contains("Der Termin liegt in der Vergangenheit. Keine Änderungen mehr möglich."))
            {
                anmeldung = true;
                return "Änderung nicht Möglich!";
            }
            anmeldung = true;
            return "Netzwerk fehler.";
                
                    
       }
        private async System.Threading.Tasks.Task<string> getStringResponse(HttpWebRequest wq)
        {
            try
            {
                using (WebResponse response = await wq.GetResponseAsync())
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ec)
            {
                return ec.ToString();
            }
        }
        //Handle Async in *Form* and add method to handle async response like: task.ContinueWith(x => Console.WriteLine(x.Result));
        public System.Threading.Tasks.Task<List<Appointment>> getTermine()
        {
            return LoadTermine((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.phrase + apiTrmIDIS + "alle"));
        }
        public List<Appointment> getTermine(string trmId)
        {
            System.Threading.Tasks.Task<List<Appointment>> lisi = LoadTermine((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.phrase + apiTrmIDIS + trmId));
            lisi.Start();
            lisi.Wait();
            return lisi.Result;
        }
        private async System.Threading.Tasks.Task<List<Appointment>> LoadTermine(HttpWebRequest myRequest)
        {
            //List<Termine> terminListe = new List<Termine>();
            List<Appointment> Resu = null;
            using (WebResponse response = await myRequest.GetResponseAsync())
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    Resu = Appointment.CreateAppointmentList(ParseJson.JsonArrayToDictionaryList(reader.ReadToEnd()));

                }
            }
            return Resu;
        }


        [Obsolete("Ony for Debug")]
        public String AppointmentToList(List<Appointment> resu)
        {
            string mesu = "";
            foreach(Appointment sesu in resu)
            {
                mesu += sesu.ToString();
            }
            return mesu;
        }
    }
}
