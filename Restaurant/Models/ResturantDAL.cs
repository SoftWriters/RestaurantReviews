using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.Http;

namespace Restaurant.Models
{
    public class ResturantDAL
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["ESICINS96"].ConnectionString);

        public Response GetRestaurants(Request obj)
        {
            Response objRest = new Response();
            try
            {
                MySqlCommand cmd = new MySqlCommand("pr_getResturants", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("V_City", obj.CityId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRest.dt = dt;
                objRest.ResponseCode = "1000";
                objRest.ResponseMessage = "Success";            

            }
            catch (Exception ex)
            {
                objRest.ResponseCode = "1001";
                objRest.ResponseMessage = "No Data Found";
            }
            return objRest;
        }

        public Response SaveRestaurants(SaveRest obj)
        {
            Response objRest = new Response();
            try
            {
                MySqlCommand cmd = new MySqlCommand("pr_SaveResturants", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("V_ResturantName", obj.ResturantName);
                cmd.Parameters.AddWithValue("V_Address", obj.Address);
                cmd.Parameters.AddWithValue("V_City", obj.CityId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objRest.ResponseCode = "1002";
                    objRest.ResponseMessage = "Success";
                }
                else
                {
                    objRest.ResponseCode = "1003";
                    objRest.ResponseMessage = "Insertion Failed";
                }

            }
            catch (Exception ex)
            {
                objRest.ResponseCode = "1003";
                objRest.ResponseMessage = "Insertion Failed";
            }
            return objRest;
        }
    }
}