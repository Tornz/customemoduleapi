using Microsoft.AspNetCore.Mvc;
using CustomeModule.API.HelperExtension;
using CustomeModule.Interfaces.HelperExtensions.Interfaces;
using CustomeModule.Interfaces.Services.Interface;
using CustomeModule.Model.Dto;
using CustomeModule.Model.Model;
using CustomeModule.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CustomeModule.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IUserService _usersService;

        public TokenController(IUserService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]LoginVM login)
        {
            var response = new SingleModelResponse<User>() as ISingleModelResponse<User>;
            try
            {
                var validated = _usersService.CheckAccess(login.email, login.password);
                if (validated.userid == 0)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "N";
                    response.Message = null;
                    response.Data = null;
                }
                else
                {
                    var subject = validated.name;
                    var token = new JwtTokenBuilder()
                                    .AddSecurityKey(JwtSecurityKey.Create("CustomeModule-secret-key"))
                                    .AddSubject(subject)
                                    .AddIssuer("CustomeModule.Security.Bearer")
                                    .AddAudience("CustomeModule.Security.Bearer")
                                    .AddClaim("SecurityCustomeModule", "001")
                                    .AddExpiry(5)                                   
                                    .Build();
                    validated.username = "";
                    validated.password = "";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                    response.Message = token.Value;
                    response.Data = validated;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = "N";
                response.Message = ex.Message;
            }
            return response.ToHttpResponse();
        }
    }
}
