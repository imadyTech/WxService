using System;
using System.Collections.Generic;
using System.Text;
using imady.Domain;
using imady.WxContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace imady.Repository
{
    public class WxDataRepository<T> : IWxDataRepository<T> where T: BaseEntity
    {
        private IWxBaseContext<T> Context;
        private DbSet<T> Entities
        {
            get { return this.Context.Set(); }
        }

        #region ====== 构造函数（需要传入一个泛型DbContext）======
        public WxDataRepository() { }

        public WxDataRepository(IWxBaseContext<T> context)
        {
            this.Context = context;
        }
        #endregion

        #region ====== 析构函数 ======
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }
        #endregion

        //==================================================

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var course = Context.Set().Find(id);
            Entities.Remove(course);
        }

        public T Get(Guid id)
        {
            var entity = Context.Set().Find(id);
            return entity;
        }

        public void Create(T entity)
        {
            Context.Set().Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.AsEnumerable<T>();
        }

    }

}
