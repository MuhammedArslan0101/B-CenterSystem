using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace isMerkeziSistemi.Entities
{
    public class FooterLink
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string facebook { get; set; }
        public string youtube { get; set; }
        public string instagram { get; set; }
        public string twitter { get; set; }
        public string googleMap { get; set; }
    }
}