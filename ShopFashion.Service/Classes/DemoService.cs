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
        bool Insert(string name);

        string GetDemoByName(string name);
    }
    public class DemoService : IDemoService, IEntityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DemoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetDemoByName(string name)
        {
            var result = _unitOfWork.DemoRepository.Get(
                filter : x =>x.Name ==name);
            return result.ToString();
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
