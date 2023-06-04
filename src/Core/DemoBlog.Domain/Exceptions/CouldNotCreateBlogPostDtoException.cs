using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoBlog.Contracts;

namespace DemoBlog.Domain.Exceptions
{
    public class CouldNotCreateBlogPostDtoException : BadRequestException
    {
        public CouldNotCreateBlogPostDtoException() : 
            base($"Failed to create {nameof(CreateBlogPostDto)}")
        { }
    }
}
