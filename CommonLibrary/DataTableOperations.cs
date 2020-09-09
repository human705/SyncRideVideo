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

        public DataTable LoadTableFromGCRideList(GoldenCheetahRide thisRide)
        {
            if (thisRide.RIDE.SAMPLES.Count() == 0)
            {
                return null;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("secs", typeof(int));
            dt.Columns.Add("km", typeof(double));
            dt.Columns.Add("cad", typeof(int));
            dt.Columns.Add("kph", typeof(double));
            dt.Columns.Add("hr", typeof(double));
            dt.Columns.Add("alt", typeof(double));
            dt.Columns.Add("lat", typeof(double));
            dt.Columns.Add("lon", typeof(double));
            dt.Columns.Add("slope", typeof(double));

            for (int i = 0; i < thisRide.RIDE.SAMPLES.Count() - 1; i++)
            {
                DataRow dr = dt.NewRow();

                dr["secs"] = thisRide.RIDE.SAMPLES[i].SECS;
                dr["km"] = thisRide.RIDE.SAMPLES[i].KM;
                dr["cad"] = thisRide.RIDE.SAMPLES[i].CAD;
                dr["kph"] = thisRide.RIDE.SAMPLES[i].KPH;
                dr["hr"] = thisRide.RIDE.SAMPLES[i].HR;
                dr["alt"] = thisRide.RIDE.SAMPLES[i].ALT;
                dr["lat"] = thisRide.RIDE.SAMPLES[i].LAT;
                dr["lon"] = thisRide.RIDE.SAMPLES[i].LON;
                dr["slope"] = thisRide.RIDE.SAMPLES[i].SLOPE;
                dt.Rows.Add(dr); //add other rows  
            }
                return dt;
        }

        public void UpdateRideListSamplesFromTable(DataTable thisDataTable, GoldenCheetahRide thisGCRide)
        {
            thisGCRide.RIDE.SAMPLES = null;
            thisGCRide.RIDE.SAMPLES = new List<SAMPLE>();
            foreach (DataRow dr in thisDataTable.Rows)
            {
                SAMPLE newSample = new SAMPLE();
                newSample.SECS = (int)dr["secs"];
                newSample.KM = (double)dr["km"];
                newSample.CAD = (int)dr["cad"];
                newSample.KPH = (double)dr["kph"];
                newSample.HR = (double)dr["hr"];
                newSample.ALT = (double)dr["alt"];
                newSample.LAT = (double)dr["lat"];
                newSample.LON = (double)dr["lon"];
                newSample.SLOPE = (double)dr["slope"];

                thisGCRide.RIDE.SAMPLES.Add(newSample);
            }
        }

        public void UpdateTableFromRideListSamples(DataTable thisDataTable, GoldenCheetahRide thisGCRide)
        {
            thisDataTable.Clear();
            for (int i = 0; i < thisGCRide.RIDE.SAMPLES.Count() - 1; i++)
            {
                DataRow dr = thisDataTable.NewRow();

                dr["secs"] = thisGCRide.RIDE.SAMPLES[i].SECS;
                dr["km"] = thisGCRide.RIDE.SAMPLES[i].KM;
                dr["cad"] = thisGCRide.RIDE.SAMPLES[i].CAD;
                dr["kph"] = thisGCRide.RIDE.SAMPLES[i].KPH;
                dr["hr"] = thisGCRide.RIDE.SAMPLES[i].HR;
                dr["alt"] = thisGCRide.RIDE.SAMPLES[i].ALT;
                dr["lat"] = thisGCRide.RIDE.SAMPLES[i].LAT;
                dr["lon"] = thisGCRide.RIDE.SAMPLES[i].LON;
                dr["slope"] = thisGCRide.RIDE.SAMPLES[i].SLOPE;
                thisDataTable.Rows.Add(dr); //add other rows  
            }
        }
    }
}
