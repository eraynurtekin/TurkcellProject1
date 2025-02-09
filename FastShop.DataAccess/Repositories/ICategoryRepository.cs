﻿using FastShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastShop.DataAccess.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IList<Category> GetAll();
    }
}
