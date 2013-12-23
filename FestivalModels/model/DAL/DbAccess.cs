using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Festival.Model.DAL
{
    class DbAccess
    {
        private static ConnectionStringSettings ConnectionStringSetting
        {
            //References --> Add --> System.Configuration --> ;+ctrl
            get { return ConfigurationManager.ConnectionStrings["ConnectionString"]; }
        }

        private static DbConnection GetConnection()
        {
            DbConnection con = DbProviderFactories.GetFactory(ConnectionStringSetting.ProviderName).CreateConnection();
            con.ConnectionString = ConnectionStringSetting.ConnectionString;
            con.Open();

            return con;
        }

        public static void ReleaseConnection(DbConnection con)
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

        private static DbCommand BuildCommand(string sql, params DbParameter[] arrParameters)
        {
            DbCommand command = GetConnection().CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;

            if (arrParameters != null)
            {
                command.Parameters.AddRange(arrParameters);
            }

            return command;
        }

        public static DbDataReader GetData(string sql, params DbParameter[] arrParameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;

            try
            {
                command = BuildCommand(sql, arrParameters);
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (reader != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }

        }

        public static int ModifyData(string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(sql, parameters);
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                    return 0;
                }
                throw;
            }
        }

        public static DbParameter AddParameter(string name, object value)
        {
            DbParameter par = DbProviderFactories.GetFactory(ConnectionStringSetting.ProviderName).CreateParameter();
            par.ParameterName = name;
            par.Value = value;

            return par;
        }

        #region Transaction

        public static DbTransaction BeginTransaction()
        {
            DbConnection con = null;
            try
            {
                con = GetConnection();
                return con.BeginTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ReleaseConnection(con);

                throw;
            }
        }

        private static DbCommand BuildCommand(DbTransaction trans, string sql, params DbParameter[] arrParameters)
        {
            DbCommand command = trans.Connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;

            if (arrParameters != null)
            {
                command.Parameters.AddRange(arrParameters);
            }

            return command;
        }

        public static DbDataReader GetData(DbTransaction trans, string sql, params DbParameter[] arrParameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;

            try
            {
                command = BuildCommand(trans, sql, arrParameters);
                command.Transaction = trans;
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (reader != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }

        }

        public static int ModifyData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(trans, sql, parameters);
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                    return 0;
                }
                throw;
            }
        }

        #endregion
    }
}
