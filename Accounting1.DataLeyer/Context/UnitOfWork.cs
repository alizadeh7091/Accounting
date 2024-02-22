using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting1.DataLeyer.Repositories;
using Accounting1.DataLeyer.Services;

namespace Accounting1.DataLeyer.Context
{
    public class UnitOfWork : IDisposable
    {
        Accounting_DBEntities db = new Accounting_DBEntities();

        private ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository==null)
                {
                    _customerRepository = new CustomerRepository(db);
                }
                return _customerRepository;
            }
        }

        private GenericRepository<Accounting> _AccountingRepository;

        public GenericRepository<Accounting> AccountingRepository
        {
            get
            {
                if (_AccountingRepository == null)
                {
                    _AccountingRepository = new GenericRepository<Accounting>(db);
                }
                return _AccountingRepository;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
