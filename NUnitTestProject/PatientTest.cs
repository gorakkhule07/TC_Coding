using iMedOneDB.Models;
using NUnit.Framework;
using System;
using TestWebApi;

namespace NUnitTestProject
{
    [TestFixture]
    public class PatientTest
    {
        TBLPATIENT patient = new TBLPATIENT();
        PatientBAL patientBo = new PatientBAL();
        [SetUp]
        public void Setup()
        {

        }

        [TestCase]
        public void When_NameNotAlphabets_Expect_onlycharacters()
        {
            //assign
            patient.Name = "gorak 01%&*99()#$";
            patient.SurName = "khule";
            patient.DOB = DateTime.Now;

            //act
            var result = patientBo.ValidatePatient(patient);

            //Assert
            if (result != null && result.Count > 0)
            {
                Assert.AreEqual(PatientBAL.NameValidationShouldBeAlphabets, result[0]);
            }
            else
            {
                //failed
                Assert.Fail();
            }
        }

        [TestCase]
        public void When_SurnameNotAlphabets_Expect_onlycharacters()
        {
            //assign
            patient.Name = "gorak";
            patient.SurName = "khule01%&";
            patient.DOB = DateTime.Now;

            //act
            var result = patientBo.ValidatePatient(patient);

            //Assert
            if (result != null && result.Count > 0)
            {
                Assert.AreEqual(PatientBAL.SurNameValidationShouldBeAlphabets, result[0]);
            }
            else
            {
                //failed
                Assert.Fail();
            }
        }

        [TestCase]
        public void When_DOBNotLessThan100Years_Expect_greaterThan100year()
        {
            //assign
            patient.Name = "gorak";
            patient.SurName = "khule";
            patient.DOB = DateTime.Now.AddYears(-110);

            //act
            var result = patientBo.ValidatePatient(patient);

            //Assert
            if (result != null && result.Count > 0)
            {
                Assert.AreEqual(PatientBAL.DOBValidationShouldNotBelessThan100Years, result[0]);
            }
            else
            {
                //failed
                Assert.Fail();
            }
        }

        [TestCase]
        public void When_DOBNotNotGreaterThantodays_Expect_greaterThanTodays()
        {
            //assign
            patient.Name = "gorak";
            patient.SurName = "khule";
            patient.DOB = DateTime.Now.AddDays(1);

            //act
            var result = patientBo.ValidatePatient(patient);

            //Assert
            if (result != null && result.Count > 0)
            {
                Assert.AreEqual(PatientBAL.DOBValidationShouldBeGreaterThanTodays, result[0]);
            }
            else
            {
                //failed
                Assert.Fail();
            }
        }
    }
}