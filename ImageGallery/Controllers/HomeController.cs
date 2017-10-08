using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ImageGallery.Models;

namespace ImageGallery.Controllers
{
    public class HomeController : Controller
    {

        private int pageSize = 4;

        public ActionResult Index(int page = 1)
        {
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/Content/Images/"));
            FileInfo[] files = directory.GetFiles("*.jpg");

            if ((page - 1) * pageSize > files.Length || page < 1) return RedirectToAction("Index", 1);

            var viewModel = new ImageListViewModel
            {
                ImageNames = files.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.Name),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = files.Length
                }
            };

            return View(viewModel);
        }

        public PartialViewResult Gallery(int page = 1)
        {
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/Content/Images/"));
            FileInfo[] files = directory.GetFiles("*.jpg");

            if ((page - 1) * pageSize >= files.Length || page < 1) page = 1;

            var viewModel = new ImageListViewModel
            {
                ImageNames = files.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => x.Name),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = files.Length
                }
            };

            return PartialView(viewModel);
        }
        
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid && upload != null)
            {
                upload.SaveAs(Server.MapPath("~/Content/Images/" + upload.FileName));
                var image = new WebImage(upload.InputStream);
                image.Resize(500, 281, true, true);
                image.Save(Server.MapPath("~/Content/ImagesSmall/" + upload.FileName), "jpg");

                TempData["message"] = $"{upload.FileName} has been saved";
                return RedirectToAction("Upload");
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}