using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SAMS.Customers.Dtos;
using System;

namespace SAMS.Customers
{
    public interface ICustomerAppService : IApplicationService
    {
        ListResultDto<GetCustomerForSelectDto> GetCustomerByName(string name);
        PagedResultDto<CustomerDto> GetCustomers(GetCustomersInput input);
        GetDetailOutput GetDetail(GetDetailInput input);
        void Create(CreateInput input);
        void Edit(EditInput input);
        void Delete(int id);
        DateTime? GetLastServiceTime(int customerId);
        ListResultDto<GetCustomerForSelectDto> GetCustomerByLocation(string location);
    } 
}