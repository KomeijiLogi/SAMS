using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Customers.Dtos
{
    public class CreateInput
    {

        
        public string Name { get; set; }


        public string Number1 { get; set; }


        public string Number2 { get; set; }

     
        public string Area { get; set; }//黑龙江省-大庆市-肇州镇


        public string Address { get; set; }


        public string Description { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// EMail
        /// </summary>
        public string Email { get; set; }
        //public DateTime CreationTime { get; set; }
    }
}
