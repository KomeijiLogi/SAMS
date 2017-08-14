using System.Threading.Tasks;
using Abp.Domain.Services;
using SAMS.Users;
using SAMS.WorkOrders;
using SAMS.Accessories;

namespace SAMS.WorkOrders
{
    public interface IWorkOrderManager:IDomainService
    {

        WorkOrderBill Get(int id);
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="bill"></param>
        void Create(WorkOrderBill bill);

        /// <summary>
        /// 受理
        /// </summary>
        /// <param name="bill"></param>
        void Accept(WorkOrderBill bill);

        /// <summary>
        /// 派工
        /// </summary>
        /// <param name="bill"></param>
        void Dispatch(WorkOrderBill bill,User user);

        /// <summary>
        /// 完工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Complete(WorkOrderBill bill);

        /// <summary>
        /// 回访
        /// </summary>
        /// <param name="bill"></param>
        void ReturnVisit(WorkOrderBill bill);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="bill"></param>
        void Close(WorkOrderBill bill);

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="bill"></param>
        void Cancel(WorkOrderBill bill);

        WorkOrderAccessoryEntry AddAccessoryEntry(WorkOrderBill bill, Accessory accessory, int count, int backCount);
        void DeleteAccessoryEntry(WorkOrderAccessoryEntry entry);
        void UpdateAccessoryEntry(WorkOrderAccessoryEntry entry);
        void AddActivity(Activity activity);
    }
}