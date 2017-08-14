using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Events.Bus;
using Abp.Threading;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SAMS.Authorization;
using SAMS.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Mvc.Authorization
{
    public class AbpTicketMvcAuthorizeFilter: AbpMvcAuthorizeFilter
    {
        private readonly LogInManager _logInManager;
        private readonly UserManager _userManager;
        public AbpTicketMvcAuthorizeFilter(IAuthorizationHelper authorizationHelper,
            IErrorInfoBuilder errorInfoBuilder, 
            IEventBus eventBus, 
            LogInManager logInManager,    
            UserManager userManager)
            :base(authorizationHelper,errorInfoBuilder,eventBus)
        {

            _logInManager = logInManager;
            _userManager = userManager;
        }
        override public  void OnAuthorization(AuthorizationContext filterContext)
        {
            //云之家ticket登录
            var ticket = filterContext.RequestContext.HttpContext.Request.QueryString["ticket"];
            
            if (!string.IsNullOrWhiteSpace(ticket))
            {
                string appID = ConfigurationManager.AppSettings["appID"];//轻应用注册到云之家时生成
                string appSecret = ConfigurationManager.AppSettings["appSecret"];//轻应用注册到云之家时生成
                string yunxt = ConfigurationManager.AppSettings["yunxt"];
                var loginResult = AsyncHelper.RunSync(() => _logInManager.LoginAsync(ticket, appID, appSecret,yunxt));
                if(loginResult.Result== AbpLoginResultType.Success)
                {
                    AsyncHelper.RunSync(() => SignInAsync(loginResult.User,  filterContext, loginResult.Identity));
                }
            }
            base.OnAuthorization(filterContext);
        }
        private async Task SignInAsync(User user, AuthorizationContext filterContext,ClaimsIdentity identity = null  )
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            filterContext.RequestContext.HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            filterContext.RequestContext.HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
        }
    }
}