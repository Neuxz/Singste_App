using System;
using System.Collections.Generic;
using Android.Content;
using System.IO;
using Mono.Data.Sqlite;
using Singste_App;
using Singste_App.Droid;

namespace Singste_App
{
    class DriveManagementAndroid : DriveManagement
    {
        private static string dbName = "Temp.cach";
        private static string databasePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        public override bool createDatabase(User current)
        {
            bool retur = getDatabase().phrase == null;
            if (retur)
            {
                using (SqliteConnection co = new SqliteConnection("Data Source=" + databasePath))
                {
                    co.Open();
                    SqliteCommand cmd = co.CreateCommand();//   .CreateCommand();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS User (	`ID`	TEXT,	`Name`	TEXT,	`Chor`	TEXT, `Delay` INTEGER, `Appointments`	BLOB)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Insert into User (ID,Name, Chor, Delay, Appointments) values(@id, @name, @chor, @delay, @appoi)";
                    new List<SqliteParameter>() {
                    new SqliteParameter("@id", current.usrID),
                    new SqliteParameter("@name", current.phrase),
                    new SqliteParameter("@chor", current.usrCH),
                    new SqliteParameter("@delay", current.delay),
                    new SqliteParameter("@appoi", current.storage)
                    }.ForEach(cmdPam => cmd.Parameters.Add(cmdPam));
                    cmd.ExecuteNonQuery();
                }
            }
            return retur;
        }

        public override List<User> getDatabaseList()  
        {
            List<User> result = new List<User>();
            using (SqliteConnection co = new SqliteConnection("Data Source=" + databasePath))
            {
                try
                {
                    co.Open();
                    SqliteCommand cmd = co.CreateCommand();
                    cmd.CommandText = "Select * From User";
                    SqliteDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        User temp = User.getEmptyUser();
                        try { temp.usrID = (string)read["ID"]; } catch { temp.usrID = ""; }
                        try { temp.phrase = (string)read["Name"]; } catch { throw new Exception("NO Passphrase"); }
                        try { temp.usrCH = (string)read["Chor"]; } catch { temp.usrCH = ""; }
                        try { temp.delay = (int)read["Delay"]; } catch { temp.delay = 5000; }
                        try { temp.storage = (List<Appointment>)read["Appointments"]; } catch { }
                        result.Add(temp);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return result;
        }
        public override User getDatabase()
        {
            List<User> result = new List<User>();
            using (SqliteConnection co = new SqliteConnection("Data Source=" + databasePath))
            {
                try
                {
                    co.Open();
                    SqliteCommand cmd = co.CreateCommand();
                    cmd.CommandText = "Select * From User";
                    SqliteDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        User temp = User.getEmptyUser();
                        try { temp.usrID = (string)read["ID"]; } catch { temp.usrID = ""; }
                        try { temp.phrase = (string)read["Name"]; } catch { throw new Exception("NO Passphrase"); }
                        try { temp.usrCH = (string)read["Chor"]; } catch { temp.usrCH = ""; }
                        try { temp.delay = (int)read["Delay"]; } catch { temp.delay = 5000; }
                        try { temp.storage = (List<Appointment>)read["Appointments"]; } catch { }
                        result.Add(temp);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return result[0];
        }
    }
}