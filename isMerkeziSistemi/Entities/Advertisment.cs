using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace isMerkeziSistemi.Entities
{
    public class Advertisment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string JobName { get; set; }
        public string Category { get; set; }
        public string Tip { get; set; }
        public string Image { get; set; }
        public int maas { get; set; }
        public string City { get; set; }


    }
}