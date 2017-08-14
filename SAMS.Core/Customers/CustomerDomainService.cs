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
        private readonly IRepository<Customer> _customerRepository;
        public CustomerDomainService (IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public void Create(Customer customer)
        {
           
            int id=_customerRepository.InsertAndGetId(customer);
           
        }

        public Customer Get(int id)
        {
            return _customerRepository.Get(id);
        }
    }
}
