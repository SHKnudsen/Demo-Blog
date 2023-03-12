using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlog.Application.Interfaces.Configuration
{
    public interface IDatabaseConnection
    {
        string ConnectionString { get; set; }
    }
}
