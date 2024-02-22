using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting1.DataLeyer.Repositories;
using Accounting1.DataLeyer.Services;
using Accounting1.DataLeyer.Context;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Accounting1.DataLeyer.Accounting_DBEntities db = new Accounting1.DataLeyer.Accounting_DBEntities();
            //ICustomerRepository customer = new CustomerRepository(db);
            //Accounting1.DataLeyer.Customers AddCustomer = new Accounting1.DataLeyer.Customers()
            //{
            //    FullName="سپهر علیزاده",
            //    CustomerImage = "no Photo",
            //    Mobile="0912333221"
            //};
            //customer.InsertCustomer(AddCustomer);
            ////customer.Save();
            //var list = customer.GetAllCustomers();
            UnitOfWork db = new UnitOfWork();
            var list = db.CustomerRepository.GetAllCustomers();
            db.Dispose();
            
        }
    }
}
