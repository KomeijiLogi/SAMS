
using Abp.Application.Services.Dto;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using Newtonsoft.Json;
using SAMS.Roles;
using SAMS.Users;
using SAMS.Users.Dto;
using SAMS.Users.Dtos;
using SAMS.Web.Areas.Admin.Models.Common;
using SAMS.Web.Areas.Admin.Models.Staff;
using SAMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Controllers
{
      [AbpMvcAuthorize(Roles ="Admin")]
    public class StaffController: SAMSControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly UserManager _userManager;
        private readonly IRoleAppService _roleAppService;
        public StaffController(IUserAppService userAppService, UserManager userManager, IRoleAppService roleAppService)
        {
            _userAppService = userAppService;
            _userManager = userManager;
            _roleAppService = roleAppService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public  PartialViewResult Get(GetListParam param)
        {
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetViewModel();
           
            string name=null;
            if(param.Where!=null)
            {
                var where = JsonConvert.DeserializeObject<GetStaffWhere>(param.Where);
                name = where.Name;
            }
            var input = new GetUsersInput() { MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting, Name = name };

            viewModel.Staffs = AsyncHelper.RunSync(()=> _userAppService.GetUsers(input));
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;

            return PartialView("_Get", viewModel);
        }
        //新建
        public ActionResult Create()
        {
            var roles = _roleAppService.GetRoles();
            var rolesItemList = new List<SelectListItem>();

            rolesItemList.AddRange(new SelectList(roles.Items.ToArray(), "DisplayName", "DisplayName", ""));

            ViewData["roles"] = rolesItemList;
            return View("Create");
        }
        //编辑
        public async Task<ActionResult> Edit(long id)
        {

            var user = await _userAppService.GetUserForEdit(new NullableIdDto<long> { Id = id });
            var viewModel = new EditViewModel(user);
         
            return View("Edit", viewModel);
        }
        [HttpPost]
        public async Task<ContentResult> Delete(long[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                await _userAppService.DeleteUser(ids[i]);
            }
            return Content("ok");
        }
        [HttpPost]
        public async Task<ContentResult> Create(Staff staff)
        {
            var createInput = new CreateOrUpdateUserInput()
            {
                User = new UserEditDto() {
                    IsActive = true,
                    Mobile = staff.Mobile,
                    Name = staff.Name,
                    Password = "",
                    EmailAddress = staff.UserName+"@test.com",
                    Surname = "null",
                    UserName =staff.UserName,
                    ShouldChangePasswordOnNextLogin=false
                }
                ,
                AssignedRoleNames = new string[] { staff.RoleName}
            };
            await _userAppService.CreateOrUpdateUser(createInput);
            return Content("ok");
        }
        [HttpPost]
        public async Task<ContentResult> Edit(Staff staff)
        {
            var editInput = new CreateOrUpdateUserInput()
            {
                User = new UserEditDto()
                {
                    Id = staff.ID,
                    IsActive = true,
                    Mobile = staff.Mobile,
                    Name = staff.Name,
                    Password = "",
                    EmailAddress = "test@test.com",
                    Surname = "null",
                    UserName = staff.UserName,
                    ShouldChangePasswordOnNextLogin = false
                },
                AssignedRoleNames = new string[] { staff.RoleName }
            };
            await _userAppService.CreateOrUpdateUser(editInput);

            return Content("ok");
        }

    }
}