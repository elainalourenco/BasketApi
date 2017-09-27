﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        T InsertOrUpdate(T item);
        void Delete(int id);
    }
}
