using Microsoft.EntityFrameworkCore;

namespace DotNetCollege.EFCore.Sample8.Model
{
    [Owned]
    public class AdditionalDetails
    {
        public int CarDetailId { get; set; }
        public string VIN { get; set; }
        public string OrderCode { get; set; }
        public string DealerCode { get; set; }
    }
}
