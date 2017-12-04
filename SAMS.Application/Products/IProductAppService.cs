using Abp.Application.Services.Dto;
using SAMS.Products.Dtos;
using System.Collections.Generic;
namespace SAMS.Products
{
    public interface IProductAppService
    {
        ListResultDto<ProductListDto> GetProducts();
        GetDetailOutput GetDetail(GetDetailInput input);
        //void Create(CreateInput input);
        //void Edit(EditInput input);
        //void Delete(int id);
        ListResultDto<ProductListDto> GetProductsByNameModel(string name, string model);
        ListResultDto<BomDto> GetBom(string productId);
        void UpdateBoms(string productId, IEnumerable<string> AccessoryIds);
        void AddBom(string productId, string accessoryId);
        void DeleteBom(string productId, string accessoryId);

    }
}