using System.Threading.Tasks;
using SAMS.WorkOrders.Dtos;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;

namespace SAMS.WorkOrders
{
    public interface IWorkOrderAppService: IApplicationService
    {
        
        /// <summary>
        /// 新建并受理工单
        /// </summary>
        /// <param name="input"></param>
        void CreateAndAcceptWorkOrder(CreateOrUpdateWorkOrderInput input);
        GetDetailOutput GetDetail(GetDetailInput input);
        GetExpenseOutput GetExpense( int id);
        PagedResultDto<WorkOrderListDto> GetStaffList(GetListInput input);
        void Dispatch(DispatchInput input);
        WorkOrderBill Create(CreateOrUpdateWorkOrderInput input);
        void Complete(CompleteInput input);
        void Report(ReportInput input);
        void UpdateExpense(ExpenseInput input);
        void CreateOrUpdateWorkOrder(CreateOrUpdateWorkOrderInput input);
        GetWorkOrderForEditOutput GetWorkOrderForEdit(int id);
        PagedResultDto<WorkOrderListDto> GetWorkOrders(GetWorkOrderInput input);
        PagedResultDto<GetNewStaffUseDto> GetNewStaffUse(GetNewStaffUseInput input);
        PagedResultDto<GetNewAccessoryUseDto> GetNewAccessoryUse(GetNewAccessoryUseInput input);
        void Accept(int id);
        void Cancel(int id);
        void ReturnVisit(ReturnVisitInput input);
        void Close(int id);
        void Archive(int id);
        int GetAllCount();
        int GetCountByCreationTime(DateTime datetime);
        int GetUnDispatchCount();
        int GetUnReturnVisitCount();
        int GetUnCloseCount();

        PagedResultDto<WorkOrderListDto> GetStaffCreate(GetStaffCreateInput input);
        ListResultDto<string> GetCustomerOffices(string customerId);
    }
}