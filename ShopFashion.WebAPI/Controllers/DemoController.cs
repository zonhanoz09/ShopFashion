using ShopFashion.Service.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopFashion.WebAPI.Controllers
{
    public class DemoController : ApiController
    {

        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            var x = _demoService.Demo1();
            var check = _demoService.Insert("NhanHV111");
            return new string[] { "value1", "value2" };
        }

        // PUT api/demo/5
        public void Post(string name, [FromBody]string value)
        {
            var check = _demoService.Insert(name);
        }
    }
}
