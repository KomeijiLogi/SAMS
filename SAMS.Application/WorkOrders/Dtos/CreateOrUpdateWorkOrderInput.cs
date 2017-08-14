using System.ComponentModel.DataAnnotations;

namespace SAMS.WorkOrders.Dtos
{
    public class CreateOrUpdateWorkOrderInput
    {
        
        public WorkOrderEditDto WorkOrder { get; set; }

        
    }
}