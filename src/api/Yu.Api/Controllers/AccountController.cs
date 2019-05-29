﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Yu.Core.Constants;
using Yu.Core.Jwt;
using Yu.Core.Mvc;
using Yu.Model.Account;
using Yu.Model.Account.InputModels;
using Yu.Model.Account.Validators;
using Yu.Service.Account;

namespace Yu.Api.Controllers
{
    [Description("账户管理")]
    public class AccountController : AnonymousController
    {
        private readonly IJwtFactory _jwtFactory;

        private readonly IAccountService _accountService;

        public AccountController(IJwtFactory jwtFactory,IAccountService accountService)
        {
            _jwtFactory = jwtFactory;
            _accountService = accountService;
        }

        [HttpPost]
        [Description("用户名密码登陆")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            // 设计两个表,一个表是用来储存用户基本信息的,例如 昵称,头像等信息
            // 第二个表是用来对用户进行认证的即identiy的aspuser表
            // 需要对其进行字段的扩展 ,首先是用户类型:手机用户,邮箱用户,用户名登陆用户,微信用户,
            // 其次对每个用户类型添加其认证用的识别字段,例如telephone,email,username,openid
            // 登陆认证的过程中,例如email和username需要对数据库的密码凭证进行校验,例如验证passwordhash
            // 但是手机登陆或者微信第三方登陆都是通过微信api进行验证的情况下,则不需要调用identity的验证函数.
            // 在区分用户登陆方式上考虑采用以上方式

            // 针对app普通用户,商户用户,平台管理员等不同的用户角色考虑直接在userclaim上添加不同平台的区分
            // 例如后台运维用户则直接在用户声明里添加(userPlatformType,customerUser/bussinessUser/operatioinUser)

            var userName = await _accountService.FindUser(model.UserName, model.Password);

            if (string.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("UserName,Password", "用户名或密码错误0");
                return BadRequest(ModelState);
            }

            // 生成JwtToken
            var token = _jwtFactory.GenerateJwtToken(new List<(string,string)>{
                (CustomClaimTypes.UserName,userName)
            });

            return Ok(token);
        }

    }
}