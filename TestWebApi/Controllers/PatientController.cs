using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMedOneDB;
using iMedOneDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestWebApi.Controllers
{
    [Produces("application/json")]
    public class PatientController : Controller
    {
        PatientBAL patientBo = new PatientBAL();

        
        [Route("api/Patient/GetAllPatients")]
        [HttpGet]
        public IEnumerable<TBLPATIENT> Get()
        {
            IEnumerable<TBLPATIENT> LstPatients = null;
            try
            {
                LstPatients = DBContext.GetData<TBLPATIENT>();
            }
            catch (Exception ex)
            {
                //write looger
            }
            return LstPatients;
        }

        
        [Route("api/Patient/GetPatient")]
        [HttpGet]
        public PatientModel GetPatient(int id)
        {
            PatientModel tblpatient = new PatientModel();
            IEnumerable<Tblcity> tblCity = DBContext.GetData<Tblcity>();

            var patient = from p in DBContext.GetData<TBLPATIENT>()
                          where p.Id == id
                          select p;

            if (patient != null)
            {
                tblpatient.patient = patient.FirstOrDefault();
                tblpatient.states = DBContext.GetData<Tblstate>();

                var states = from c in tblCity
                             where c.Id == tblpatient.patient.CityId
                             select c;

                tblpatient.stateid = states != null ? states.FirstOrDefault().StateId : 0;

                tblpatient.cities = from c in tblCity
                                    where c.StateId == tblpatient.stateid
                                    select c;

                return tblpatient;
            }
            else
            {
                return null;
            }
        }

       
        [HttpPost]
        [Route("api/Patient/RegisterPatient")]
        public PatientModel SavePatient([FromBody]PatientModel patientModel)
        {

            patientModel.UserMessages = patientBo.ValidatePatient(patientModel.patient);
            List<TBLPATIENT> tblPatients = new List<TBLPATIENT>();

            try
            {
                if (patientModel.UserMessages.Count() == 0)
                {

                    //update 
                    tblPatients.Add(patientModel.patient);
                    patientModel.Result = DBContext.SaveAll<TBLPATIENT>(tblPatients);

                }
            }
            catch (Exception ex)
            {
                //Write loggers
            }
            return patientModel;
        }

        
        [Route("api/Patient/GetStates")]
        [HttpGet]
        public IEnumerable<Tblstate> GetStates()
        {
            IEnumerable<Tblstate> LstStates = null;
            try
            {
                LstStates = DBContext.GetData<Tblstate>();
            }
            catch (Exception ex)
            {
                //write looger
            }
            return LstStates;
        }

        [Route("api/Patient/GetCitiesByStateId")]
        [HttpGet]
        public IEnumerable<Tblcity> GetCitiesByStateId(int stateid)
        {
            IEnumerable<Tblcity> LstCities = null;
            try
            {
                LstCities = from c in DBContext.GetData<Tblcity>()
                            where c.StateId == stateid
                            select c;
            }
            catch (Exception ex)
            {
                //write looger
            }
            return LstCities;
        }

    }
}
