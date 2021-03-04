using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LAB2
{
    class DBUtils
    {
        SqlConnection conn = null;
        SqlCommand command = null;
        SqlDataReader dataReader = null;

        public SqlConnection getConnection()
        {
            string connetionString = @"Data Source=SE130172;User ID=sa;Password=123456";
            conn = new SqlConnection(connetionString);
            return conn;
        }
        //public void temp()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    finally
        //    {
        //        if (dataReader != null)
        //        {
        //            dataReader.Close();
        //        }
        //        if (command != null)
        //        {
        //            command.Dispose();
        //        }
        //        if (conn != null)
        //        {
        //            conn.Close();
        //        }
        //    }
        //}
        public bool checkDBExisted(string databaseName)
        {
            bool result = false;
            try
            {
                string sql = "SELECT name FROM master.dbo.sysdatabases WHERE name = '" + databaseName + "'";
                command = new SqlCommand(sql, conn);
                dataReader = command.ExecuteReader();
                result = dataReader.Read();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
            
            return result;
        }
        

        public void createDB(string databaseName)
        {
            try
            {
                string sql = "CREATE DATABASE " + databaseName;
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
            
        }
        public bool checkTableExisted(string databaseName, string tableName)
        {
            bool result = false;
            try
            {
                string sql = "USE " + databaseName + " SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '" + tableName + "'";
                command = new SqlCommand(sql, conn);
                dataReader = command.ExecuteReader();
                result = dataReader.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
            return result;
        }
        public void createTable(string databaseName, string tableName, string data)
        {
            try
            {
                string sql = "CREATE TABLE " + databaseName + ".dbo." + tableName + " (" + data + ")";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
            
        }
        public void removeTableData(string databaseName, string tableName)
        {
            try
            {
                string sql = "DELETE FROM " + databaseName + ".dbo." + tableName;
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
            
        }

        public void addFK(string databaseName, string tableName1, string tableName2, string columnName1, string columnName2, string fkName)
        {
            try
            {
                string sql = "ALTER TABLE " + databaseName + ".dbo." + tableName1 + " ADD CONSTRAINT " + fkName + " FOREIGN KEY (" + columnName1 + ") REFERENCES " + databaseName + ".dbo." + tableName2 + "(" + columnName2 + ")";
                //Console.WriteLine(sql);
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
            
        }
    }
}
