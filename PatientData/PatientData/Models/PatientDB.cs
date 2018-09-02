using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PatientData.Models;
using MongoDB.Bson;

namespace PatientData.Models {
    public static class PatientDB {
        static MongoClient client = new MongoClient("mongodb://localhost");
        static IMongoDatabase db = client.GetDatabase("patient_data_db");
        static IMongoCollection<Patient> _patients = db.GetCollection<Patient>("Patients");

        public static IEnumerable<Patient> GetPatients() {
            return _patients.Find(new BsonDocument()).ToEnumerable();

        }

        public static void PutPatients(IEnumerable<Patient> patients) {
            IMongoCollection<Patient> _patients = db.GetCollection<Patient>("Patients");
            _patients.InsertMany(patients);

        }



        public static Patient GetPatientByID(string id) {
            return _patients.Find(x => x.Id == id).FirstOrDefault();
        }

        public static Patient GetPatientByI2D(string id) {
            return _patients.Find(x => x.Id == id).FirstOrDefault();
        }



    }
}