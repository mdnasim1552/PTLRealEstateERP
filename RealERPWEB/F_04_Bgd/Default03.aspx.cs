using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using RealEntity;
using RealERPLIB;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace RealERPWEB.F_04_Bgd
{ 

    
public partial class Default03 : System.Web.UI.Page
    {
        ProcessAccess xmldata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lnkObjToXML_Click(object sender, EventArgs e)
        {
            try
            {
                List<RealEntity.C_04_Bgd.EClassEmployee> lst = new List<RealEntity.C_04_Bgd.EClassEmployee>();
                lst.Add(new RealEntity.C_04_Bgd.EClassEmployee("13", "Emdad", "Haque", "mondol", "Sr.Engineer", "Engineer", "ruet", "dhaka"));
                lst.Add(new RealEntity.C_04_Bgd.EClassEmployee("18", "Shobuz", "Haque", "mondol", "Sr.Engineer", "Engineer", "ruet", "dhaka"));
                lst.Add(new RealEntity.C_04_Bgd.EClassEmployee("21", "Nurul", "Haque", "mondol", "Sr.Engineer", "Engineer", "ruet", "dhaka"));
                lst.Add(new RealEntity.C_04_Bgd.EClassEmployee("58", "Debu", "Haque", "mondol", "Sr.Engineer", "Engineer", "ruet", "dhaka"));
                string xmlser = this.CreateXML(lst);
                bool result = xmldata.UpdateTransInfo("", "SP_ENTRY_ACCOUNTS_XMLDATA", "INSERTDATA", xmlser, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {

                    return;
                }
                //  List<RealEntity.C_04_Bgd.EClassEmployee> objemp = (List<RealEntity.C_04_Bgd.EClassEmployee>)CreateObject(xmlser,lst);
            }
            catch (Exception ex) { }
        }



        public string CreateXML(List<RealEntity.C_04_Bgd.EClassEmployee> lst)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
                                                      // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<RealEntity.C_04_Bgd.EClassEmployee>));
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, lst);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }


        }

        public List<RealEntity.C_04_Bgd.EClassEmployee> CreateObject(string XMLString, List<RealEntity.C_04_Bgd.EClassEmployee> lst)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(typeof(List<RealEntity.C_04_Bgd.EClassEmployee>));
            //The StringReader will be the stream holder for the existing XML file 
            lst = (List<RealEntity.C_04_Bgd.EClassEmployee>)oXmlSerializer.Deserialize(new StringReader(XMLString));
            //initially deserialized, the data is represented by an object without a defined type 
            return lst;
        }







        protected void lbtnExcelData_Click(object sender, EventArgs e)
        {








            //Create an Emplyee DataTable
            DataTable employeeTable = new DataTable("Employee");
            employeeTable.Columns.Add("Employee ID");
            employeeTable.Columns.Add("Employee Name");
            employeeTable.Rows.Add("1", "ABC");
            employeeTable.Rows.Add("2", "DEF");
            employeeTable.Rows.Add("3", "PQR");
            employeeTable.Rows.Add("4", "XYZ");

            //Create a Department Table
            DataTable departmentTable = new DataTable("Department");
            departmentTable.Columns.Add("Department ID");
            departmentTable.Columns.Add("Department Name");
            departmentTable.Rows.Add("1", "IT");
            departmentTable.Rows.Add("2", "HR");
            departmentTable.Rows.Add("3", "Finance");

            //Create a DataSet with the existing DataTables
            DataSet ds = new DataSet("Organization");
            ds.Tables.Add(employeeTable);
            ds.Tables.Add(departmentTable);

            this.ExportDataSetToExcel(ds);
        }

        /// <summary>
        /// This method takes DataSet as input paramenter and it exports the same to excel
        /// </summary>
        /// <param name="ds"></param>
        private void ExportDataSetToExcel(DataSet ds)
        {
            //    //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"C:\Org.xlsx");

            foreach (DataTable table in ds.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                // Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();

        }








    }
}