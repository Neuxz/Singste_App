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
using System.IO;
using Android.Content.Res;
using Mono.Data.Sqlite;
using System.Data.SqlClient;
//using System.Data.SqlClient;

namespace SingsteApp
{
    class DriveManagementAndroid : Singste_App.DriveManagement
    {
        private static string dbName = "Temp.cach";
        private static string databasePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        public override bool createDatabase(User current)
        {
            /*if (!File.Exists(databasePath))
            {
                //http://stackoverflow.com/questions/18715613/use-a-local-database-in-xamarin
                //https://forums.xamarin.com/discussion/6990/how-to-correctly-save-and-read-files
                AssetManager assets = new ContextWrapper(ct).Assets;
                using (StreamReader br = new StreamReader(assets.Open(dbName)))
                {
                    using (StreamWriter bw = new StreamWriter(new FileStream(databasePath, FileMode.Create)))
                    {
                        bw.Write(br.ReadToEnd());
                        bw.Dispose();
                        bw.Close();
                    }
                
                }
            }*/
            bool retur = getDatabase().phrase == null;
            if (retur)
            {
                using (SqliteConnection co = new SqliteConnection("Data Source=" + databasePath))
                {
                    co.Open();
                    SqliteCommand cmd = co.CreateCommand();//   .CreateCommand();
                    cmd.CommandText = "CREATE TABLE User (	`ID`	TEXT,	`Name`	TEXT,	`Chor`	TEXT,	`Appointments`	BLOB)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Insert into User (ID,Name, Chor, Appointments) values(@id, @name, @chor, @appoi)";
                    List<SqliteParameter> sqlisat = new List<SqliteParameter>() {
                    new SqliteParameter("@id", current.usrID),
                    new SqliteParameter("@name", current.phrase),
                    new SqliteParameter("@chor", current.usrCH),
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
                co.Open();
                SqliteCommand cmd = co.CreateCommand();
                cmd.CommandText = "Select * From User";
                try
                {
                    SqliteDataReader read = cmd.ExecuteReader();
                    if (read.Read())
                    {

                        try { result.usrID = (string)read["ID"]; } catch { result.usrID = ""; }
                        try { result.phrase = (string)read["Name"]; } catch { throw new Exception("NO Passphrase"); }
                        try { result.usrCH = (string)read["Chor"]; } catch { result.usrCH = ""; }
                        try { result.storage = (List<Appointment>)read["Appointments"]; } catch { }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return result;
        }
    }
}