using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using imady.Domain;

namespace imady.WxContext
{
    public interface IWxBaseContext<T> where T : BaseEntity
    {

        DbSet<T> Set();
        int SaveChanges();
        void Dispose();
    }
}