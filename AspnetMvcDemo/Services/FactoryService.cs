using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.Services
{
    public class FactoryService
    {
        QCEntities db = new QCEntities();

        //Function for getting Factories for each User
        public List<Factory11> UsersFactory (User user)
        {
            var q = (from fact in db.Factory11
                     where fact.Location_Id == user.Location_Id && fact.State == true
                     select fact
                     ).ToList();
            return q;
        }

        public int[] GetUsersFactoryID(int? Location_Id)
        {
            var q = (from fact in db.Factory11
                     where fact.Location_Id == Location_Id && fact.State == true
                     select fact.Id
                     ).ToArray();
            return q;
        }
    }
}