using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class TextConnector
    {

        public string FullFilePath(string fileName)
        {
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{ fileName } ";
        }

        public List<string> LoadFile(string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public List<CorrectionPoint> ConvertToCorrectionPointsList(List<string> lines)
        {
            List<CorrectionPoint> output = new List<CorrectionPoint>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                CorrectionPoint cp = new CorrectionPoint
                {
                    FileTimeInSecs = int.Parse(cols[0]),
                    VideoTimeInSecs = int.Parse(cols[1]),
                    Latitude = double.Parse(cols[2]),
                    Longitude = double.Parse(cols[3]),
                    DistanceFromStart = double.Parse(cols[4]),
                    DistanceFromPrevious = double.Parse(cols[5])
                };
                output.Add(cp);
            }

            return output;
        }

        public void SaveFile (string file, List<CorrectionPoint> points)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            //else
            //{
                
            //    FileStream fs = new FileStream(file, FileMode.Truncate);
            //    fs.Close();
            //}
            FileStream ofstream = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(ofstream);
            sw.AutoFlush = true;

            foreach (var point in points)
            {
                string lineContent = point.FileTimeInSecs + "," +
                    point.VideoTimeInSecs + "," +
                    point.Latitude + "," +
                    point.Longitude + "," +
                    point.DistanceFromStart + "," +
                    point.DistanceFromPrevious;
                sw.WriteLine(lineContent);
            }
            sw.Close();
        }
    }
}
