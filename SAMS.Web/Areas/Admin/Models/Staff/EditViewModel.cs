using Abp.AutoMapper;
using SAMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Models.Staff
{
    [AutoMapFrom(typeof(GetUserForEditOutput))]
    public class EditViewModel : GetUserForEditOutput
    {
        public EditViewModel(GetUserForEditOutput output)
        {
            output.MapTo(this);
        }
        public List<SelectListItem> RoleSelectList
        {
            get
            {
                var rolesItemList = new List<SelectListItem>();

                rolesItemList.AddRange(
                    Roles.Select(r=> new SelectListItem()
                    {
                        Text =r.RoleDisplayName,
                        Value =r.RoleName,
                        Selected =r.IsAssigned
                    }
                    )
                 
                 );
                
                return rolesItemList;
            }
        }
    }
}