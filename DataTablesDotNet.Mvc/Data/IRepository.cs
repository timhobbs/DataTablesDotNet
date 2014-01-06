using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataTablesDotNet.Mvc.Data {

    internal interface IRepository<T> {

        T Get(int id);

        List<T> GetAll();

        List<T> GetAllWhere(Expression<Func<T, bool>> predicate);
    }
}