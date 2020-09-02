using iMedOneDB;
using iMedOneDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestWebApi
{
    public class PatientBAL
    {
        List<String> userMessages = new List<string>();

        public static string NameValidationShouldBeAlphabets = "Name should be alphabets.";
        public static string DOBValidationShouldBeGreaterThanTodays = "Birth date should not be greater than today’s date.";
        public static string DOBValidationShouldNotBelessThan100Years = "Birth date should not be less than 100 years.";
        public static string SurNameValidationShouldBeAlphabets = "Surname should be alphabets.";
        public static string PatientAlreadyRegisterdValidationShouldBeUinque = "Patient already registered.";

        public List<String> ValidatePatient(TBLPATIENT patient)
        {
            Regex r = new Regex("^[a-zA-Z ]+$");

            if (patient.DOB >= DateTime.Now)
            {
                userMessages.Add(DOBValidationShouldBeGreaterThanTodays);
            }
            else if (patient.DOB.Year < DateTime.Now.Year - 100)
            {
                userMessages.Add(DOBValidationShouldNotBelessThan100Years);
            }
            if (!r.IsMatch(patient.Name))
            {
                userMessages.Add(NameValidationShouldBeAlphabets);
            }
            if (!r.IsMatch(patient.SurName))
            {
                userMessages.Add(SurNameValidationShouldBeAlphabets);
            }
            // check unique patient
            var result = from p in DBContext.GetData<TBLPATIENT>()
                         where p.Name.ToLower() == patient.Name.ToLower() &&
                         p.SurName.ToLower() == patient.SurName.ToLower() &&
                         p.DOB == patient.DOB &&
                         p.Gender == patient.Gender &&
                         p.Id != patient.Id
                         select p;
            if (result != null && result.Count() > 0)
            {
                userMessages.Add(PatientAlreadyRegisterdValidationShouldBeUinque);
            }

            return userMessages;
        }
    }

}
