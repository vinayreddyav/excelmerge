using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace ExcelMerge
{
    public class MergeHelper
    {
        public void MergeExcel(string file1, string file2)
        {
            
            string connectionStringFile1 = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", file1);
            string connectionStringFile2 = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", file2);

            DataSet data = new DataSet();

            


            using (OleDbConnection con = new OleDbConnection(connectionStringFile1))
            {
                var dataTable = new DataTable("File1");
                string query = string.Format("SELECT * FROM [Sheet1$]");
                con.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                adapter.Fill(dataTable);
                data.Tables.Add(dataTable);
            }

            using (OleDbConnection con = new OleDbConnection(connectionStringFile2))
            {
                var dataTable = new DataTable("File2");
                string query = string.Format("SELECT * FROM [Sheet1$]");
                con.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                adapter.Fill(dataTable);
                data.Tables.Add(dataTable);
            }


            //var tablesJoinend = from t1 in data.Tables["File1"].Rows.Cast<DataRow>()
            //                    join t2 in data.Tables["File2"].Rows.Cast<DataRow>() on t1["Deal Number"] equals t2["Deal Number"]
            //                    select t1;

            //var dt = tablesJoinend.CopyToDataTable();

            //data.Tables["File1"].Merge(data.Tables["File2"],false,MissingSchemaAction.Add);



            var datatable1 = data.Tables["File1"];
            var datatable2 = data.Tables["File2"];
            DataTable finalDatatable = new DataTable();

            //DataRelation drel = new DataRelation("EquiJoin", data.Tables["File2"].Columns["Deal Number"], data.Tables["File1"].Columns["Deal Number"]);

            //data.Relations.Add(drel);
            //List<DataColumn> columnNames = new List<DataColumn>();

            foreach (DataRow row in datatable1.Rows)
            {
                foreach (DataColumn column in datatable1.Columns)
                {
                    if (!finalDatatable.Columns.Contains(column.ColumnName))
                        finalDatatable.Columns.Add(column.ColumnName);
                }
            }

            foreach (DataRow row in datatable2.Rows)
            {
                foreach (DataColumn column in datatable2.Columns)
                {
                    if (!finalDatatable.Columns.Contains(column.ColumnName))
                        finalDatatable.Columns.Add(column.ColumnName);
                }
            }

            for (int i = 0; i <= datatable1.Rows.Count - 1; i++)
            {
                var value = datatable1.Rows[i]["numero_tiers"];
                DataRow DataRow1 = datatable1.Rows[i];
                string expression = "numero_tiers = '" + value.ToString() + "'";
                DataRow DataRow2 = datatable2.Select(expression).FirstOrDefault();
                finalDatatable.Rows.Add(new object[] {  DataRow1["numero_tiers"] });
             }

            


                //open file
                StreamWriter wr = new StreamWriter(@"D:\\Book1.xls");

            try
            {

                for (int i = 0; i < finalDatatable.Columns.Count; i++)
                {
                    wr.Write(finalDatatable.Columns[i].ToString().ToUpper() + "\t");
                }

                wr.WriteLine();

                //write rows to excel file
                for (int i = 0; i < (finalDatatable.Rows.Count); i++)
                {
                    for (int j = 0; j < finalDatatable.Columns.Count; j++)
                    {
                        if (finalDatatable.Rows[i][j] != null)
                        {
                            wr.Write(Convert.ToString(finalDatatable.Rows[i][j]) + "\t");
                        }
                        else
                        {
                            wr.Write("\t");
                        }
                    }
                    //go to next line
                    wr.WriteLine();
                }
                //close file
                wr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
