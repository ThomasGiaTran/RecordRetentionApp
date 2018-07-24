using Microsoft.AspNet.Identity;
using RecordRetentionApp.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace RecordRetentionApp.Managers
{
    public class AccountManager
    {
        static string TylerConString = ConfigurationManager.ConnectionStrings["TylerCNB"].ConnectionString;
        static string DefaultConString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static string JasmineString = ConfigurationManager.ConnectionStrings["RecordRetentionDB"].ConnectionString;

        public bool ValidLogin(LoginViewModel vm)
        {
            bool valid = false;
            string dbPassword = "";
            int iknt = 0;
            SqlConnection dbConn = new SqlConnection();
            dbConn.ConnectionString = TylerConString;
            dbConn.Open();

            SqlCommand dbComm = default(SqlCommand);
            SqlDataReader dbread = default(SqlDataReader);

            string strSQL;
            strSQL = "SELECT phash ";
            strSQL = strSQL + "FROM cnb_auth ";
            strSQL = strSQL + "WHERE RTrim(empl_no) = @EmplNo ";

            //response.write(strSQL)
            dbComm = new SqlCommand(strSQL, dbConn);
            dbComm.Parameters.AddWithValue("@EmplNo", vm.empl_no.Trim());
            dbread = dbComm.ExecuteReader();


            while (dbread.Read())
            {
                iknt = iknt + 1;
                dbPassword = dbread["phash"].ToString();
            }

            dbread.Close();
            dbComm = null;
            dbConn.Close();
            dbConn = null;

            if (iknt > 0)
            {
                PasswordHasher myPasswordHasher = new PasswordHasher();
                PasswordVerificationResult result;
                result = myPasswordHasher.VerifyHashedPassword(dbPassword, vm.Password);
                if (result.ToString().Trim() == "Success")
                {
                    valid = true;
                }
            }
            return valid;
        }

        public bool Existed(LoginViewModel vm)
        {
            SqlConnection dbConn = new SqlConnection();
            dbConn.ConnectionString = DefaultConString;
            dbConn.Open();

            SqlCommand dbComm = default(SqlCommand);
            SqlDataReader dbread = default(SqlDataReader);
            string strSQL = null;
            int iknt = 0;

            strSQL = "SELECT * FROM [tyler_cnb].[dbo].[AspNetUsers] ";
            strSQL += "WHERE UserName = @EmplNo ";

            //response.write(strSQL)
            dbComm = new SqlCommand(strSQL, dbConn);
            dbComm.Parameters.AddWithValue("@EmplNo", vm.empl_no.Trim());
            dbread = dbComm.ExecuteReader();

            while (dbread.Read())
            {
                iknt = iknt + 1;
            }

            dbread.Close();
            dbComm = null;
            dbConn.Close();
            dbConn = null;

            if (iknt > 0)
                return true;
            else
                return false;
        }

        public string getUserNames(string s)
        {
            SqlConnection dbConn = new SqlConnection();
            dbConn.ConnectionString = DefaultConString;
            dbConn.Open();

            SqlCommand dbComm = default(SqlCommand);
            SqlDataReader dbread = default(SqlDataReader);
            string strSQL = null;

            strSQL = "SELECT * FROM [tyler_cnb].[dbo].[CNB_users] ";
            strSQL += "WHERE empl_no = @EmplNo ";

            //response.write(strSQL)
            dbComm = new SqlCommand(strSQL, dbConn);
            dbComm.Parameters.AddWithValue("@EmplNo", s);
            dbread = dbComm.ExecuteReader();

            string fullName = "";

            while (dbread.Read())
            {
                fullName = dbread["FirstName"].ToString() + " " + dbread["LastName"].ToString();
            }

            dbread.Close();
            dbComm = null;
            dbConn.Close();
            dbConn = null;

            return fullName;
        }

        public List<Office_Of_Record> getOffice_Of_Record()
        {
            List<Office_Of_Record> Office_Of_Record = new List<Office_Of_Record>();

            SqlConnection dbConn = new SqlConnection();
            dbConn.ConnectionString = JasmineString;
            dbConn.Open();

            SqlCommand dbComm = default(SqlCommand);
            SqlDataReader dbread = default(SqlDataReader);
            string strSQL = null;

            strSQL = "SELECT distinct Office_Of_Record FROM retention_schedule ";

            dbComm = new SqlCommand(strSQL, dbConn);
            dbread = dbComm.ExecuteReader();

            while (dbread.Read())
            {
                Office_Of_Record record = new Office_Of_Record();
                record.Office_Of_Record_Name = dbread["Office_Of_Record"].ToString();
                Office_Of_Record.Add(record);
            }

            dbread.Close();
            dbComm = null;
            dbConn.Close();
            dbConn = null;

            return Office_Of_Record;
        }

        public List<retention_schedule> getFolderNames()
        {
            List<retention_schedule> folderName = new List<retention_schedule>();

            SqlConnection dbConn = new SqlConnection();
            dbConn.ConnectionString = JasmineString;
            dbConn.Open();

            SqlCommand dbComm = default(SqlCommand);
            SqlDataReader dbread = default(SqlDataReader);
            string strSQL = null;

            strSQL = "SELECT distinct Folder_Name FROM retention_schedule ";

            dbComm = new SqlCommand(strSQL, dbConn);
            dbread = dbComm.ExecuteReader();

            while (dbread.Read())
            {
                retention_schedule record = new retention_schedule();
                record.Folder_Name = dbread["Folder_Name"].ToString();
                folderName.Add(record);
            }

            dbread.Close();
            dbComm = null;
            dbConn.Close();
            dbConn = null;

            return folderName;
        }
    }
}