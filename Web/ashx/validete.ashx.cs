﻿using BookShop.BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShop.Model;
using BookShop.Model.Enum;

namespace BookShop.Web
{
    /// <summary>
    /// validete 的摘要说明
    /// </summary>
    public class validete : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string type = context.Request.QueryString["type"];
            switch (type)
            {
                case "userName":
                    valideteUserName(context);
                    break;
                case "userEmail":
                    valideteUserEmail(context);
                    break;
                default:
                case "vCode":
                    valideteCode(context);
                    break;
                case "register":
                    valideteRegister(context);
                    break;
            }
        }
        /// <summary>
        /// 验证注册的基础信息
        /// </summary>
        /// <param name="context"></param>
        private void valideteRegister(HttpContext context)
        {
            User user = new User()
            {
                Address = context.Request.Form["txtAddress"],
                LoginId = context.Request.Form["txtUserName"],
                LoginPwd = context.Request.Form["txtConfirmPwd"],
                Mail = context.Request.Form["txtEmail"],
                Name = context.Request.Form["txtRealName"],
                Phone = context.Request.Form["txtPhone"]
            };
            user.UserState.Id = Convert.ToInt32(UserStateEnum.NormalState);
        }

        /// <summary>
        /// 判断验证码是否正确
        /// </summary>
        /// <param name="context"></param>
        private void valideteCode(HttpContext context)
        {
            string vCode = context.Request.Form["vCode"];
            bool isSuccess = Common.Common.valideteCode(vCode);
            context.Response.Write(isSuccess ? "ok" : "no");
        }

        /// <summary>
        /// 验证邮箱是否已经存在
        /// </summary>
        /// <param name="context"></param>
        private void valideteUserEmail(HttpContext context)
        {
            string userEmai = context.Request.Form["userEmail"];
            UserManager userManager = new UserManager();
            context.Response.Write(userManager.ValideteEmail(userEmai) ? "ok" : "no");
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        private void valideteUserName(HttpContext context)
        {
            string userName = context.Request.Form["userName"];
            UserManager userManager = new UserManager();
            context.Response.Write(userManager.Exists(userName) ? "ok" : "no");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}