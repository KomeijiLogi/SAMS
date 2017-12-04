using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SAMS.Customers.Dtos;
using System;

namespace SAMS.Customers
{
    public interface ICustomerAppService : IApplicationService
    {
        ListResultDto<GetCustomerByNameDto> GetCustomerByName(string name);
        PagedResultDto<CustomerDto> GetCustomers(GetCustomersInput input);
        GetDetailOutput GetDetail(GetDetailInput input);
        //void Create(CreateInput input);
        //void Edit(EditInput input);
        //void Delete(int id);
        DateTime? GetLastServiceTime(string customerId);
       // ListResultDto<GetCustomerForSelectDto> GetCustomerByLocation(string location);
    } 
}