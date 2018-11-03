using ShopFashion.Model;
using ShopFashion.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Service
{
    public interface IEntityService
    {
        //IEnumerable<T> GetAll();
        //ListFeedback<T> GetAll(PageInfo pageInfo, string sortColumn, EnumCommon.SortDirection direction);
        //IQueryable<T> FindQuery();
        //IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        //T Create(T entity);
        //ListFeedback<T> Create(IList<T> entityList);
        //void Delete(T entity);
        //void DeleteBy(Expression<Func<T, bool>> predicate);
        //void Update(T entity);

        //void Update(IList<T> entityList);
    }
    public class EntityService : IEntityService
    {
        public readonly IUnitOfWork _unitOfWork;

        protected EntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public virtual IEnumerable<T> GetAll()
        //{
        //    return _repository.GetAll();
        //}

        //public ListFeedback<T> GetAll(PageInfo pageInfo, string sortColumn, EnumCommon.SortDirection direction)
        //{
        //    return _repository.GetAll(pageInfo, sortColumn, direction);
        //}

        //public IQueryable<T> FindQuery()
        //{
        //    return _repository.FindQuery();
        //}

        //public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        //{
        //    return _repository.FindBy(predicate);
        //}

        //public virtual T Create(T entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }
        //    T obj = _repository.Add(entity);
        //    _unitOfWork.Commit();
        //    return obj;
        //}

        //public ListFeedback<T> Create(IList<T> entityList)
        //{
        //    if (entityList == null)
        //    {
        //        throw new ArgumentNullException("entityList");
        //    }

        //    if (!entityList.Any())
        //    {
        //        return null;
        //        //throw new ArgumentOutOfRangeException("entityList");
        //    }

        //    ListFeedback<T> obj = _repository.Add(entityList);
        //    _unitOfWork.Commit();
        //    return obj;
        //}

        //public virtual void Update(T entity)
        //{
        //    if (entity == null) throw new ArgumentNullException("entity");
        //    _repository.Edit(entity);
        //    _unitOfWork.Commit();
        //}

        //public void Update(IList<T> entityList)
        //{
        //    if (entityList == null) throw new ArgumentNullException("entityList");

        //    if (!entityList.Any())
        //    {
        //        throw new ArgumentOutOfRangeException("entityList");
        //    }

        //    _repository.Edit(entityList);
        //    _unitOfWork.Commit();
        //}

        //public virtual void Delete(T entity)
        //{
        //    if (entity == null) throw new ArgumentNullException("entity");
        //    _repository.Delete(entity);
        //    _unitOfWork.Commit();
        //}

        //public void DeleteBy(Expression<Func<T, bool>> predicate)
        //{
        //    _repository.DeleteBy(predicate);
        //    _unitOfWork.Commit();
        //}
    }
}
