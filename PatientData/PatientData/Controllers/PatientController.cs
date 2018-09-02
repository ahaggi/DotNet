using MongoDB.Bson;
using MongoDB.Driver;
using PatientData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatientData.Controllers
{
    public class PatientController : ApiController{

        
        public PatientController() {

        }

        public IEnumerable<Patient> Get() {
            return PatientDB.GetPatients();
        }

        //public HttpResponseMessage Get(string id) { //xx
        //    Patient patient = PatientDB.GetPatientByID(id);
        //    if (patient == null) {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found");
        //    }
        //    return Request.CreateResponse(patient);
        //}
        public IHttpActionResult Get(string id) {
            Patient patient = PatientDB.GetPatientByID(id);
            if (patient == null) {
                return NotFound();
            }
            return Ok(patient);
        }


        //[Route("api/patient/{id}/medications")]
        //public HttpResponseMessage GetMedications(string id) {
        //    Patient patient = PatientDB.GetPatientByID(id);
        //    if (patient == null) {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found");
        //    }
        //    return Request.CreateResponse(patient.Medicaions);
        //}

        //[Route("api/patient/{id:int}/medications")]
        [Route("api/patient/{id}/medications")]
        public IHttpActionResult GetMedications(string id) {
            Patient patient = PatientDB.GetPatientByID(id);
            if (patient == null) {
                return NotFound();
            }
            return Ok(patient.Medicaions);
        }

    }
}
