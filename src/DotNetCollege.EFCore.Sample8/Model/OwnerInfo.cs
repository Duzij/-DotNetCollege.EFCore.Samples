using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCollege.EFCore.Sample8.Model
{
    
    [Owned]
    public class OwnerInfo
    {
        public int Id { get; set; }
        public int CarDetailId { get; set; }
        public string Info { get; set; }
    }

}
