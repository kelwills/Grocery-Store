using GrocerySys_DAL.Models;
using GrocerySys_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GrocerySys_WebApi.Controllers
{
    public class ImageApiController : ApiController
    {
        ImageRepository ImgREPO = new ImageRepository();

        public void insertImages(ImageModel IMG)
        {
            ImgREPO.insertImages(IMG);
        }

        public void deleteImages(string ImgID)
        {
            ImgREPO.deleteImages(ImgID);
        }

        public List<ImageModel> getImages()
        {
            return ImgREPO.getImages();
        }
    }
}
