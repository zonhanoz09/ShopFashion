using AutoMapper;
using ShopFashion.Model.Classes;
using ShopFashion.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Service.Configuration
{
    public class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DemoTable, DemoTableService>();
            });
        }
    }
}