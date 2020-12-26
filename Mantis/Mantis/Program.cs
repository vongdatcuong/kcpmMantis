using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis
{
    class Program
    {
        static void Main(string[] args)
        {
            registerChrome(@"C:\Users\vdcuo\OneDrive\Desktop\Nam 4\Kiem thu phan mem\BTCN10\1712314-Selenium\1712314-Source\register-data-chrome.xlsx");
            registerFirefox(@"C:\Users\vdcuo\OneDrive\Desktop\Nam 4\Kiem thu phan mem\BTCN10\1712314-Selenium\1712314-Source\register-data-firefox.xlsx");
            setAuthChrome(@"C:\Users\vdcuo\OneDrive\Desktop\Nam 4\Kiem thu phan mem\BTCN10\1712314-Selenium\1712314-Source\set-role-data.xlsx");
            setAuthFirefox(@"C:\Users\vdcuo\OneDrive\Desktop\Nam 4\Kiem thu phan mem\BTCN10\1712314-Selenium\1712314-Source\set-role-data.xlsx");
        }

        // Register
        static void registerChrome(string filePath)
        {
            RegisterTest register = new RegisterTest();
            register.SetUp(0);
            DataTable dt = ReadExcelToDt(filePath);
            foreach (DataRow Row in dt.Rows)
            {
                string username = Row["username"].ToString();
                string email = Row["email"].ToString();
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
                {
                    register.register(username, email);
                }
            }
        }

        static void registerFirefox(string filePath)
        {
            RegisterTest register = new RegisterTest();
            register.SetUp(1);
            DataTable dt = ReadExcelToDt(filePath);
            foreach (DataRow Row in dt.Rows)
            {
                string username = Row["username"].ToString();
                string email = Row["email"].ToString();
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
                {
                    register.register(username, email);
                }
            }
        }

        // Set Authorization
        static void setAuthChrome(string filePath)
        {
            SetAuthTest setAuth = new SetAuthTest();
            setAuth.SetUp(0, "admin", "123456");
            DataTable dt = ReadExcelToDt(filePath);
            foreach (DataRow Row in dt.Rows)
            {
                string username = Row["username"].ToString();
                string role = Row["role"].ToString();
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(role))
                {
                    setAuth.setAuth(username, role);
                }
            }
        }

        static void setAuthFirefox(string filePath)
        {
            SetAuthTest setAuth = new SetAuthTest();
            setAuth.SetUp(1, "admin", "123456");
            DataTable dt = ReadExcelToDt(filePath);
            foreach (DataRow Row in dt.Rows)
            {
                string username = Row["username"].ToString();
                string role = Row["role"].ToString();
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(role))
                {
                    setAuth.setAuth(username, role);
                }
            }
        }

        public static DataTable ReadExcelToDt(string filePath)
        {
            // Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                DataTable dt = new DataTable();

                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Loop through the Worksheet rows.
                bool first = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Add Header
                    if (first)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        first = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        var firstCell = row.FirstCellUsed();
                        var lastCell = row.LastCellUsed();
                        if (firstCell == null || lastCell == null)
                            continue;
                        foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }

                return dt;
            }
        }
    }
}
