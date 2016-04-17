using System;
using System.Collections.Generic;
using System.Text;

namespace SingsteApp
{
    public class ForumTile
    {
        public int frm_id;
        public int mgl_id;
        public string frm_bild;
        public string frm_anzeigename;
        public string frm_nachname;
        public string frm_beitrag;
        public DateTime frm_datum;
        public DateTime frm_modificationtime;
        private ForumTile(Dictionary<String, String> ForumTile)
        {
             string temp;
             if (ForumTile.TryGetValue("frm_id", out temp))
             { frm_id = Int32.Parse(temp);}
            if (ForumTile.TryGetValue("mgl_id", out temp))
             { mgl_id = Int32.Parse(temp);}
            if (ForumTile.TryGetValue("frm_bild", out temp))
             { frm_bild = temp;}
            if (ForumTile.TryGetValue("frm_anzeigename", out temp))
             { frm_anzeigename = temp;}
            if (ForumTile.TryGetValue("frm_nachname", out temp))
             { frm_nachname = temp;}
            if (ForumTile.TryGetValue("frm_beitrag", out temp))
             { frm_beitrag = temp;}
            if (ForumTile.TryGetValue("frm_datum", out temp))
             { frm_datum = DateTime.Parse(temp);}
            if (ForumTile.TryGetValue("frm_modificationtime", out temp))
             { frm_modificationtime = DateTime.Parse(temp);}
        }
        public static List<ForumTile> CreateForumTileList(List<Dictionary<String, String>> ForumTile)
        {
            List<ForumTile> result = new List<ForumTile>();
            foreach (Dictionary<String, String> dic in ForumTile)
            {
                result.Add(new ForumTile(dic));
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Beitrags ID:\t" + frm_id + "\r\n");
            sb.Append("\t Mitglieds ID\t" + mgl_id + "\r\n");
            sb.Append("\t BildVerw.\t" + frm_bild + "\r\n");
            sb.Append("\t V. Name\t" + frm_anzeigename + "\r\n");
            sb.Append("\t N. Name\t" + frm_nachname + "\r\n");
            sb.Append("\t Beitrag\t" + frm_beitrag + "\r\n");
            sb.Append("\t Erstellt am\t" + frm_datum + "\r\n");
            sb.Append("\t Geändert am\t" + frm_modificationtime + "\r\n");
            return sb.ToString();
        }
    }
}
