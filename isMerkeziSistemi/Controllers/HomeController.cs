using isMerkeziSistemi.Entities;
using isMerkeziSistemi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace isMerkeziSistemi.Controllers
{
    public class HomeController : Controller
    {
        private MongoClient mongoClient;
        private IMongoCollection<Advertisment> advCollection;

        public HomeController()
        {
            mongoClient = new MongoClient(ConfigurationManager.AppSettings["mongoDBHost"]);
            var db = mongoClient.GetDatabase(ConfigurationManager.AppSettings["mongoDBName"]);
            advCollection = db.GetCollection<Advertisment>("jobs");

        }

        public ActionResult Filter(int? min, int? max , string category , string city , string tip )
        {
            var filter = advCollection.AsQueryable<Advertisment>();
            filter = (MongoDB.Driver.Linq.IMongoQueryable<Advertisment>)filter.Where(
                x => x.maas >= min || x.maas <= max
                || x.Category == category
                || x.Tip == tip
                || x.City == city);

            return View(filter);

        }
          

        public ActionResult Search(string s)
        {
            var search = advCollection.AsQueryable<Advertisment>() ;
            
            if (!string.IsNullOrEmpty(s))
            {
                search = (MongoDB.Driver.Linq.IMongoQueryable<Advertisment>)search.Where(i => i.JobName.Contains(s) || i.Category.Contains(s)
                 || i.City.Contains(s) || i.Tip.Contains(s));
            }

            return View(search.ToList());
        }

        public ActionResult UserReg()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("deneme53169@gmail.com", "deneme12345");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("deneme53169@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "Sender Name: " + contact.Name + "<br>" +
                            "Email: " + contact.Email + "<br>" +
                            "subject: " + contact.Subject + "<br>" +
                            "Text : <b>" + contact.Message + "</b>";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");

            
        }
    }
}