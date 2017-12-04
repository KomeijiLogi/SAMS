using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using SAMS.Customers.Dtos;
using SAMS.WorkOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using Abp.UI;

namespace SAMS.Customers
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IRepository<Customer,string> _customnerRepository;
        private readonly IRepository<WorkOrderBill> _workOrderBillRepository;
        private readonly ICustomerDomainService _customerDomainService;

        public CustomerAppService(IRepository<Customer,string> customnerRepository, IRepository<WorkOrderBill> workOrderBillRepository
            , ICustomerDomainService customerDomainService )
        {
            _customnerRepository = customnerRepository;
            _workOrderBillRepository = workOrderBillRepository;
            _customerDomainService = customerDomainService;
        }
        public ListResultDto<GetCustomerByNameDto> GetCustomerByName(string name)
        {

            var list = _customnerRepository.GetAll()
                .Where(x => x.Name.Contains(name))
                .ToList();

            return new ListResultDto<GetCustomerByNameDto>(list.MapTo<List<GetCustomerByNameDto>>());
        }
        //public ListResultDto<GetCustomerForSelectDto> GetCustomerByLocation(string location)
        //{
        //    var list = _customnerRepository.GetAll()
        //        .Where(x => x.Area.Contains(location))
        //        .ToList();

        //    return new ListResultDto<GetCustomerForSelectDto>(list.MapTo<List<GetCustomerForSelectDto>>());
        //}

        public PagedResultDto<CustomerDto> GetCustomers(GetCustomersInput input)
        {
            var customerQuery = _customnerRepository.GetAll();

            var workOrderQuery = _workOrderBillRepository
                .GetAll()
                .GroupBy(e => e.Customer)
                .Select(e => e.OrderByDescending(o => o.CreationTime).FirstOrDefault())
                ;
            DateTime? dt = null;
            var query = from customer in customerQuery
                        join workorder in workOrderQuery on customer.Id equals workorder.CustomerId into temp
                        from tt in temp.DefaultIfEmpty()
                        select new CustomerDto()
                        {
                            LastServiceTime = tt == null ? dt : tt.ServiceTime,
                            Id = customer.Id,
                            Name=customer.Name

                        };


            var list = query.OrderBy("Name")
                .PageBy(input)
                .ToList();
            var recordCount = list.Count;
            // var billList = list.MapTo<List<GetNewAccessoryUseDto>>();
            return new PagedResultDto<CustomerDto>(recordCount, list);

        }

        public GetDetailOutput GetDetail(GetDetailInput input)
        {
            
            var query = _customnerRepository
             .GetAllIncluding()
             .Where(e => e.Id == input.Id);

            var customer = query.FirstOrDefault();
            if (customer == null)
            {
                throw new UserFriendlyException("客户不存在");
            }
            return customer.MapTo<GetDetailOutput>();
        }

       
        
        public DateTime? GetLastServiceTime(string customerId)
        {
           var workOrder= _workOrderBillRepository.GetAll()
                .Where(bill => bill.CustomerId == customerId)
                .OrderByDescending(bill => bill.CreationTime)
                .FirstOrDefault();
            if (workOrder == null)
                return null;
            else
                return workOrder.CreationTime;
        }
    }
}
