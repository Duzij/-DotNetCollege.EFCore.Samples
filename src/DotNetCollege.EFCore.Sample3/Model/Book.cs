using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample3.Model
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<AuthorBook> Authors { get; set; }
    }
}
