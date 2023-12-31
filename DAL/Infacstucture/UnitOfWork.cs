﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Infacstucture
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private FRMDbContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public FRMDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = this._dbFactory.Init();
                }
                return _dbContext;
            }
        }
        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public async Task commitAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
