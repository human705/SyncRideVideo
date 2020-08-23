using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommonLibrary
{
    public class DictionalyProcessing
    {
        //public Dictionary<string, object> dict { get; set; }
        public void AddUpdateKey (string key, object dataType, Dictionary<string, object> dict)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, dataType);
            } else
            {
                dict[key] = dataType;
            }
        }

        public T GetAnyValue<T>(string key, Dictionary<string, object> dict)
        {
            object obj;
            T retType;
            dict.TryGetValue(key, out obj);
            try
            {
                retType = (T)obj;
            } catch
            {
                retType = default(T);
            }

            return retType;

        }

        public void WriteDictionaryToCSV(Dictionary<string, object> dictionary, string file)
        {
            using (FileStream fs = File.OpenWrite(file))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                // Put count.
                //writer.Write(dictionary.Count);
                // Write pairs.
                foreach (var pair in dictionary)
                {
                    writer.Write(pair.Key + ",");
                    writer.Write(pair.Value + Environment.NewLine);
                }
            }
        }

        public Dictionary<string, object> LoadDictionaryFromCSV(string file)
        {
            var result = new Dictionary<string, object>();
            using (FileStream fs = File.OpenRead(file))
            using (StreamReader reader = new StreamReader(fs))
            {
                string[] lines = File.ReadAllLines(file);
                if (lines.Length > 0)
                {
                    for (int r = 0; r < lines.Length; r++)
                    {
                        string[] dataWords = lines[r].Split(',');
                        result.Add(dataWords[0], dataWords[1]);
                    }
                }
            }
            return result;
        }
    }


}
