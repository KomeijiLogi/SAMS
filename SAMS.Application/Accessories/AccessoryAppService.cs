using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using SAMS.Accessories.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using Abp.AutoMapper;

namespace SAMS.Accessories
{
    public class AccessoryAppService : SAMSAppServiceBase, IAccessoryAppService
    {
        private readonly IRepository<Accessory,string> _accessoryRepository;
        public AccessoryAppService(IRepository<Accessory,string> accessoryRepository)
        {
            _accessoryRepository = accessoryRepository;
        }
        /// <summary>
        /// 配件列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<GetAccessoryDto> GetAccessory(GetAccessoryInput input)
        {
            //if (input.Where=="")
            //{
                var query = _accessoryRepository.GetAll();
                var recordCount = query.Count();
                var list = query.OrderBy(input.Sorting)
                    .PageBy(input).Select(x =>
                    new GetAccessoryDto
                    {
                        AccessoryId = x.Id,
                        AccessoryName = x.Name,
                        AccessoryNumber = x.Number,
                        AccessoryModel = x.Model,
                        AccessoryUnit = x.Unit
                    })
                    .ToList();
                //var accessoryList = list.MapTo<List<GetAccessoryDto>>();
                return new PagedResultDto<GetAccessoryDto>(recordCount, list);
            //}
            //else
            //{
            //    var query = _accessoryRepository.GetAll();
            //    var recordCount = query.Count();
            //    var list = query.OrderBy(input.Sorting)
            //        .PageBy(input).Where(x => x.Number.Contains(input.Where)).Select(x =>
            //          new GetAccessoryDto
            //          {
            //              AccessoryId = x.Id,
            //              AccessoryName = x.Name,
            //              AccessoryNumber = x.Number,
            //              AccessoryModel = x.Model,
            //              AccessoryUnit = x.Unit
            //          })
            //        .ToList();
            //    //var accessoryList = list.MapTo<List<GetAccessoryDto>>();
            //    return new PagedResultDto<GetAccessoryDto>(recordCount, list);

            //}


        }

        public void Delete(string id)
        {
            _accessoryRepository.Delete(id);
        }
        public void Create(CreateInput input)
        {
            var accessory = new Accessory()
            {
                Number = input.Number,
                Unit=input.Unit,
                Model = input.Model,
                Name = input.Name

            };
            _accessoryRepository.Insert(accessory);

        }
        public void Edit(EditInput input)
        {
            var accessory = _accessoryRepository.Get(input.Id);
            accessory.Number = input.Number;
            accessory.Unit = input.Unit;
            accessory.Name = input.Name;
            accessory.Model = input.Model;
        }
        public GetDetailOutput GetDetail(string id)
        {

            var accessory = _accessoryRepository.Get(id);
         

     
            return accessory.MapTo<GetDetailOutput>();
        }
    }
}
