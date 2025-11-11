using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CuahangNongduoc
{

    public class DataService : DataTable
    {
        // The connection to a database of this data service.

        //private static OleDbConnection	m_Connection;
        private static SqlConnection m_Connection;

        //public static String m_ConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=cuahang.dll;";
        public static String m_ConnectString = @"Server=.\SQLEXPRESS; Database = CHNONGDUOC2; Integrated Security = True; MultipleActiveResultSets = True; TrustServerCertificate = True";

        //public static String m_ConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Admin Pc\\Downloads\\New folder\\do an\\Cuahang_Nongduoc\\Cuahang Nongduoc\\bin\\Debug/cuahang.dll;";
        // The command to execute query or non-query command on a database of this data service.
        //private OleDbCommand		m_Command;
        private SqlCommand m_Command;

        // The data adapter to execute query on a database of this data service.
        //private OleDbDataAdapter	m_DataAdapter;
        private SqlDataAdapter m_DataAdapter;
        public DataService() { }


        public SqlCommand Command
        {
            get { return m_Command; }
            set { m_Command = value; }
        }

        public void Load(SqlCommand command)
        {
            OpenConnection();
            m_Command = command;
            try
            {

                m_Command.Connection = m_Connection;

                m_DataAdapter = new SqlDataAdapter();
                m_DataAdapter.SelectCommand = m_Command;

                this.Clear();
                m_DataAdapter.Fill(this);

            }
            catch (Exception e)
            {
                String str = e.Message;
            }
        }

        public bool ExecuteAdapter(SqlDataAdapter da)
        {
            using (SqlConnection cn = m_Connection)
            {
                da.InsertCommand.Connection = cn;
                da.UpdateCommand.Connection = cn;
                da.DeleteCommand.Connection = cn;
                cn.Open();
                da.Update(this);
                cn.Close();
                return true;
            }
        }



        //public static bool OpenConnection()
        //{
        //    try
        //    {
        //        if (m_Connection == null)
        //            //m_Connection = new OleDbConnection("Data Source=LAPTOP07\\OleDbEXPRESS;Initial Catalog=VCB;Integrated Security=True;");
        //            //m_Connection = new OleDbConnection("Data Source=localhost;Initial Catalog=phongkham;User ID=sa; Password=tvteo;");
        //            m_Connection = new SqlConnection(m_ConnectString);


        //        if (m_Connection.State == ConnectionState.Closed)
        //            m_Connection.Open();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        m_Connection.Close();
        //        return false;
        //    }

        //}

        public static bool OpenConnection()
        {
            try
            {
                if (m_Connection == null)
                    m_Connection = new SqlConnection(m_ConnectString);

                if (m_Connection.State == ConnectionState.Closed)
                    m_Connection.Open();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không th? k?t n?i SQL Server: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Closes the connection of this data service.
        /// </summary>
        public void CloseConnection()
        {
            m_Connection.Close();
        }

        /// <summary>
        /// Update DataTable
        /// </summary>
        /// <returns></returns>
        public int ExecuteNoneQuery()
        {
            int result = 0;
            SqlTransaction tr = null;
            try
            {
                OpenConnection();
                tr = m_Connection.BeginTransaction();

                m_Command.Connection = m_Connection;
                m_Command.Transaction = tr;

                m_DataAdapter = new SqlDataAdapter();
                m_DataAdapter.SelectCommand = m_Command;

                SqlCommandBuilder builder = new SqlCommandBuilder(m_DataAdapter);

                result = m_DataAdapter.Update(this);


                tr.Commit();

            }
            catch (Exception e)
            {
                if (tr != null) tr.Rollback();

            }
            return result;
        }
        /// <summary>
        /// Thuc thi mot command
        /// </summary>
        /// <param name="command">OleDb hay Store Procedure</param>
        /// <returns></returns>
        public int ExecuteNoneQuery(SqlCommand cmd)
        {

            int result = 0;
            SqlTransaction tr = null;

            try
            {
                OpenConnection();

                tr = m_Connection.BeginTransaction();

                cmd.Connection = m_Connection;

                cmd.Transaction = tr;

                result = cmd.ExecuteNonQuery();

                this.AcceptChanges();

                tr.Commit();

            }
            catch (Exception e)
            {
                if (tr != null) tr.Rollback();
                throw;
            }
            return result;

        }

        public int Update()
        {
            int result = 0;
            try
            {
                if (m_DataAdapter == null)
                {
                    if (m_Command == null)
                        throw new InvalidOperationException("Không có SqlCommand hoặc SqlDataAdapter để cập nhật dữ liệu.");

                    m_DataAdapter = new SqlDataAdapter(m_Command);
                    SqlCommandBuilder builder = new SqlCommandBuilder(m_DataAdapter);
                }

                OpenConnection();
                result = m_DataAdapter.Update(this);
                this.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
        public object ExecuteScalar(SqlCommand cmd)
        {
            object result = null;
            SqlTransaction tr = null;

            try
            {
                OpenConnection();

                tr = m_Connection.BeginTransaction();

                cmd.Connection = m_Connection;

                cmd.Transaction = tr;

                result = cmd.ExecuteScalar();

                this.AcceptChanges();

                tr.Commit();

                if (result == DBNull.Value)
                {
                    result = null;
                }
            }
            catch
            {
                if (tr != null) tr.Rollback();
                throw;
            }
            return result;
        }
    }
}
