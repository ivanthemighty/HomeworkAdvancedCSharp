using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace DomasnaInvoice
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>
            {
                new Client("Bob", "Bobsky", "bob123", "bob12345", 0),
                new Client("Test", "Client", "test", "test", 15),
                new Client("Nikola", "Trajcevski", "nik123", "nik12345", 500),
                new Client("Martin", "Nikoloski", "martin123", "123456", 50),
                new Admin("Admin-BEG", "BEG", "BEG", "BEG", InvoicesEnum.BEG),
                new Admin("Admin-EVN", "EVN", "EVN", "EVN", InvoicesEnum.EVN),
                new Admin("Admin-Vodovod", "Vodovod", "Vodovod", "Vodovod", InvoicesEnum.Vodovod),
            };

            List<User> clients = new List<User>();
            clients = users.Where(x => x.Role == RoleEnum.Client).ToList();

            Client client = (Client)clients.FirstOrDefault(x => x.Role == RoleEnum.Client);

            while (true)
            {
                try
                {
                    Console.Write("Please enter username: ");
                    string username = Console.ReadLine();
                    Console.Write("Please enter password: ");
                    string password = Console.ReadLine();

                    //User loginAdmin = users.First(x => x.Login(username, password) != null);
                    User loginUser = users.First(x => x.Login(username, password) != null);

                    if (loginUser == null)
                    {
                        throw new Exception("Invalid username");
                    }

                    // ADMIN CODE
                    if (loginUser.Role == RoleEnum.Admin)
                    {
                        HelloAdmin((Admin)loginUser);

                        Console.WriteLine("#1 List all clients");
                        //Console.WriteLine("#2 Keep track for all inoices");
                        //Console.WriteLine("#3 Pay invoice");
                        Console.WriteLine("#4 Exit\n");

                        Console.Write("Pick an option: ");
                        string pickString = Console.ReadLine();

                        bool convert = int.TryParse(pickString, out int pick);
                        if (!convert)
                        {
                            throw new Exception("Wrong input try again");
                        }

                        switch (pick)
                        {
                            case 1:
                                if (loginUser.Username == "EVN")
                                {
                                    CurrentClient(clients);
                                    //ListAllClientsEVN(client, clients);
                                }
                                if (loginUser.Username == "BEG")
                                {
                                    ListAllClientsBEG(client, clients);
                                }
                                if (loginUser.Username == "Vodovod")
                                {
                                    ListAllClientsVodovod(client, clients);
                                }
                                break;
                            case 4:
                                Console.WriteLine("\nSuccesfull exit\n");
                                break;
                            default:
                                throw new Exception("Wrong input");
                        }
                    }

                    // CLIENT CODE
                    if (loginUser.Role == RoleEnum.Client)
                    {
                        HelloClient((Client)loginUser);

                        while (true)
                        {
                            Console.WriteLine("#1 Increase Balance");
                            Console.WriteLine("#2 Keep track for all inoices");
                            Console.WriteLine("#3 Pay invoice\n");
                            Console.WriteLine("#4 LOGOUT \n");

                            Console.Write("Pick an option: ");
                            string pickString = Console.ReadLine();

                            bool convert = int.TryParse(pickString, out int pick);
                            if (!convert)
                            {
                                throw new Exception("Wrong input try again");
                            }

                            switch (pick)
                            {
                                case 1:
                                    ClientBalanceIncrase((Client)loginUser);
                                    break;
                                case 2:
                                    InvoicesCheck((Client)loginUser);
                                    break;
                                case 3:
                                    PayInvoices((Client)loginUser);
                                    break;
                                case 4:
                                    //Console.WriteLine("Successfull logout");
                                    throw new Exception("Successfull logout!!");
                                default:
                                    throw new Exception("Wrong input!!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            }
        }

        static void CurrentClient(List<User> clients)
        {
            Client clientObj = (Client)clients.FirstOrDefault(x => x.Role == RoleEnum.Client);
            foreach (var client in clients)
            {
                Console.WriteLine(client.FullName);
                Console.WriteLine(clientObj.InvoicesList);
            }
            for (int i = 0; i < clients.Count; i++)
            {
                Console.WriteLine(clients[i]);
            }
            //for (int i = 0; i < clients.Count; i++)
            //{
            //    Console.WriteLine(clients[i].FullName);

            //}
        }

        // client settings
        static void HelloClient(Client login)
        {
            Console.WriteLine($"\nHello {login.FullName}\n");
        }

        static void ClientBalanceIncrase(Client login)
        {
            Console.Write("Enter amount: ");
            string stringNum = Console.ReadLine();

            bool convert = int.TryParse(stringNum, out int amount);
            if (!convert)
            {
                throw new Exception("Wrong input");
            }

            if (amount <= 0)
            {
                throw new Exception("You cant input negative");
            }

            Console.WriteLine($"You have succesfully increased your balance by:{amount}$");
            login.IncreaseBalance(amount);
            Console.WriteLine($"Your balance now is {login.BalanceCheck()}$\n");
        }

        static void InvoicesCheck(Client login)
        {
            //foreach(Invoices invoice in login.InvoicesList)
            //{
            //    Console.WriteLine($"Invoice: {invoice.Name} {invoice.Price}$");    
            //}

            Random rnd = new Random();

            int count = 1;

            Console.WriteLine();
            foreach (Invoices invoice in login.InvoicesList)
            {
                Console.WriteLine($"#{count++} {invoice.Name,2} Price:{invoice.Price,2}$ {invoice.Status,3} \nFor month: {invoice.Month.ToString("MMMM yyyy"),4} \nDue:{invoice.DueDate.ToString($" {0:MM MMMM}"),1}\n");
            }
            Console.WriteLine();
        }

        static void PayInvoices(Client login)
        {
            //int beg = (int)InvoicesEnum.BEG; // 1 Enum
            //int evn = (int)InvoicesEnum.EVN; // 2 Enum
            //int vodovod = (int)InvoicesEnum.Vodovod; // 3 Enum

            int count = 1;
            Console.WriteLine("\nWhich one do you want to pay?");
            Console.WriteLine();
            foreach (Invoices invoice in login.InvoicesList)
            {
                //if (invoice.Status == InvoiceStatus.Unpaid)
                //{
                //    Console.WriteLine($"#{count++} {invoice.Name,2} {invoice.Price,2}$ {invoice.Status,3}");
                //}
                Console.WriteLine($"#{count++} {invoice.Name,2} Price:{invoice.Price,2}$ {invoice.Status,3} \nFor month: {invoice.Month.ToString("MMMM yyyy"),4} \nDue:{invoice.DueDate.ToString($" {0:MM MMMM}"),1}\n");
            }
            Console.WriteLine();
            Console.Write("Pick an option: ");
            string pickString = Console.ReadLine();

            bool convert = int.TryParse(pickString, out int pick);
            if (!convert)
            {
                throw new Exception("Wrong input try again");
            }

            foreach (Invoices invoice in login.InvoicesList)
            {
                if (invoice.Status == InvoiceStatus.Paid)
                {
                    Console.WriteLine("Already paid!!\n");
                    break;
                }
            }

            switch (pick)
            {
                case 1:
                    if (login.InvoicesList[0].Status == InvoiceStatus.Unpaid)
                    {
                        double invoicePrice = login.InvoicesList[0].Price;
                        double balanceCondition = login.BalanceCheck();
                        if (balanceCondition >= invoicePrice)
                        {
                            login.InvoicesList[0].Status = InvoiceStatus.Paid;
                            login.IncreaseBalance(-invoicePrice);
                            Console.WriteLine("Succesfully paid\n");
                        }
                        else
                            throw new Exception("\nNot enough money!!");
                    }
                    break;
                case 2:
                    if (login.InvoicesList[1].Status == InvoiceStatus.Unpaid)
                    {
                        double invoicePrice = login.InvoicesList[1].Price;
                        double balanceCondition = login.BalanceCheck();
                        if (balanceCondition >= invoicePrice)
                        {
                            login.InvoicesList[1].Status = InvoiceStatus.Paid;
                            login.IncreaseBalance(-invoicePrice);
                            Console.WriteLine("Succesfully paid\n");
                        }
                        else
                            throw new Exception("\nNot enough money!!");
                    }
                    break;
                case 3:
                    if (login.InvoicesList[2].Status == InvoiceStatus.Unpaid)
                    {
                        double invoicePrice = login.InvoicesList[2].Price;
                        double balanceCondition = login.BalanceCheck();
                        if (balanceCondition >= invoicePrice)
                        {
                            login.InvoicesList[2].Status = InvoiceStatus.Paid;
                            login.IncreaseBalance(-invoicePrice);
                            Console.WriteLine("Succesfully paid\n");
                        }
                        else
                            throw new Exception("\nNot enough money!!");
                    }
                    break;
                default:
                    throw new Exception("Wrong input try again\n");
            }
        }


        // Admin settings
        static void ListAllClientsEVN(Client login, List<User> clients)
        {
            Console.WriteLine();
            
                //if (invoice.Name == InvoicesEnum.EVN)
                //{
                //    for (int i = 0; i < clients.Count; i++)
                //    {
                //        Console.WriteLine($"{clients[i].FullName,4} {invoice.Price,4}$   {invoice.Status,1}");
                //    }
                //}
                
                for(int i = 0; i < clients.Count; i++)
                {
                    //foreach(Invoices invoices in login.InvoicesList) 
                    //{
                    //    if (login.InvoicesList[2].Name == InvoicesEnum.EVN)
                    //    {
                    //        Console.WriteLine($"{clients[i].FullName,4} {login.InvoicesList[2].Price,4}$   {login.InvoicesList[2].Status,1}");
                    //    }
                    //    break;
                    //}
                    if (login.InvoicesList[0].Name == InvoicesEnum.EVN)
                    {
                        Console.WriteLine($"Client:{clients[i].FullName,4} Status: {login.InvoicesList[0].Status,1}");
                        
                    }
                    
                    //if (login.InvoicesList[2].Name == InvoicesEnum.EVN)
                    //{
                    //    Console.WriteLine($"{clients[i].FullName,4} {login.InvoicesList[2].Price,4}$   {login.InvoicesList[2].Status,1}");
                    //}
            }
            Console.WriteLine();
        }

        static void ListAllClientsBEG(Client login, List<User> clients)
        {
            Console.WriteLine();
            //foreach (Invoices invoice in login.InvoicesList)
            //{
            //    if (invoice.Name == InvoicesEnum.BEG)
            //    {
            //        for (int i = 0; i < clients.Count; i++)
            //        {
            //            Console.WriteLine($"{clients[i].FullName,4} {invoice.Price,4}$   {invoice.Status,1}");
            //        }
            //    }
            //    //for (int i = 0; i < clients.Count; i++)
            //    //{
            //    //    Console.WriteLine($"{clients[i].FullName,4} {invoice.Price,4}$   {invoice.Status,1} {invoice.Name}");
            //    //}
            //    break;
            //}
            Console.WriteLine();
            for (int i = 0; i < clients.Count; i++)
            {
                if (login.InvoicesList[1].Name == InvoicesEnum.BEG)
                {
                    Console.WriteLine($"Client:{clients[i].FullName,4} Status: {login.InvoicesList[1].Status,1}");
                }
            }
            Console.WriteLine();
        }

        static void ListAllClientsVodovod(Client login, List<User> clients)
        {
            //Console.WriteLine();
            //foreach (Invoices invoice in login.InvoicesList)
            //{
            //    if (invoice.Name == InvoicesEnum.Vodovod)
            //    {
            //        for (int i = 0; i < clients.Count; i++)
            //        {
            //            Console.WriteLine($"{clients[i].FullName,4} {invoice.Price,4}$   {invoice.Status,1}");
            //        }
            //    }
            //    //for (int i = 0; i < clients.Count; i++)
            //    //{
            //    //    Console.WriteLine($"{clients[i].FullName,4} {invoice.Price,4}$   {invoice.Status,1}");
            //    //}
            //    break;
            //}
            //Console.WriteLine();

            Console.WriteLine();
            for (int i = 0; i < clients.Count; i++)
            {
                if (login.InvoicesList[2].Name == InvoicesEnum.Vodovod)
                {
                    Console.WriteLine($"Client: {clients[i].FullName,4}  Status: :{login.InvoicesList[2].Status,1}");
                }
            }
            Console.WriteLine();
        }

        static void HelloAdmin(Admin login)
        {
            Console.WriteLine($"\nHello {login.FullName}\n");
        }
    }
}

