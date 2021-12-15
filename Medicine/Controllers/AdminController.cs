using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BE;
using BL;
using Medicine.Models;

namespace Medicine.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LogIn(FormCollection collection)
        {
            try
            {
                Passwordmodel passwordmodel = new Passwordmodel();
                passwordmodel.CheckAdminPassword(int.Parse(collection["IdAdmin"]), collection["PassName"]);
                if (passwordmodel.CheckAdminPassword(int.Parse(collection["IdAdmin"]), collection["PassName"]))
                {
                   
                    return RedirectToAction("AdminOps");
                }
            }
            catch(Exception e)
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
                // return JavaScript(alert("Hello this is an alert"));
                 return RedirectToAction("ExeptionHandler", new { s=e.Message});
            }
            return RedirectToAction("Error");
        }
        public ActionResult AdminOps()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        } 
    
        public ActionResult AddPatient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPatient(FormCollection collection)
        {
            Patient p = new Patient() { FirstName = collection["FirstName"], LastName = collection["LastName"], Id = int.Parse(collection["Id"]), Address = collection["Address"], dateOfBirth = DateTime.Parse(collection["dateOfBirth"]) };
            PatientModel model = new PatientModel();
            model.AddPatient(p);
            return RedirectToAction("AdminOps");
        }

        public ActionResult AddDoctor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDoctor(FormCollection collection)
        {
            Doctor d = new Doctor() { FirstName = collection["FirstName"], LastName = collection["LastName"], Id = int.Parse(collection["Id"]), LicenseNumber = collection["LicenseNumber"], Address = collection["Address"], dateOfBirth = DateTime.Parse(collection["dateOfBirth"]) };
            DoctorModel model = new DoctorModel();
            model.AddDoctor(d);
            Passwordmodel passwordmodel = new Passwordmodel();
            UserPassword user = new UserPassword() { Id = int.Parse(collection["Id"]), Password = collection["DoctorPassword"] };
            passwordmodel.AddPassword(user);
            return RedirectToAction("AdminOps");
        }
        public ActionResult viewDoctors()
        {
            DoctorModel model = new DoctorModel();
            return View(model.GetDoctors());
        }
        public ActionResult EditDoctor(int id)
        {
            DoctorModel model = new DoctorModel();
            Doctor d = model.GetDoctor(id);
            return View(d);
        }
        [HttpPost]
        public ActionResult EditDoctor(int id, FormCollection collection)
        {
            DoctorModel model = new DoctorModel();

            Doctor d = new Doctor() { FirstName = collection["FirstName"], LastName = collection["LastName"], LicenseNumber = collection["LicenseNumber"], Id = id, Address = collection["Address"], dateOfBirth = DateTime.Parse(collection["dateOfBirth"]) };
            model.EditDoctor( d);
            return RedirectToAction("viewDoctors");
        }

        public ActionResult viewPatients()
        {
            PatientModel model = new PatientModel();

            return View(model.GetPatients());
        }
        public ActionResult EditPatient(int id)
        {
            PatientModel model = new PatientModel();
            Patient p = model.GetPatient(id);
            return View(p);
        }
        [HttpPost]
        public ActionResult EditPatient(int id, FormCollection collection)
        {
            PatientModel model = new PatientModel();
            Patient p = new Patient() { FirstName = collection["FirstName"], LastName = collection["LastName"], Id = id, Address = collection["Address"], dateOfBirth = DateTime.Parse(collection["dateOfBirth"]) };
            model.EditPatient(p);
            return RedirectToAction("viewPatients");
        }

        public ActionResult DrugData(Drug d)
        {
            DrugCatalogModel model = new DrugCatalogModel();
            DrugDataModel dataModel = new DrugDataModel() { DrugName = d.Name, Values = model.GetDrugUse(d.NDC) };
            return View(dataModel);
        }

        public ActionResult updateCatalog()
        {
            DrugCatalogModel catalogModel = new DrugCatalogModel();
            List<Drug> ld = catalogModel.GetDrugs();   
             return View(ld);
        }

        public ActionResult UpdateImage(String id)
        {
            return View();

        }
        [HttpPost]
        public ActionResult UpdateImage(String NDC, HttpPostedFileBase file)
        {
            //  string filefullpath = Path.GetFullPath(file.FileName);
            // extract only the fielname
            var fileName = Path.GetFileName(file.FileName);
            // store the file inside ~/App_Data/uploads folder
            string path = @"C:\Users\tahel\Desktop\הכי מעודכן\Medicine2\Medicine\GoogleDriveFiles\" + fileName;
            DrugCatalogModel catalogModel = new DrugCatalogModel();
            catalogModel.UpdateImage(NDC,file,path);
            
            //send to model--------------------------------------------------------------------------------------------------------------

             

            return View();

        }
        public ActionResult DeleteDrug(string name)
        {
            DrugCatalogModel model = new DrugCatalogModel();
            model.deleteDrug(name);
            return RedirectToAction("updateCatalog");
        }


        public ActionResult getDrugFromAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult getDrugFromAdmin(FormCollection collection)
        {
            DrugCatalogModel model = new DrugCatalogModel();
            Drug d = model.GetDrugByName(collection["drug"]);
            return RedirectToAction("DrugData", d);
        }
        public ActionResult CreateDrug()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDrug(FormCollection collection)
        {
            DrugCatalogModel model = new DrugCatalogModel();
            Drug dr = new Drug() { Name = collection["Name"], ManufacturerName = collection["SubstanceName"], ActiveIngredientUnit = collection["ActiveIngredientUnit"], GenericName = collection["GenericName"], ImagePath = collection["ImagePath"], NDC = collection["NDC"] };
            model.AddDrug(dr);
            return RedirectToAction("updateCatalog");
        }

        public ActionResult ExeptionHandler(string s)
        {
            ExeptionHandler model = new ExeptionHandler() { Message = s };
            return View(model);
        }
    }
}