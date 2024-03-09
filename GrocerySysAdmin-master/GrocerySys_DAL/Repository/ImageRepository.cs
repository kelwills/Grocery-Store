using GrocerySys_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GrocerySys_DAL.Repository
{
    public class ImageRepository
    {
        public void insertImages(ImageModel IMG)
        {
            using(SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_insertImages", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", IMG.ImgID);
                cmd.Parameters.AddWithValue("@name", IMG.ImgName);
                cmd.Parameters.AddWithValue("@desc", IMG.ImgDesc);
                cmd.Parameters.AddWithValue("@data", IMG.ImgData);
                cmd.ExecuteNonQuery();
            }
        }


        public void deleteImages(string ImgID)
        {
            using (SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_deleteImages", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ImgID);
                cmd.ExecuteNonQuery();
            }
        }


        public List<ImageModel> getImages()
        {
            List<ImageModel> ImageList = new List<ImageModel>();
            using (SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_getImages", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using(SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {
                        ImageModel IMG = new ImageModel();
                        IMG.ImgID = SDR["ImgID"].ToString();
                        IMG.ImgName = SDR["ImgName"].ToString();
                        IMG.ImgDesc = SDR["ImgDesc"].ToString();
                        IMG.ImgData = (byte[])SDR["ImgData"];
                        ImageList.Add(IMG);
                    }
                }
            }
            return ImageList;
        }
    }
}