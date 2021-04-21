using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Grpc.Core;
using isMerkeziSistemi.Entities;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace isMerkeziSistemi.Models
{
    public class AdvModel
    {
        private MongoClient mongoClient;
        private IMongoCollection<Advertisment> advCollection;

        public AdvModel()
        {
            mongoClient = new MongoClient(ConfigurationManager.AppSettings["mongoDBHost"]);
            var db = mongoClient.GetDatabase(ConfigurationManager.AppSettings["mongoDBName"]);
            advCollection = db.GetCollection<Advertisment>("jobs");

        }

        public List<Advertisment> findAll()
        {
            return advCollection.AsQueryable<Advertisment>().ToList();

        }

        public Advertisment find(string id)
        {
            var adId = new ObjectId(id);
            return advCollection.AsQueryable<Advertisment>().SingleOrDefault(a => a.Id == adId);



        }

        public void delete(string id)
        {
            advCollection.DeleteOne(Builders<Advertisment>.Filter.Eq("_id",
                ObjectId.Parse(id)));
        }
        public void create(Advertisment a )
        {
            advCollection.InsertOne(a);

        }
    }

    
    }
