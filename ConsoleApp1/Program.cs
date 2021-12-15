using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            UserPasswordDAL user = new UserPasswordDAL();
            user.CheckAdminPassword(new UserPassword() { Id = 123, Password = "123" });
            //////////////////////////////////////////////////////////////////////
        }
    }
}
