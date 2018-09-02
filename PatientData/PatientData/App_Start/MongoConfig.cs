using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PatientData.Models;
using MongoDB.Driver;

namespace PatientData {
    public static class MongoConfig {
        public static void seed() {
            var patients = PatientDB.GetPatients();
            if (!patients.AsQueryable().Any(p => p.Name == "noe")) {

                var _ailments = new List<Ailment>();
                for (int i = 0; i < 3; i++)
                    _ailments.Add(new Ailment { Name = "ailments" + i });

                var _medications = new List<Medicaion>();
                for (int i = 0; i < 3; i++)
                    _medications.Add(new Medicaion {
                        Name = "medication" + i,
                        Doses = i
                    });

                var data = new List<Patient>();
                for (int i = 0; i < 5; i++) {
                    var patient = new Patient {
                        Name = "name" + i,
                        Ailments = _ailments,
                        Medicaions = _medications
                    };
                    data.Add(patient);
                }
                PatientDB.PutPatients(data);
            }

        }
    }
}