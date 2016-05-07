using System;
using System.Collections.Generic;
using Android.Content;
using System.IO;
using Mono.Data.Sqlite;
using Singste_App;
//using System.Data.SqlClient;

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
                    List<SqliteParameter> sqlisat = new List<SqliteParameter>() {
                    new SqliteParameter("@id", current.usrID),
                    new SqliteParameter("@name", current.phrase),
                    new SqliteParameter("@chor", current.usrCH),
                    new SqliteParameter("@delay", current.delay),
                    new SqliteParameter("@appoi", current.storage)
                    };
                    sqlisat.ForEach(cmdPam => cmd.Parameters.Add(cmdPam));
                    cmd.ExecuteNonQuery();
                }
            }
            return retur;
        }

        public override User getDatabase()
        {
            User result = new User();
            using (SqliteConnection co = new SqliteConnection("Data Source=" + databasePath))
                {
                try
                {
                co.Open();
                SqliteCommand cmd = co.CreateCommand();
                cmd.CommandText = "Select * From User";

                    SqliteDataReader read = cmd.ExecuteReader();
                    if(read.Read())
                    {

                        try { result.usrID = (string)read["ID"]; } catch { result.usrID = ""; }
                        try { result.phrase = (string)read["Name"]; } catch { throw new Exception("NO Passphrase"); }
                        try { result.usrCH = (string)read["Chor"]; } catch { result.usrCH = ""; }
                        try { result.delay = (int)read["Delay"]; } catch { result.delay = 5000; }
                        try { result.storage = (List<Appointment>)read["Appointments"]; } catch { }
                    }
                }
                catch(Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return result;
        }
    }
}