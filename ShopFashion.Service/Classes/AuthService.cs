using ShopFashion.Model.NotMapping;
using ShopFashion.Repository.Classes;
using ShopFashion.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Service.Classes
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(UserRegisterModel userRegisterModel);
    }
    public class AuthService : IAuthService, IEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;

        public AuthService(IUnitOfWork unitOfWork, IAuthRepository authRepository)
        {
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;
        }

        public async Task<bool> RegisterUser(UserRegisterModel userRegisterModel)
        {
           var result = await _authRepository.RegisterUser(userRegisterModel);
            return false;
        }
    }
}
