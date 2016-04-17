using System;
using System.Collections.Generic;

namespace SingsteApp
{
    public class ParseJson
    {
        private ParseJson()
        {
        }
        public static List<Dictionary<String, String>> JsonArrayToDictionaryList(String StringDataIn)
        {
            List<Dictionary<String, String>> Json = new List<Dictionary<string, string>>();
            StringDataIn = StringDataIn.Replace("[{", "");//Beginn of Array
            StringDataIn = StringDataIn.Replace("}]", "");//End of Array
            string[] seperator = { "},{" };//Array Element Border
            foreach (string s in StringDataIn.Split(seperator, StringSplitOptions.RemoveEmptyEntries))
            {
                    Json.Add(JsonObjectToDictionary(s));
            }
            return Json;
        }

        public static Dictionary<String, String> JsonObjectToDictionaryList(String StringDataIn)
        {
            Dictionary<String, String> Json = new Dictionary<string, string>();
            StringDataIn = StringDataIn.Replace("{", "");//Beginn of Object
            StringDataIn = StringDataIn.Replace("}", "");//End of Object
            return JsonObjectToDictionary(StringDataIn);
        }

        private static Dictionary<String, String> JsonObjectToDictionary(String StringDataIn)
        {
            Dictionary<String, String> Json = new Dictionary<String, String>();
            string[] seperator = { "\",\"" };//Array Atribut Border
            string[] seperatorA = { "\":" };//Array Atribut Keys
            string[] temp;
            foreach (string s in StringDataIn.Split(seperator, StringSplitOptions.RemoveEmptyEntries))
            {
                temp = s.Split(seperatorA,StringSplitOptions.RemoveEmptyEntries);
                if (temp.Length > 2)
                    throw new Exception("Illigal Argument");
                Json.Add(temp[0].Replace("\"", ""), temp[1].Replace("\"", ""));
            }
            return Json;
        }
    }
}
