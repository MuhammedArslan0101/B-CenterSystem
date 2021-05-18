using isMerkeziSistemi.Entities;
using isMerkeziSistemi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isMerkeziSistemi.Controllers
{
    public class AdvController : Controller
    {
        private AdvModel advModel = new AdvModel();
        private CategoryModel categoryModel = new CategoryModel();
        // GET: Category
        public ActionResult Index()
        {
            ViewBag.adv = advModel.findAll();
            return View();
        }

        public PartialViewResult advPartial()
        {
            ViewBag.adv = advModel.findAll().OrderByDescending(i => i.Id);

            return PartialView("advPartial");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            ViewBag.category = categoryModel.findAll();

            return View("Add", new Advertisment());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(Advertisment a, HttpPostedFileBase file)
        {
            string path = Path.Combine("~/Content/images", file.FileName);
            file.SaveAs(Server.MapPath(path));
            a.Image = file.FileName.ToString();

            advModel.create(a);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Delete(string id)
        {
            advModel.delete(id);
            return RedirectToAction("Index");

        }

       
    }
}