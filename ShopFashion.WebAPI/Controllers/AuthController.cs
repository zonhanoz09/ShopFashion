using ShopFashion.Model.NotMapping;
using ShopFashion.Service.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopFashion.WebAPI.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IDemoService _demoService;
        private readonly IAuthService _authService;

        public AuthController(IDemoService demoService, IAuthService authService)
        {
            _demoService = demoService;
            _authService = authService;
        }

        [Route("api/auth")]
        [HttpGet]
        public async Task<string> GetName()
        {
            UserRegisterModel userRegisterModel = new UserRegisterModel();
            userRegisterModel.Email = "huynhvannhancntt@gmail.com";
            userRegisterModel.Password = "123456";
            userRegisterModel.ConfirmPassword = "123456";
            var name = await _authService.RegisterUser(userRegisterModel);
            return "";
        }
    }
}
