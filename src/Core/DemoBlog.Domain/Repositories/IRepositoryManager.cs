using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBlog.Domain.Repositories;

public interface IRepositoryManager
{
    IBlogDbRepository BlogRepository { get; }
}
