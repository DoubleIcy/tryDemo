using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
namespace League_OF_Legends
{
    public class MyCSV
    {
        DataTable dt = new DataTable();
        public DataTable ReadFile(string filename)
        {
            if (File.Exists(filename))
            {

                StreamReader sr = new StreamReader(filename);//encoding;
                while (true)
                {
                    string fileDataLine = sr.ReadLine();
                    if (string.IsNullOrEmpty(fileDataLine))
                        break;
                    Split(fileDataLine);
                } 
            }

            return dt;
        }
        int maxCol = 0, maxRow = 0;
        void Split(string str)
        {
            string[] strs = str.Split(',');
            if (strs.Length > maxCol)
                maxCol = strs.Length;
            maxRow++;
            if (maxRow == 1)
            {
                for (int j = 0; j < strs.Length;j++ )
                    dt.Columns.Add();
            }

            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            for (int j = 0; j < strs.Length; j++)
            {
                dt.Rows[maxRow - 1][j] = strs[j];
            }
        }

        public DataTable DataTableCap(DataTable _dt)
        {
            if (_dt == null || _dt.Rows.Count == 0)
                return _dt;
            //foreach (DataColumn dc in _dt.Columns)
            //{
            //    dc.Caption=_dt.Columns[dc]
            //}
            for(int j=0;j<dt.Columns.Count;j++)
            {
                _dt.Columns[j].Caption = ToStr(_dt.Rows[0][j]);
                _dt.Columns[j].ColumnName = ToStr(_dt.Rows[0][j]); 
            }
            _dt.Rows.RemoveAt(0);
            _dt.AcceptChanges();
            return _dt;
        }
        public string ToStr(object obj)
        {
            return Convert.ToString(obj);
        }
    }
}
