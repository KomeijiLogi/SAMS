using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Customers
{
    public class CustomerDomainService : ICustomerDomainService
    {
        private readonly IRepository<Customer, string> _customerRepository;
        public CustomerDomainService(IRepository<Customer, string> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public void Create(Customer customer)
        {

            // int id=_customerRepository.InsertAndGetId(customer);

        }

        public Customer Get(string id)
        {
            return _customerRepository.Get(id);

        }
    }
}
