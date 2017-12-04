using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Customers
{
    public interface ICustomerDomainService :IDomainService
    {
        /// <summary>
        /// 新建客户
        /// </summary>
        /// <param name="customer"></param>
        void Create(Customer customer);
        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="customer"></param>
       // void Update(Customer customer);
        /// <summary>
        /// 根据客户Id取得客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer Get(string id);


    }
}
