using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace 后台管理
{
    public static class SQLHelper
    {
        private const string ConnectionString =
            "Server=tcp:kmri1lx01x.database.windows.net,1433;Database=zuchefw;" +
            "User ID=xieaoran@kmri1lx01x;" + "Password=ilovewindows8*;" +
            "Trusted_Connection=False;Encrypt=True;" +
            "MultipleActiveResultSets=True;";

        private static SqlConnection SQLConnection = new SqlConnection(ConnectionString);

        public static void Connect()
        {
            SQLConnection.Open();
        }
        public static void Close()
        {
            SQLConnection.Close();
        }
        #region 填充GUI
        public static void ReadOrders(DataGridView view)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText =
                "select ID,Condition,AliPayNumber,DateTimeCreated,DateTimeStart,NeedDriver,NeedManualConfirm,Note,Car_ID,Comment_ID,Store_ID,User_ID from Orders";

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();

            view.Rows.Clear();
            while (sqlDataReader.Read())
            {
                view.Rows.Add(
                    sqlDataReader["ID"],
                    sqlDataReader["Condition"],
                    sqlDataReader["DateTimeCreated"],
                    sqlDataReader["DateTimeStart"],
                    sqlDataReader["AliPayNumber"],
                    sqlDataReader["NeedDriver"],
                    sqlDataReader["NeedManualConfirm"],
                    sqlDataReader["Note"],
                    sqlDataReader["User_ID"],
                    sqlDataReader["Car_ID"],
                    sqlDataReader["Store_ID"],
                    sqlDataReader["Comment_ID"]);
            }
            sqlDataReader.Close();
            view.AutoResizeColumns();
        }

        public static void ReadCars(DataGridView view)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText =
                "select ID,Enabled,Name,BasicInfo from Cars";

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();

            view.Rows.Clear();
            while (sqlDataReader.Read())
            {
                view.Rows.Add(
                    sqlDataReader["ID"],
                    sqlDataReader["Name"],
                    sqlDataReader["Enabled"],
                    sqlDataReader["BasicInfo"]);
            }
            sqlDataReader.Close();
            view.AutoResizeColumns();
        }

        public static void ReadActivities(DataGridView view)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText =
                "select ID,Name,BasicInfo from Activities";

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();

            view.Rows.Clear();
            while (sqlDataReader.Read())
            {
                view.Rows.Add(
                    sqlDataReader["ID"],
                    sqlDataReader["Name"],
                    sqlDataReader["BasicInfo"]);
            }
            sqlDataReader.Close();
            view.AutoResizeColumns();
        }

        public static void ReadPlaneOrders(DataGridView view)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText =
                "select ID,Condition,DateTimeCreated,DateTimePlane,Note,Airport_ID,User_ID from PlaneOrders";

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();

            view.Rows.Clear();
            while (sqlDataReader.Read())
            {
                view.Rows.Add(
                    sqlDataReader["ID"],
                    sqlDataReader["Condition"],
                    sqlDataReader["DateTimeCreated"],
                    sqlDataReader["DateTimePlane"],
                    sqlDataReader["Note"],
                    sqlDataReader["Airport_ID"],
                    sqlDataReader["User_ID"]);
            }
            view.AutoResizeColumns();
        }
        #endregion

        #region 单条数据读取
        public static string ReadAirport(string id)
        {
            return id == "0" ? "机场A" : "机场B";
        }
        public static string ReadUser(string id)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText = "select Name,CellPhoneNumber,Email from Users where ID=" + id;

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            return string.Format("姓名：{0}\r\n手机号：{1}\r\n电子邮箱：{2}",
                sqlDataReader["Name"],
                sqlDataReader["CellPhoneNumber"],
                sqlDataReader["Email"]);
        }

        public static string ReadCar(string id)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText = "select Name,Price,Deposit from Cars where ID=" + id;

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            return string.Format("车型名：{0}\r\n日租金：{1}\r\n定金：{2}",
                sqlDataReader["Name"],
                sqlDataReader["Price"],
                sqlDataReader["Deposit"]);
        }
        public static string ReadComment(string id)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText = "select Type,DateCreated,Rating,Content from Comments where ID=" + id;

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            return string.Format("评价类型：{0}\r\n评价生成日期：{1}\r\n车型评分（星）：{2}\r\n评价内容：{3}",
                GetCommentType(sqlDataReader["Type"].ToString()),
                sqlDataReader["DateCreated"],
                sqlDataReader["Rating"],
                sqlDataReader["Content"]);

        }

        public static string ReadOrder(string id)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText =
                "select AliPayNumber,Condition,DateTimeCreated,DateTimeStart,NeedDriver,NeedManualConfirm,Note from Orders where ID=" +
                id;

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            return string.Format("订单号：{0}\r\n订单状态：{1}\r\n订单创建日期：{2}\r\n预约取车日期：{3}\r\n需要司机：{4}\r\n人工确认：{5}\r\n备注：{6}",
                sqlDataReader["AliPayNumber"],
                GetOrderCondition(sqlDataReader["Condition"].ToString()),
                sqlDataReader["DateTimeCreated"],
                sqlDataReader["DateTimeStart"],
                sqlDataReader["NeedDriver"],
                sqlDataReader["NeedManualConfirm"],
                sqlDataReader["Note"]);
        }

        public static string ReadFlightOrder(string id)
        {
            var sqlCommand = SQLConnection.CreateCommand();

            sqlCommand.CommandText = "select DateTimeCreated,DateTimePlane,Note from PlaneOrders where ID=" + id;

            sqlCommand.ExecuteNonQuery();
            var sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            return string.Format("订单创建日期：{0}\r\n预约接机日期：{1}\r\n订单备注：{2}",
                sqlDataReader["DateTimeCreated"],
                sqlDataReader["DateTimePlane"],
                sqlDataReader["Note"]);
        }
        #endregion
        public static void InsertData(string sheet, params string[] value)
        {
            var sqlCommand = SQLConnection.CreateCommand();
            string tempText = "insert into " + sheet + " values (N'" + string.Join("',N'", value) + "')";
            tempText = Regex.Replace(tempText, "N'true'", "1");
            sqlCommand.CommandText = tempText;
            sqlCommand.ExecuteNonQuery();
        }

        public static void DeleteData(string sheet, string col, string value)
        {
            var sqlCommand = SQLConnection.CreateCommand();
            string tempText = "delete from dbo." + sheet + " where " + col + " = " + value;
            sqlCommand.CommandText = tempText;
            sqlCommand.ExecuteNonQuery();
        }

        public static void UpdateData(string sheet, string col1, string value1, string col2, string value2)
        {
            var sqlCommand = SQLConnection.CreateCommand();
            string tempText = "update " + sheet + " set " + col1 + " = " + value1 + " where " + col2 + " = " + value2;
            sqlCommand.CommandText = tempText;
            sqlCommand.ExecuteNonQuery();
        }
        #region 一些枚举
        private static string GetCommentType(string type)
        {
            switch (type)
            {
                case "0":
                    return "好评";
                case "1":
                    return "中评";
                case "2":
                    return "差评";
                default:
                    return "未知评价类型";
            }
        }

        private static string GetOrderCondition(string type)
        {
            switch (type)
            {
                case "0":
                    return "等待支付";
                case "1":
                    return "支付完毕，等待确认";
                case "2":
                    return "确认完毕，等待取车";
                case "3":
                    return "取车完毕，等待还车";
                case "4":
                    return "还车完毕，订单完成";
                case "5":
                    return "已取消";
                default:
                    return "未知订单状态";
            }
        }
        #endregion
    }
}