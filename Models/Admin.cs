using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Admin : User
    {
        public InvoicesEnum Company { get; set; }

        public Admin(string firstName, string lastName, string username, string password, InvoicesEnum company) : base(firstName, lastName, username, password, RoleEnum.Admin)
        {
            Company = company;
        }

        //public Admin CompanyCheck(InvoicesEnum company)
        //{
        //    if(company == InvoicesEnum.EVN)
        //    {
        //        return this;
        //    }
        //    if (company == InvoicesEnum.EVN)
        //    {
        //        return this;
        //    }
        //    if (company == InvoicesEnum.EVN)
        //    {
        //        return this;
        //    }
        //    return null;
        //}
    }
}
