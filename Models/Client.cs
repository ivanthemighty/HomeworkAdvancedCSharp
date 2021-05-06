using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Client : User
    {
        public List<Invoices> InvoicesList { get; set; }
        private double Balance { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }

        public Client(string firstName, string lastName, string username, string password, double balance) : base(firstName, lastName, username, password, RoleEnum.Client)
        {
            InvoicesList = new List<Invoices>
            {
                new Invoices(InvoicesEnum.EVN, InvoiceStatus.Unpaid, DateTime.Now),
                new Invoices(InvoicesEnum.BEG, InvoiceStatus.Unpaid, DateTime.Now),
                new Invoices(InvoicesEnum.Vodovod, InvoiceStatus.Unpaid, DateTime.Now),
            };
            Balance = balance;
        }

        public void IncreaseBalance(double balance)
        {
            Balance = Balance + balance;
        }

        public double BalanceCheck()
        {
            return Balance;
        }

        public bool PayInvoice(double price)
        {
            if (Balance - price >= 0)
            {
                return true;
            }
            return false;
        }

        //static string getFullName(int month)
        //{
        //    DateTime date = new DateTime(2020, month, 1);

        //    return date.ToString("MMMM");
        //}
    }
}
