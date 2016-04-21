using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singste_App
{
    public class User
    {
        public string usrID;
        public string usrCH;
        public string phrase;
        public int delay;
        public List<Appointment> storage = new List<Appointment>();
        public User() { }
        public User(string phrase, string chr)
        {
            this.phrase = phrase;
            this.usrCH = chr;

        }
        public void updateLocalTerminlist(List<Appointment> newList)
        {
            foreach(Appointment ap in newList)
            {
                if(!storage.Any(appointment => appointment.Trm_id.Equals(ap.Trm_id)))
                {
                    storage.Add(ap);
                    ///Alert new Appointment
                }
            }
        }
    }
}