using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBlog.Domain.Exceptions
{
    public class UpdateMissingBlogPostException : BadRequestException
    {
        public UpdateMissingBlogPostException() : 
            base($"No blog post provide for update")
        {
        }
    }
}
