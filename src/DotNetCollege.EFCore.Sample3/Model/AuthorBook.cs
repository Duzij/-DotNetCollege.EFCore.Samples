using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample3.Model
{
    public class AuthorBook
    {
        public int Id { get; set; }

        public int? BookId { get; set; }
        public int? AuthorId { get; set; }

    }
}
