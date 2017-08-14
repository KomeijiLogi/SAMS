using System;
using System.Data.Entity;
using System.Linq;
namespace SAMS.Web.Areas.StaffMobile.Models
{
   
    public class WorkOrderEntry
    {
        public int Id { get; set; }
        public int workOrderId { get; set; }                 //工单ID
        public int workOrderNo { get; set; }                 //工单编号
        public String workOrderName { get; set; }            //工单名称
        public String workOrderUser { get; set; }            //工单用户  
        public String workOrderProduct { get; set; }         //报修产品
        public String workOrderProject { get; set; }         //服务项目
        public String workOrderServer { get; set; }          //服务描述
        public String workOrderTime { get; set; }            //提交时间
        public String customName { get; set; }               //客户名称
        public String customPeople { get; set; }              //联系人 
        public String customPhone { get; set; }              //联系电话
        public String customAddress { get; set; }            //联系地址
        public String workOrderStatus { get; set; }           //工单状态 0未处理 1完成

    }
    public class PluginEntry
    {
        public int Id { get; set; }
        public String pluginEncode { get; set; }       //设备编码
        public String pluginName { get; set; }         //备件名称
        public String pluginFactory { get; set; }      //备件厂商
        public String pluginSpec { get; set; }         //备件规格
        public String pluginType { get; set; }         //备件型号
        public String pluginUnit { get; set; }         //备件单位
        public String pluginNumber { get; set; }        //备件数量
        public String pluginSafetyStock { get; set; }    //备件安全库存
        public String pluginWarningStatus { get; set; }   //备件预警状态 0盈余(绿色) 1预警（黄色） 2匮乏（红色） 
    }
    public class ReceiptEntry
    {
        public int Id { get; set; }
        public String pluginEncode { get; set; }      //备件编码
        public String pluginName { get; set; }         //备件名称
        public String pluginFactory { get; set; }      //备件厂商
        public String pluginSpec { get; set; }         //备件规格
        public String pluginType { get; set; }         //备件型号
        public String pluginUnit { get; set; }         //备件单位
        public String pluginNumber { get; set; }        //备件数量
        public String pluginSafetyStock { get; set; }    //备件安全库存
        public String pluginWarningStatus { get; set; }   //备件预警状态 0盈余(绿色) 1预警（黄色） 2匮乏（红色） 
        public String BdReasonNo { get; set; }             //故障原因编号
        public String BdResaonName { get; set; }           //故障原因名称
        public String BdReasonContext { get; set; }        //故障原因描述
        public String BdType { get; set; }                  //故障类型
        public String BdWeight { get; set; }                 //故障权重，值越高代表优先级越高        
        public String BdStatus { get; set; }                 //故障状态  

    }
    public class WorkOrderHistoryEntry
    {
        public int Id { get; set; }
        public int workOrderId { get; set; }                 //工单ID
        public int workOrderNo { get; set; }                 //工单编号
        public String workOrderName { get; set; }            //工单名称
        public String workOrderUser { get; set; }            //工单用户  
        public String workOrderProduct { get; set; }         //报修产品
        public String workOrderProject { get; set; }         //服务项目
        public String workOrderServer { get; set; }          //服务描述
        public String workOrderTime { get; set; }            //提交时间
        public String customName { get; set; }               //客户名称
        public String customPeople { get; set; }              //联系人 
        public String customPhone { get; set; }              //联系电话
        public String customAddress { get; set; }            //联系地址
        public String workOrderStatus { get; set; }           //工单状态 0异常 1正常 

    }
    public class ReasonEntity
    {

        public int Id { get; set; }
        public String BdReasonNo { get; set; }              //故障原因编号
        public String BdResaonName { get; set; }            //故障原因名称
        public String BdReasonContext { get; set; }         //故障原因描述
        public String BdType { get; set; }                  //故障类型
        public String BdWeight { get; set; }                //故障权重，值越高代表优先级越高        
        public String BdStatus { get; set; }                //故障状态  

    }

}