using Abp;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Microsoft.AspNet.Identity;
using SAMS.Sessions;
using SAMS.Users;
using SAMS.Web.Areas.Admin.Models.Personal;
using SAMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Controllers
{
      [AbpMvcAuthorize(Roles ="Admin")]
    public class PersonalController : SAMSControllerBase
    {
        private readonly UserManager _userManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IAbpSession _abpSession;
        public PersonalController(ISessionAppService sessionAppService, UserManager userManager, IAbpSession abpSession)
        {
            _userManager = userManager;
            _sessionAppService = sessionAppService;
            _abpSession = abpSession;
        }
        public ActionResult EditPassword()
        {
            return View(new EditPasswordViewModel() { ID = _userManager.AbpSession.UserId.Value });
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ContentResult> EditPassword(EditPasswordViewModel model)
        {
            IdentityResult result = await _userManager.ChangePasswordAsync(model.ID, model.OldPwd, model.NewPwd);
            if (result.Succeeded)
            {
                return Content("ok");
            }
            else
            {
                return Content("fail");
            }
        }
        [DontWrapResult]
        public JsonResult CheckPwd(string　OldPwd)
        {
            var user = _userManager.FindById(_abpSession.UserId.Value);
            bool isSuccess=_userManager.CheckPassword(user, OldPwd);
            return Json(isSuccess,JsonRequestBehavior.AllowGet);
            
        }
    }
}