using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Invoices
    {
        //public List<Invoices> InvoicesList { get; set; }
        public InvoicesEnum Name { get; set; }
        public double Price { get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime Month { get; set; }
        public DateTime DueDate { get; set; }
        public InvoicesEnum Company { get; set; }

        public Invoices(InvoicesEnum name, InvoiceStatus status, DateTime month)
        {
            Name = name;
            Price = rnd.Next(2, 10);
            Status = status;
            Month = month;
            DueDate = Month.AddDays(7);
        }

        //public void CheckMonth(int month)
        //{
        //    DueDate = Month.AddDays(7);
        //}
        Random rnd = new Random();
    }
}
