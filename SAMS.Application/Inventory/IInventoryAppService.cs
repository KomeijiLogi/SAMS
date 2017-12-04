using Abp.Application.Services.Dto;
using SAMS.Inventory.Dto;
using SAMS.WorkOrders.Dtos;

namespace SAMS.Inventory
{
    public interface IInventoryAppService
    {
        PagedResultDto<StaffStockDto> GetStaffStock(GetNewStaffStockInput input);
        PagedResultDto<OrgStockDto> GetOrgStock(GetOrgStockInput input);
        void CreateNewEnterStockOrder(CreateNewEnterStockOrderInput input);
        void CreateNewExportStockOrder(CreateNewExportStockOrderInput input);
        PagedResultDto<StockBillHeadDto> GetNewExportEnterStockOrder(GetNewExportEnterStockOrderInput input);
        StockBillDto GetStockBillDetail(int id);
        PagedResultDto<StockBillDto> GetNewStaffPicking(GetNewStaffPickingInput input);
        PagedResultDto<StockBillDto> GetNewStaffRefund(GetNewStaffRefundInput input);
        ListResultDto<StaffStockDto> GetStaffStockList(long staffId);
        ListResultDto<StaffStockDto> GetStaffStockListByProduct(long staffId, string productId);
    }
}