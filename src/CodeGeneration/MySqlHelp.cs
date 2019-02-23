/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/2/13 17:01:30
** desc：    MySqlHelp类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CodeGeneration
{
    public static class MySqlHelper
    {
        #region  建立MySql数据库连接

        private static MySqlConnection _mySqlCon;

        public static string DbConStr = "{0}";

        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回MySqlConnection对象</returns>
        public static void GetMySqlCon(string ip, string port, string userId, string pwd, string dbName = "information_schema")
        {
            var mStrSqlcon = $"Database={dbName};Data Source={ip};User Id={userId};Password={pwd};pooling=false;CharSet=utf8;port={port}";
            MySqlConnection myCon = new MySqlConnection(mStrSqlcon);
            _mySqlCon?.Close();
            _mySqlCon = myCon;

            DbConStr = mStrSqlcon.Replace(dbName, "{0}");
        }
        #endregion

        #region  执行SQL命令
        /// <summary>
        /// 执行MySqlCommand
        /// </summary>
        /// <param name="mStrSqlstr">SQL语句</param>
        public static void ExecuteNonQuery(string mStrSqlstr)
        {
            try
            {
                using (MySqlConnection mysqlcon = _mySqlCon)
                {
                    mysqlcon.Open();
                    MySqlCommand mysqlcom = new MySqlCommand(mStrSqlstr, mysqlcon);
                    mysqlcom.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// 执行MySqlCommand
        /// </summary>
        /// <param name="mStrSqlstr">SQL语句</param>
        public static object ExecuteScalar(string mStrSqlstr)
        {
            try
            {
                using (MySqlConnection mysqlcon = _mySqlCon)
                {
                    mysqlcon.Open();
                    MySqlCommand mysqlcom = new MySqlCommand(mStrSqlstr, mysqlcon);
                    return mysqlcom.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                return "error";
            }
            
        }

        /// <summary>
        /// 创建一个MySqlDataReader对象
        /// </summary>
        /// <param name="mStrSqlstr">SQL语句</param>
        /// <returns>返回MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(string mStrSqlstr)
        {
            MySqlConnection mysqlcon = _mySqlCon;
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(mStrSqlstr, mysqlcon);
            return mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion
    }
}
