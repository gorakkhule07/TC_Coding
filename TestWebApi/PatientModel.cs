using iMedOneDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi
{
    public class PatientModel
    {
        public TBLPATIENT patient;

        public IEnumerable<Tblstate> states;

        public IEnumerable<Tblcity> cities;

        public int stateid { get; set; }

        public bool Result { get; set; }
        public List<string> UserMessages { get; set; }
    }
}
