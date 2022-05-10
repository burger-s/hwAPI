using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace hwAPI.Model.Core
{
    public interface ITheRepository<T>
    {
        // api/[GET]
        Task<IEnumerable<T>> GetAll();

        // api/1/[GET]
        Task<T> Get<TFeild>(Expression<Func<T, TFeild>> func, TFeild id);

        // api/[POST]
        Task Create(T Tobj);

        // api/[PUT]
        Task<bool> Update(T Tobj, Expression<Func<T, bool>> func);

        // api/1/[DELETE]
        Task<bool> Delete<TFeild>(Expression<Func<T, TFeild>> func, TFeild id);

        Task<long> GetNextId();
    }
}
