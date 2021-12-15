using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicine.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string idp)
        {
            var h = idp;
            List<string> idpats = new List<string>() { "1111", "5555" };
            if (idpats.Contains(idp))
            {
                return View("IndexPo");
            }
            else
                return RedirectToAction("Index", "Patient");
        }


        public ActionResult Prescription(string idp)
        {
            PatientBL BL = new PatientBL();
            List<Prescription> l = BL.GetPatientPrescription(int.Parse(idp));
            
            return View(l);
        }
        
    }
}
