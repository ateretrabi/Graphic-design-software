using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicine.Models
{
    public class DrugCatalogModel
    {

        public List<Drug> GetDrugs()
        {
            DrugBL bl = new DrugBL();
            return bl.GetAllDrugs();
           
        }
        public void UpdateImage(string NDC, HttpPostedFileBase drivefile, string path)
        {
            DrugBL bl = new DrugBL();
            bl.UpdateDrugImage(NDC, path, drivefile);

        }

        public Drug GetDrugByName(string name)
        {
            DrugBL bl = new DrugBL();
            return bl.GetDrugByName(name);
        }

        public void deleteDrug(string name)
        {
            Drug d = GetDrugByName(name);
            DrugBL bl = new DrugBL();
            bl.RemoveDrug(d.NDC);
        }

        public int[] GetDrugUse(string drugNDC)
        {
            DrugBL bL = new DrugBL();
            int[] a = bL.GetDrugUse(drugNDC, 2020);
            return a;


        }

        internal void AddDrug(Drug dr)
        {
            DrugBL bl = new DrugBL();
            bl.AddDrug(dr);
        }
    }
}