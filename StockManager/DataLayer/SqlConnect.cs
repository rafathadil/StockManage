using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;

namespace StockManager.Model
{
    public static class DataLayer
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader dr;
        static List<string> lsDB = new List<string>();
        static List<string> lsSever = new List<string>();

        static string ConnectionString;

        static string LastDBError;
        public static bool CheckConn(string Conn)
        {
            try
            {
                ConnectionString = Conn;
                con = new SqlConnection(Conn);
                string ConnString = String.Format(Conn);
                con.Open();
                cmd = new SqlCommand("select * from Item_master", con);
                dr = cmd.ExecuteReader();
               
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return false;
            }
            return true;
        }




       


        enum EspAction
        {
            Insert, Select, Update, Delete
        }

        #region Master
        public static bool InsertItemIntoMaster(MStockitem RequestModel)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("MasterInsertUpdateDelete");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999999;
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemNo", Value = RequestModel.ItemNo });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@ItemName", Value = RequestModel.ItemName });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@Price", Value = RequestModel.Price });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@Unit", Value = RequestModel.Unit });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@StatementType", Value = EspAction.Insert.ToString() });
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                       
                    }

                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return false;
            }
        }

        public static bool UpdateItemInMaster(MStockitem RequestModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("MasterInsertUpdateDelete");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999999;
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemNo", Value = RequestModel.ItemNo });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@ItemName", Value = RequestModel.ItemName });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@Price", Value = RequestModel.Price });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@Unit", Value = RequestModel.Unit });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@StatementType", Value = EspAction.Update.ToString() });
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    

                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return false;

            }
        }

        public static bool DeleteItemFromMaster(MStockitem RequestModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("MasterInsertUpdateDelete");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999999;
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemNo", Value = RequestModel.ItemNo });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@ItemName", Value = RequestModel.ItemName });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@Price", Value = RequestModel.Price });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@Unit", Value = RequestModel.Unit });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@StatementType", Value = EspAction.Delete.ToString() });
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                   

                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return false;

            }
        }

        public static ObservableCollection<MStockitem> GetDataForMaster()
        {
            try
            {
                ObservableCollection<MStockitem> ClCollection = new ObservableCollection<MStockitem>();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("MasterInsertUpdateDelete");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999999;
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemNo", Value = 0 });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@ItemName", Value = "" });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@Price", Value = 0 });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@Unit", Value = "" });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@StatementType", Value = EspAction.Select.ToString() });
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            MStockitem MStockitem = new MStockitem();
                            if (!(reader["ItemNo"] is DBNull))
                                MStockitem.ItemNo = Convert.ToInt32(reader["ItemNo"]);

                            MStockitem.ItemName = Convert.ToString(reader["ItemName"]);

                            if (!(reader["Price"] is DBNull))
                                MStockitem.Price = Convert.ToDouble(reader["Price"]);

                            MStockitem.Unit = Convert.ToString(reader["Unit"]);

                            if (!(reader["QTY"] is DBNull))
                                MStockitem.Quantity = Convert.ToInt64(reader["QTY"]);

                            if (!(reader["Date"] is DBNull))
                                MStockitem.Date = Convert.ToDateTime(Convert.ToString(reader["Date"]));

                          

                            ClCollection.Add(MStockitem);
                        }
                    }

                    con.Close();
                }
                return ClCollection;
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return new ObservableCollection<MStockitem>();
            }
        }
        #endregion

        #region Detail

       
        public static ObservableCollection<MStockitem> GetDataForDetail()
        {
            try
            {
                ObservableCollection<MStockitem> ClCollection = new ObservableCollection<MStockitem>();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("StockInsertUpdateDelete");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999999;
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemNo", Value = 0 });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemDetailID", Value = 0 });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@QTY", Value = 0 });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@Date", Value = "" });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@StatementType", Value = EspAction.Select.ToString() });
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                          


                            MStockitem MStockitem = new MStockitem();

                            if (!(reader["ItemNo"] is DBNull))
                                MStockitem.ItemNo = Convert.ToInt32(reader["ItemNo"]);
                            MStockitem.ItemName = Convert.ToString(reader["ItemName"]);
                            if (!(reader["QTY"] is DBNull))
                                MStockitem.Quantity = Convert.ToDouble(reader["QTY"]);

                            if (!(reader["Date"] is DBNull))
                                MStockitem.Date = Convert.ToDateTime(Convert.ToString(reader["Date"]));

                            if (!(reader["ItemDetailID"] is DBNull))
                                MStockitem.ItemDetailIDForStock = Convert.ToInt32(reader["ItemDetailID"]);

                            ClCollection.Add(MStockitem);
                        }
                    }
                    

                    con.Close();
                }



                return ClCollection;
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return new ObservableCollection<MStockitem>();
            }
        }


        public static bool InsertItemIntoDetail(MStockitem RequestModel)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("StockInsertUpdateDelete");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999999;
                    if (RequestModel.ItemNoForStock != null)
                        cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemNo", Value = ((KeyValuePair<int, string>)RequestModel.ItemNoForStock).Key });
                    else
                        cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemNo", Value = RequestModel.ItemNo });

                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemDetailID", Value = RequestModel.ItemDetailIDForStock });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@QTY", Value = RequestModel.Quantity });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@Date", Value = RequestModel.Date });


                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@StatementType", Value = RequestModel.ItemDetailIDForStock != 0 ? EspAction.Update.ToString() : EspAction.Insert.ToString() });

                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                   

                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return false;
            }
        }







        public static ObservableCollection<MStockitem> GetItemList()
        {
            try
            {
                ObservableCollection<MStockitem> ClCollection = new ObservableCollection<MStockitem>();

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter("select * from V_GetItemList", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        MStockitem MStockitem = new MStockitem();
                        MStockitem.ItemName = item["ItemName"].ToString();
                        MStockitem.ItemNo = Convert.ToInt32(item["ItemNo"]);
                        ClCollection.Add(MStockitem);
                    }

                }

                return ClCollection;
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return new ObservableCollection<MStockitem>();
            }
        }

        #endregion

        #region Report


        public static DataTable GetReportData(MreportModel MreportModel)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_GetItemBasedData");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999999;
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ItemNo", Value = ((KeyValuePair<int, string>)MreportModel.ItemNo).Key  });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@DateFrom", Value = MreportModel.DateFrom });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@DateTo", Value = MreportModel.DateTO });
                    cmd.Parameters.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@Mode", Value = MreportModel.IsUseDateFilter ? "1" : "0" });
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        dt.Load(reader);

                    }
                    else
                    {
                        con.Close();
                        return null;
                    }

                    con.Close();


                }




                return dt;
            }
            catch (Exception ex)
            {
                LastDBError = ex.Message;
                return null;
            }
        }
        #endregion

    }
}
