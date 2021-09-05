using System.Collections.Generic;

namespace DotNetCollege.EFCore.Sample3.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual List<AuthorBook> Books { get; set; }

    }
}