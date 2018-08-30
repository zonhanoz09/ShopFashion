﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ShopFashion.Model;
using ShopFashion.Model.Base;
using ShopFashion.Model.Classes;

namespace ShopFashion.Repository.Shared
{
    public interface IRepository<T> where T : BaseEntity
    {
        //find
        IQueryable<T> FindQuery();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);


        //get all
        IEnumerable<T> GetAll();
        //ListFeedback<T> GetAll(PageInfo pageInfo, string sortColumn, string direction);
        //ListFeedback<T> GetByQuery(PageInfo pageInfo);

        //add
        T Add(T entity);
        //ListFeedback<T> Add(IList<T> entityList);


        //update
        void Update(T entity);
        void Update(IList<T> entityList);


        //delete
        T Delete(T entity);
        void DeleteBy(Expression<Func<T, bool>> predicate);


        //commit
        void Commit();
        Task<int> CommitAsync();
    }
    public class Repository<T> : IRepository<T>
           where T : BaseEntity
    {
        protected ShopFashionContext Context;
        protected readonly IDbSet<T> Dbset;

        public Repository(DbContext context)
        {
            Context = (ShopFashionContext)context;
            Dbset = context.Set<T>();
        }


        //find
        public IQueryable<T> FindQuery()
        {
            return Dbset.AsQueryable();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = Dbset.Where(predicate);
            return query;
        }


        //get all
        public virtual IEnumerable<T> GetAll()
        {
            return Dbset.AsEnumerable<T>();
        }

        //public ListFeedback<T> GetAll(PageInfo pageInfo, string sortColumn, string direction)
        //{
        //    var total = Dbset.Count();
        //    var query = Dbset.OrderByCustom(sortColumn, direction)
        //        .Skip((pageInfo.PageNo - 1) * pageInfo.PageSize)
        //        .Take(pageInfo.PageSize);

        //    List<T> groups = new List<T>();

        //    foreach (var item in query)
        //    {
        //        groups.Add(item);
        //    }

        //    return new ListFeedback<T>(true, groups, total);
        //}
        //public ListFeedback<T> GetByQuery(PageInfo pageInfo)
        //{
        //    var query = Dbset.Where(s => s.IsDeleted == false);
        //    //var obj = _repository.FindBy(s => s.IsDeleted == false).AsQueryable();
        //    var count = 0;
        //    if (pageInfo != null)
        //    {
        //        if (pageInfo.Fields.Count > 0)
        //        {
        //            for (int index = 0; index < pageInfo.Fields.Count; index++)
        //            {
        //                if (!string.IsNullOrEmpty(pageInfo.Fields[index]) && !string.IsNullOrEmpty(pageInfo.Fieldvalues[0]))
        //                    query = query.FilterCustom(pageInfo.Fields[index], pageInfo.Fieldvalues[index]);
        //            }
        //        }

        //        if (string.IsNullOrEmpty(pageInfo.OrderBy))
        //            pageInfo.OrderBy = Constants.OrderByDefault;
        //        count = query.Count();
        //        query = query.OrderByCustom(pageInfo.OrderBy, pageInfo.Direction)
        //            .Skip((pageInfo.PageNo - 1) * pageInfo.PageSize)
        //            .Take(pageInfo.PageSize);
        //    }

        //    return new ListFeedback<T>(true, query.ToList(), count);
        //}


        //add
        public virtual T Add(T entity)
        {
            entity.InsertedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = false;
            return Dbset.Add(entity);
        }

        //public virtual ListFeedback<T> Add(IList<T> entityList)
        //{
        //    List<T> addedList = new List<T>();

        //    foreach (T entity in entityList)
        //    {
        //        entity.InsertedAt = DateTime.Now;
        //        entity.UpdatedAt = DateTime.Now;
        //        entity.IsDeleted = false;

        //        T addedEntity = Dbset.Add(entity);

        //        addedList.Add(addedEntity);
        //    }

        //    return new ListFeedback<T>(true, addedList, addedList.Count);
        //}


        //update
        public virtual void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = false;
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(IList<T> entityList)
        {
            foreach (var entity in entityList)
            {
                entity.UpdatedAt = DateTime.Now;
                entity.IsDeleted = false;

                Context.Entry(entity).State = EntityState.Modified;
            }
        }


        //delete
        public virtual T Delete(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = true;

            return Dbset.Remove(entity);
        }

        public void DeleteBy(Expression<Func<T, bool>> predicate)
        {
            var lstEntities = Dbset.Where(predicate).ToList();
            foreach (var entity in lstEntities)
            {
                entity.UpdatedAt = DateTime.Now;
                entity.IsDeleted = true;
                //Dbset.Remove(entity);
            }
        }


        //commit
        public virtual void Commit()
        {
            Context.SaveChanges();
        }

        public virtual async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
