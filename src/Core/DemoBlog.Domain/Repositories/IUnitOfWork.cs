﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoBlog.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public IRepositoryManager RepositoryManager { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
