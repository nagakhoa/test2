﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeagueSharp.Common
{
    public static class MultiLanguage
    {
        public class TranslatedEntry
        {
            [XmlAttribute]
            public string TextToTranslate;
            [XmlAttribute]
            public string TranslatedText;
        }

        public static Dictionary<string, string> Translations = new Dictionary<string, string>();
        public static XmlSerializer Serializer = new XmlSerializer(typeof(TranslatedEntry[]), new XmlRootAttribute() { ElementName = "entries" });

        public static bool LoadLanguage(string name)
        {
            var filePath = Path.Combine(Config.LeagueSharpDirectory, "translations",  name + ".xml");
            if(File.Exists(filePath))
            {
                try
                {
                    Translations = ((TranslatedEntry[])Serializer.Deserialize(File.OpenRead(filePath))).ToDictionary(i => i.TextToTranslate, i => i.TranslatedText);
                    return true;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.ToString());
                    return false;
                }
            }

            return false;
        }

        public static string _(string textToTranslate)
        {
            return Translations.ContainsKey(textToTranslate) ? Translations[textToTranslate] : textToTranslate;
        }
    }
}