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
    public class DemoService : EntityService<DemoTable>, IDemoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemoRepository _demoRepository;

        public DemoService(IUnitOfWork unitOfWork,IDemoRepository demoRepository)
            : base(unitOfWork, demoRepository)
        {
            _demoRepository = demoRepository;
        }
        public int Demo1()
        {
            return 12;
        }

        public bool Insert(string name)
        {
            Model.Classes.DemoTable demoTable = new Model.Classes.DemoTable();
            demoTable.Name = name;
            _demoRepository.Add(demoTable);
            _demoRepository.Commit();
            return true;
        }
    }
}
