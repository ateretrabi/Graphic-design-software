using BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DrugDAL
    {
        public void AddDrug(Drug drug)
        {
            using (var ctx = new MedicineContext())
            {
                Drug d = ctx.Drugs.Find(drug.NDC);
                if (d != null)
                    throw new Exception("תרופה זו כבר קיימת במערכת");
                ctx.Drugs.Add(drug);
                ctx.SaveChanges();
            }
        }
        public void RemoveDrug(string drugNDC)
        {
            using (var ctx = new MedicineContext())
            {
                Drug drug = ctx.Drugs.Find(drugNDC);
                if (drug == null)
                    throw new Exception("לא נמצאה התרופה במערכת");
                ctx.Drugs.Remove(drug);
                ctx.SaveChanges();
            }
        }

        public Drug GetDrug(string drugNDC)
        {
            using (var ctx = new MedicineContext())
            {
                Drug drug = ctx.Drugs.Find(drugNDC);
                if (drug == null)
                    throw new Exception("לא נמצאה התרופה במערכת");
                return drug;

            }
        }
        public Drug GetDrugByName(string drugName)
        {
            using (var ctx = new MedicineContext())
            {
                Drug drug = ctx.Database.SqlQuery<Drug>(@"Select * FROM Drugs 
                    WHERE Name = @drugName", new SqlParameter("@drugName", drugName)).FirstOrDefault();
                if (drug == null)
                    throw new Exception("לא נמצאה התרופה במערכת");
                return drug;

            }
        }

        public List<Drug> GetAllDrugs()
        {
            using (var ctx = new MedicineContext())
            {
                List<Drug> drugs = new List<Drug>();
                
                drugs = ctx.Drugs.ToList();
                return drugs;

            }
        }

        public void UpdateDrugImage(string drugNDC, string imagePath)
        {
            using (var ctx = new MedicineContext())
            {
                Drug drug = ctx.Drugs.Find(drugNDC);
                if (drug == null)
                    throw new Exception("drug does not exist");
                drug.ImagePath = imagePath;
                //ctx.Entry(drug).CurrentValues.SetValues(imagePath);
                ctx.SaveChanges();

            }
        }

        public int[] GetDrugUse(string drugNDC, int year)
        {
            int[] arr;
            using (var ctx = new MedicineContext())
            {
                Drug drug = ctx.Drugs.Find(drugNDC);
                if (drug == null)
                    throw new Exception("drug does not exist");
                arr = ctx.Database.SqlQuery<int>(@"SELECT COUNT(*) FROM Prescriptions  
                    WHERE Drug_NDC=@drugNDC AND StartDate.Year=@year
                    GROUP BY StartDate.Month
                    ORDER BY StartDate.Month", new SqlParameter("@drugNDC", drugNDC), new SqlParameter("@year", year)).ToArray();

            }

            return arr;

        }


    }
}
