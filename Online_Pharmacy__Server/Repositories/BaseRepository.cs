using Online_Pharmacy__Server.App_Start;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OnlinePharmacy.Repositories
{
    //complete
    public class BaseRepository
    {
        private readonly SqlConnection conn;

        public BaseRepository()
        {
            conn = AppConfig.DefaultConnection();
        }

        private SqlDataReader GetSqlDataReader(string sql, SqlParameter[] parameters)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                return cmd.ExecuteReader();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ExecuteQuery(string sql, SqlParameter[] parameters, out DataRowCollection data)
        {
            data = null;
            SqlDataReader reader = GetSqlDataReader(sql, parameters);
            if (reader != null)
            {
                DataTable table = new DataTable();
                if (reader.HasRows == true)
                {
                    table.Load(reader);
                }
                reader.Close();
                data = table.Rows;
                reader = null;
                return true;
            }
            return false;
        }

        public bool ExecuteUpdate(string sql, SqlParameter[] parameters, string checkResultOnColumn, out object result)
        {
            result = null;
            SqlDataReader reader = GetSqlDataReader(sql, parameters);
            if (reader != null)
            {
                if (string.IsNullOrWhiteSpace(checkResultOnColumn) == false
                    && reader.HasRows == true
                    && reader.Read() == true)
                {
                    result = reader[checkResultOnColumn];
                }
                reader.Close();
                reader = null;
                return true;
            }
            return false;
        }

    }

}