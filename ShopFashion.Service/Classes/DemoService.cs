using ShopFashion.Model.Classes;
using ShopFashion.Repository.Classes;
using ShopFashion.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Service.Classes
{
    public interface IDemoService
    {
        int Demo1();

        bool Insert(string name);
    }
    public class DemoService : IDemoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DemoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int Demo1()
        {
            return 12;
        }

        public bool Insert(string name)
        {
            Model.Classes.DemoTable demoTable = new Model.Classes.DemoTable();
            demoTable.Name = name;
            _unitOfWork.DemoRepository.Insert(demoTable);
            _unitOfWork.Save();
            return true;
        }
    }
}
