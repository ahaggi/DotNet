using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PatientData.Models {
    public class Patient {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Medicaion> Medicaions { get; set; }
        public ICollection<Ailment> Ailments { get; set; }

    }

    public class Medicaion {
        public string Name { get; set; }
        public int Doses { get; set; }

    }

    public class Ailment {
        public string Name { get; set; }
    }


}