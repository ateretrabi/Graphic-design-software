using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicine.Models
{
    public class PatientModel
    {
         
        public List<Patient> GetPatients()
        {

            PatientBL BL = new PatientBL();
            return BL.GetAllPatients();
        }

        public Patient GetPatient(int id)
        {
            PatientBL BL = new PatientBL();
            return BL.GetPatient(id);
        }

        public void EditPatient(Patient p)
        {
            PatientBL BL = new PatientBL();
            BL.UpdatePatient(p);
        }

        public void AddPatient(Patient p)
        {
            PatientBL BL = new PatientBL();
            BL.AddPatient(p);
        }
    }
}