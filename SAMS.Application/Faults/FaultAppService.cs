using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;

using SAMS.Faults.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Faults
{
    public class FaultAppService : ApplicationService, IFaultAppService
    {
        private readonly IRepository<Fault> _faultRepository;
        public FaultAppService(IRepository<Fault> faultRepository)
        {
            _faultRepository = faultRepository;
        }
        public ListResultDto<GetFaultDetailOutput>GetAllDetails()
        {
            var faults =  _faultRepository
                .GetAllIncluding()
                .ToList();
            return new ListResultDto<GetFaultDetailOutput>(faults.MapTo<List<GetFaultDetailOutput>>());
            
        }
        public void Create(CreateInput input)
        {
            var fault = new Fault()
            {
                
                Name = input.Name

            };
            _faultRepository.Insert(fault);

        }
        public void Edit(EditInput input)
        {
            var product = _faultRepository.Get(input.Id);
          
            product.Name = input.Name;
            
        }
        public void Delete(int id)
        {
            _faultRepository.Delete(id);
        }
        public ListResultDto<FaultListDto> GetFaults()
        {
            var faults = _faultRepository.GetAll().OrderByDescending(e => e.CreationTime);
            return new ListResultDto<FaultListDto>(faults.MapTo<List<FaultListDto>>());

        }

        public GetFaultDetailOutput GetDetail(int id)
        {

            var query = _faultRepository
             .GetAllIncluding()
             .Where(e => e.Id == id);

            var product = query.FirstOrDefault();
            if (product == null)
            {
                throw new UserFriendlyException("故障不存在");
            }
            return product.MapTo<GetFaultDetailOutput>();
        }
    }
}
