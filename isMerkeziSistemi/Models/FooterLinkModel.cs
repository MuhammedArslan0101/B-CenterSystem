using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using isMerkeziSistemi.Entities;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace isMerkeziSistemi.Models
{
    public class FooterLinkModel
    {
        private MongoClient mongoClient;
        private IMongoCollection<FooterLink> linkCollection;

        public FooterLinkModel()
        {
            mongoClient = new MongoClient(ConfigurationManager.AppSettings["mongoDBHost"]);
            var db = mongoClient.GetDatabase(ConfigurationManager.AppSettings["mongoDBName"]);
            linkCollection = db.GetCollection<FooterLink>("footerlink");

        }

        public List<FooterLink> findAll()
        {
            return linkCollection.AsQueryable<FooterLink>().ToList();

        }

        public FooterLink find(string id)
        {
            var cId = new ObjectId(id);
            return linkCollection.AsQueryable<FooterLink>().SingleOrDefault(a => a.Id == cId);



        }

        public void create(FooterLink c)
        {
            linkCollection.InsertOne(c);

        }

        public void update(FooterLink c)
        {
            linkCollection.UpdateOne(
                Builders<FooterLink>.Filter.Eq("_id", c.Id),
                Builders<FooterLink>.Update
                .Set("facebook", c.facebook)
                .Set("instagram", c.instagram)
                 .Set("twitter", c.twitter)
                  .Set("youtube", c.youtube)
                   .Set("googleMap", c.googleMap)
                );

        }

        public void delete(string id)
        {
            linkCollection.DeleteOne(Builders<FooterLink>.Filter.Eq("_id",
                ObjectId.Parse(id)));
        }
    }
}