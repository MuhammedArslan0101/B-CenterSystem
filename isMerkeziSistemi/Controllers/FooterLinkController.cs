using isMerkeziSistemi.Entities;
using isMerkeziSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isMerkeziSistemi.Controllers
{
    public class FooterLinkController : Controller
    {
        private FooterLinkModel footerLinkModel = new FooterLinkModel();

        // GET: FooterLink
        public ActionResult Index()
        {
            ViewBag.footerlink = footerLinkModel.findAll();

            return View();
        }

        public PartialViewResult footerPartial()
        {
            ViewBag.footerlink = footerLinkModel.findAll();

            return PartialView();
        }
        [HttpGet]
        public ActionResult Add()
        {

            return View("Add", new FooterLink());
        }

        [HttpPost]
        public ActionResult Add(FooterLink c)
        {

            footerLinkModel.create(c);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            footerLinkModel.delete(id);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(string id)
        {

            return View("Edit", footerLinkModel.find(id));
        }

        [HttpPost]
        public ActionResult Edit(FooterLink c, FormCollection fc)
        {
            string id = fc["Id"];
            var f = footerLinkModel.find(id);

            f.facebook = c.facebook;
            f.twitter = c.twitter;

            f.instagram = c.instagram;
            f.youtube = c.youtube;
            f.googleMap = c.googleMap;

            footerLinkModel.update(f);
            return RedirectToAction("Index");


        }
    }
}