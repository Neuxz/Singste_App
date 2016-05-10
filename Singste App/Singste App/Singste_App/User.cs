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
        private static User us;
        private User() { }
        private User(string phrase, string chr)
        {
            this.phrase = phrase;
            this.usrCH = chr;

        }

        public static User getEmptyUser()
        {
            return new User();
        }

        public static User getUser()
        {
            if (us == null)
            {
                us = new User();
                return us;
            }
            return us;
        }

        public void setUser(User newuser)
        {
            us = newuser;
        }

        public static User getUser(string phrase, string chr)
        {
            if(us == null)
            {
                us = new User(phrase, chr);
                return us;
            }
            return us;
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