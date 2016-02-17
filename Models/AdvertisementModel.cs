using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Advertisement.Models
{
    public class AdvertisementModel
    {
        // Строка соединения
        private const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Advert1.mdf';Integrated Security=True;";
        /// <summary>
        /// Выбор всех реклам с БД
        /// </summary>
        /// <returns></returns>
        public static List<Advertisement> GetAllAdverts()
        {
            List<Advertisement> _lst = new List<Advertisement>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand("SELECT Id, Description, MaintenanceTime, Location.ToString() as Location, Type, Height, Width, MonthlyCost FROM Advertisement", con);

                //Открываем соединение с БД
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    foreach (DbDataRecord result in dr)
                    {
                        // Записываем в классы все рекламы
                        Advertisement adv = new Advertisement();

                        adv.Id = result.GetInt32(result.GetOrdinal("Id"));
                        adv.Description = result.GetString(result.GetOrdinal("Description"));
                        adv.MaintenanceTime = result.GetDateTime(result.GetOrdinal("MaintenanceTime"));
                        adv.Location = result.GetString(result.GetOrdinal("Location"));
                        adv.Type = result.GetString(result.GetOrdinal("Type"));
                        adv.Height = (float)result.GetDecimal(result.GetOrdinal("Height"));
                        adv.Width = (float)result.GetDecimal(result.GetOrdinal("Width"));
                        adv.MonthlyCost = (float)result.GetDecimal(result.GetOrdinal("MonthlyCost"));
                        _lst.Add(adv);
                    }
                }
            }
            return _lst;
        }

        /// <summary>
        /// Вставка новой рекламы
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        public static bool SetNewAdvert(Advertisement advertisement, string lat, string lng)
        {
            bool flag = false;
            string _location = "POINT (" + lat + " " + lng + " 12)";
            string query =
                string.Format(
                    "INSERT INTO Advertisement (Description, MaintenanceTime, Location, Type, Height, Width, MonthlyCost) VALUES (N'{0}', " +
                    "convert(datetime, '{1}', 103), '{2}',N'{3}',{4},{5},{6})",
                    advertisement.Description, advertisement.MaintenanceTime, _location,
                    advertisement.Type,
                    advertisement.Height.ToString("0.00").Replace(",", "."), advertisement.Width.ToString("0.00").Replace(",", "."), advertisement.MonthlyCost.ToString("0.00").Replace(",", "."));

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(query, con);
                con.Open();
                if (com.ExecuteNonQuery() == 1)
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// Удаление рекламы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteAdvert(int? id)
        {
            bool flag = false;

            string query =
                string.Format(
                    "DELETE FROM Advertisement WHERE Id = {0}", id);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(query, con);
                con.Open();
                var lines = com.ExecuteNonQuery();
            }
            return flag;
        }

        /// <summary>
        /// Поиск рекламы по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Advertisement FindAdvert(int? id)
        {
            Advertisement adv = new Advertisement();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query =
                    String.Format(
                        "SELECT Description, MaintenanceTime, Location.ToString() as Location, Type, Height, Width, MonthlyCost FROM Advertisement WHERE Id = {0}",
                        id);
                SqlCommand com = new SqlCommand(query, con);

                //Открываем соединение с БД
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    foreach (DbDataRecord result in dr)
                    {
                        // Записываем в классы все рекламы
                        adv.Id = Convert.ToInt32(id);
                        adv.Description = result.GetString(result.GetOrdinal("Description"));
                        adv.MaintenanceTime = result.GetDateTime(result.GetOrdinal("MaintenanceTime"));
                        adv.Location = result.GetString(result.GetOrdinal("Location"));
                        adv.Type = result.GetString(result.GetOrdinal("Type"));
                        adv.Height = (float)result.GetDecimal(result.GetOrdinal("Height"));
                        adv.Width = (float)result.GetDecimal(result.GetOrdinal("Width"));
                        adv.MonthlyCost = (float)result.GetDecimal(result.GetOrdinal("MonthlyCost"));
                    }
                }
            }
            return adv;
        }
        /// <summary>
        /// Редактирование реклам
        /// </summary>
        /// <param name="id"></param>
        /// <param name="advertisement"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static bool UpdateAdvert(int id, Advertisement advertisement, string lat, string lng)
        {
            bool flag = false;
            string _location = "POINT (" + lat + " " + lng + " 12)";
            string query =
                string.Format(
                    "UPDATE Advertisement " +
                    "SET Description = N'{0}', " +
                        "MaintenanceTime = convert(datetime, '{1}', 103)," +
                        "Location = '{2}'," +
                        "Type = N'{3}'," +
                        "Height = {4}," +
                        "Width = {5}," +
                        "MonthlyCost = {6}" +
                    "WHERE Id = {7}", advertisement.Description, advertisement.MaintenanceTime, _location,
                    advertisement.Type,
                    advertisement.Height.ToString("0.00").Replace(",", "."),
                    advertisement.Width.ToString("0.00").Replace(",", "."),
                    advertisement.MonthlyCost.ToString("0.00").Replace(",", "."), id);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(query, con);
                con.Open();
                if (com.ExecuteNonQuery() == 1)
                {
                    flag = true;
                }
            }
            return flag;
        }
    }
}