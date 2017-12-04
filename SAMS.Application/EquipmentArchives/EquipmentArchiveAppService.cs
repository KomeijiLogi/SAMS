using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SAMS.EquipmentArchives.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Linq.Extensions;
using SAMS.WorkOrders;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace SAMS.EquipmentArchives
{
    public class EquipmentArchiveAppService : SAMSAppServiceBase,IEquipmentArchiveAppService
    {
        private readonly IRepository<EquipmentArchive> _equipmentArchiveRepository;
        private readonly IRepository<WorkOrderBill> _workOrderBillRepository;
        public EquipmentArchiveAppService(IRepository<EquipmentArchive> equipmentArchiveRepository
            , IRepository<WorkOrderBill> workOrderBillRepository)
        {
            _equipmentArchiveRepository = equipmentArchiveRepository;
            _workOrderBillRepository = workOrderBillRepository;
        }

        public bool CheckSerialNo(int? id,string serialNo)
        {
            return _equipmentArchiveRepository.GetAll()
                .WhereIf(id.HasValue,e=>!e.Id.Equals(id.Value))
                .Where(e => e.SerialNo.Equals(serialNo)).Count() == 0;
        }

        public void Create(CreateEquipmentArchiveInput input)
        {
            var archive = _equipmentArchiveRepository.FirstOrDefault(e => e.SerialNo == input.SerialNo);
            if(archive!=null)
            {
                throw new UserFriendlyException("设备序号已经存在！");
            }
            archive = new EquipmentArchive
            {
                AssignedPersonId = input.AssignedPersonId,
                CustomerId = input.CustomerId,
                InstallType = input.InstallType,
                Office = input.Office,
                OfficeMobile = input.OfficeMobile,
                OfficePerson = input.OfficePerson,
                OfficePosition = input.OfficePosition,
                OfficeTel = input.OfficeTel,
                ProductId = input.ProductId,
                SerialNo = input.SerialNo,
                ServiceTime = input.ServiceTime,
                Warrenty = input.Warrenty
            };
            _equipmentArchiveRepository.Insert(archive);
        }
        
        public void Delete(int id)
        {
            _equipmentArchiveRepository.Delete(id);
        }

        public void Edit(EditEquipmentArchiveInput input)
        {
            var archive = _equipmentArchiveRepository.FirstOrDefault(e => (e.SerialNo == input.SerialNo && e.Id!=input.Id));
            if (archive != null)
            {
                throw new UserFriendlyException("设备序号重复！");
            }
            archive = new EquipmentArchive
            {
                Id=input.Id,
                AssignedPersonId = input.AssignedPersonId,
                CustomerId = input.CustomerId,
                InstallType = input.InstallType,
                Office = input.Office,
                OfficeMobile = input.OfficeMobile,
                OfficePerson = input.OfficePerson,
                OfficePosition = input.OfficePosition,
                OfficeTel = input.OfficeTel,
                ProductId = input.ProductId,
                SerialNo = input.SerialNo,
                ServiceTime = input.ServiceTime,
                Warrenty = input.Warrenty
            };
            _equipmentArchiveRepository.Update(archive);
        }

        public EquipmentArchiveDetailDto GetArchiveBySerialNo(string serialNo)
        {
            var query = _equipmentArchiveRepository.GetAll().Where(x => x.SerialNo.Equals(serialNo));
            var archive=query.FirstOrDefault();
            return archive.MapTo<EquipmentArchiveDetailDto>();

        }

        public EquipmentArchiveDetailDto GetDetail(int Id)
        {
           var archive= _equipmentArchiveRepository.Get(Id);
           var LastServiceTime = _workOrderBillRepository.GetAll()
                 .Where(e => e.SerialNo.Equals(archive.SerialNo))
                 //.OrderByDescending(e => e.ServiceTime)
                 .Max(e => e.ServiceTime);
           var result= archive.MapTo<EquipmentArchiveDetailDto>();
           result.LastServiceTime = LastServiceTime;
           return result;
        }

        public PagedResultDto<EquipmentArchiveListDto> GetEquipmentArchives(GetEquipmentArchivesInput input)
        {
            DateTime? EndInstallDate = input.EndInstallDate.HasValue? input.EndInstallDate.Value.Date:new Nullable<DateTime>();
            DateTime? StartWarrenty = input.StartWarrenty.HasValue?input.StartWarrenty.Value.Date:new Nullable<DateTime>();
            DateTime? StartInstallDate = input.StartInstallDate.HasValue?input.StartInstallDate.Value.Date:new Nullable<DateTime>();
            DateTime? EndWarrenty = input.EndWarrenty.HasValue?input.EndWarrenty.Value.Date:new Nullable<DateTime>();
            DateTime? StartServiceDate = input.StartServiceDate.HasValue?input.StartServiceDate.Value.Date:new Nullable<DateTime>();
            DateTime? EndServiceDate = input.EndServiceDate;
            var archiveQuery = _equipmentArchiveRepository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.CustomerName), 
                            e => e.Customer.Name.Contains(input.CustomerName))
                 .WhereIf(!string.IsNullOrEmpty(input.SerialNo),
                            e => e.SerialNo==input.SerialNo)
                 .WhereIf(!string.IsNullOrEmpty(input.ProductId),
                            e => e.ProductId == input.ProductId)
                 .WhereIf(input.StartInstallDate.HasValue, 
                        e=> DbFunctions.TruncateTime( e.ServiceTime.Value)>=StartInstallDate.Value)
                 .WhereIf(input.EndInstallDate.HasValue, e => DbFunctions.TruncateTime(e.ServiceTime.Value) <= EndInstallDate.Value)
                 .WhereIf(input.StartWarrenty.HasValue, e => DbFunctions.TruncateTime(e.Warrenty.Value) >=StartWarrenty.Value)
                 .WhereIf(input.EndWarrenty.HasValue, e => DbFunctions.TruncateTime(e.Warrenty.Value) <= EndWarrenty.Value)
                 //.WhereIf(input.EndServiceDate.HasValue, e => DbFunctions.TruncateTime(e.last.Value) <= EndServiceDate.Value)
                 //.WhereIf(input.StartServiceDate.HasValue, e => DbFunctions.TruncateTime(e.Warrenty.Value) >= StartWarrenty.Value)
                // e =>int.Parse(string.Format("{0:yyyyMMddd}",e.ServiceTime.Value))>=int.Parse( string.Format("{0:yyyyMMddd}",input.StartInstallDate.Value)))
                //.WhereIf(input.EndInstallDate.HasValue, e => int.Parse(string.Format("{0:yyyyMMddd}", e.ServiceTime.Value)) <= int.Parse(string.Format("{0:yyyyMMddd}", input.EndInstallDate.Value)))
                //.WhereIf(input.StartWarrenty.HasValue, e => int.Parse(string.Format("{0:yyyyMMddd}", e.Warrenty.Value)) <= int.Parse(string.Format("{0:yyyyMMddd}", input.EndWarrenty.Value)))
                //.WhereIf(input.StartInstallDate.HasValue, e => int.Parse(string.Format("{0:yyyyMMddd}", e.Warrenty.Value)) >= int.Parse(string.Format("{0:yyyyMMddd}", input.StartWarrenty.Value)))
                ;
            var workOrderQuery = _workOrderBillRepository
                .GetAll()
                .GroupBy(e => e)
                .Select(e => e.OrderByDescending(o => o.CreationTime).FirstOrDefault());

            DateTime? dt = null;
            var query = from archive in archiveQuery
                        join workorder in workOrderQuery on archive.SerialNo equals workorder.SerialNo into temp

                        from t in temp.DefaultIfEmpty()
                      //  where t.ServiceTime>= StartServiceDate
                        select new EquipmentArchiveListDto()
                        {
                            LastServiceTime = t== null ? dt : t.ServiceTime,
                            Office=archive.Office,
                            InstallType=archive.InstallType,
                            AssignedPersonName=archive.AssignedPerson.Name,//安装工程师
                            CustomerName=archive.Customer.Name,
                            Id=archive.Id,
                            OfficeMobile=archive.OfficeMobile,
                            OfficePerson=archive.OfficePerson,
                            OfficePosition=archive.OfficePosition,
                            OfficeTel=archive.OfficeTel,
                            ProductName=archive.Product.Name,
                            ProductModel=archive.Product.Model,
                            SerialNo=archive.SerialNo,
                            ServiceTime=archive.ServiceTime,//安装日期
                            Warrenty=archive.Warrenty


                        };
            query = query
                .WhereIf(input.EndServiceDate.HasValue, e => DbFunctions.TruncateTime(e.LastServiceTime.Value) <= EndServiceDate.Value)
                .WhereIf(input.StartServiceDate.HasValue, e => DbFunctions.TruncateTime(e.LastServiceTime.Value) >= StartServiceDate.Value);


            var recordCount = query.Count();
            var list = query.OrderBy(e=>input.Sorting)
                .PageBy(input)
                .ToList();
            var billList = list.MapTo<List<EquipmentArchiveListDto>>();
            return new PagedResultDto<EquipmentArchiveListDto>(recordCount, billList);
        }
    }
}
