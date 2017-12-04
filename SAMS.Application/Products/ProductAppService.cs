using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;

using SAMS.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Products
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product,string> _productRepository;
        private readonly IRepository<Bom> _bomRepository;
        public ProductAppService(IRepository<Product,string> productRepository,
            IRepository<Bom> bomRepository)
        {
            _productRepository = productRepository;
            _bomRepository = bomRepository;
        }
        public ListResultDto<BomDto>GetBom(string productId)
        {
            var bom = _bomRepository.GetAllIncluding().Where(e => e.ProductId == productId).ToList<Bom>();
            return new ListResultDto<BomDto>(bom.MapTo<List<BomDto>>());

        }
        public void DeleteBom(string productId,string accessoryId)
        {
            var bom=_bomRepository.GetAll()
                .Where(e => e.AccessoryId == accessoryId)
                .Where(e => e.ProductId == productId)
                .SingleOrDefault();
            _bomRepository.Delete(bom);
        }
        public void AddBom(string productId,string accessoryId)
        {
            var bom = _bomRepository.GetAll()
               .Where(e => e.AccessoryId == accessoryId)
               .Where(e => e.ProductId == productId)
               .SingleOrDefault();
            if(bom==null)
            {
                bom = new Bom() { AccessoryId = accessoryId, ProductId = productId };
                _bomRepository.Insert(bom);
            }
        }
        public void UpdateBoms(string productId, IEnumerable<string> AccessoryIds)
        {
            var oldBoms = _bomRepository.GetAll().Where(e => e.ProductId == productId).ToList();
            //删除
            var delBoms= oldBoms.Where(e => !AccessoryIds.Contains(e.AccessoryId));
            foreach(var delBom in delBoms)
            {
                _bomRepository.Delete(delBom);
            }
            //新增
            foreach( string id in AccessoryIds)
            {
                var bom=oldBoms.Where(o => o.AccessoryId == id).SingleOrDefault();
                if(bom==null)
                {
                    bom = new Bom() { AccessoryId = id, ProductId = productId };
                    _bomRepository.Insert(bom);
                }
            }
        }

        public ListResultDto<ProductListDto> GetProducts()
        {
            var products = _productRepository.GetAll().OrderByDescending(e => e.Code);
            return new ListResultDto<ProductListDto>(products.MapTo<List<ProductListDto>>());

        }
        public ListResultDto<ProductListDto> GetProductsByNameModel(string name, string model)
        {
            var products = _productRepository.GetAll().Where(e => e.Name == name).Where(e => e.Model == model).ToList();
            return new ListResultDto<ProductListDto>(products.MapTo<List<ProductListDto>>());
        }

        public GetDetailOutput GetDetail(GetDetailInput input)
        {

            var query = _productRepository
             .GetAllIncluding()
             .Where(e => e.Id == input.Id);

            var product = query.FirstOrDefault();
            if (product == null)
            {
                throw new UserFriendlyException("产品不存在");
            }
            return product.MapTo<GetDetailOutput>();
        }
        //public void Create(CreateInput input)
        //{
        //    var product = new Product()
        //    {
        //        EASNumber=input.EASNumber,
        //        K3Number=input.K3Number,
        //        Model=input.Model,
        //        Name=input.Name

        //    };
        //    _productRepository.Insert(product);

        //}
        //public void Edit(EditInput input)
        //{
        //    var product = _productRepository.Get(input.Id);
        //    product.K3Number = input.K3Number;
        //    product.EASNumber = input.EASNumber;
        //    product.Name = input.Name;
        //    product.Model = input.Model;
        //}
        //public void Delete(int id)
        //{
        //     _productRepository.Delete(id);
        //}
    }
}
