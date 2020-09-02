using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Xml;

namespace CommonLibrary
{
    public class DataTableOperations
    {

        public DataTable CreateCorrectionPointsTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FileTimeInSecs", typeof(int));
            dt.Columns.Add("VideoTimeInSecs", typeof(int));
            dt.Columns.Add("Latitude", typeof(double));
            dt.Columns.Add("Longitude", typeof(double));
            dt.Columns.Add("DistanceFromStart", typeof(double));
            dt.Columns.Add("DistanceFromPrevious", typeof(double));

            return dt;
        }

        public void WriteCorrectionPointstoCSV(string fullFileName, DataTable dt)
        {
            using (FileStream fs = File.OpenWrite(fullFileName))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                string t = dr[i].ToString();
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            };

            
        }

        public DataTable LoadCorrectionPointsFromCSV (string fullFileName, DataTable dt)
        {
            string Fulltext;
            dt.Clear();
            using (StreamReader sr = new StreamReader(fullFileName))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  

                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        DataRow dr = dt.NewRow();

                        dr[0] = Convert.ToInt32(rowValues[0]);
                        dr[1] = Convert.ToInt32(rowValues[1]);
                        dr[2] = Convert.ToDouble(rowValues[2]);
                        dr[3] = Convert.ToDouble(rowValues[3]);
                        dr[4] = Convert.ToDouble(rowValues[4]);
                        dr[5] = Convert.ToDouble(rowValues[5]);

                        dt.Rows.Add(dr); //add other rows  
                    }
                }
            }

            return dt;

        }
        public DataTable ReadCsvFileToDataTable(string fullFileName)
        {

            DataTable dtCsv = new DataTable();
            string Fulltext;
            using (StreamReader sr = new StreamReader(fullFileName))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  

                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }

            return dtCsv;
        }

    }
}
