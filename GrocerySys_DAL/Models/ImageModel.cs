using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrocerySys_DAL.Models
{
    public class ImageModel
    {
        public string ImgID { get; set; }
        public string ImgName { get; set; }
        public string ImgDesc { get; set; }
        public byte[] ImgData { get; set; }
    }
}