using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample8.Model
{
    [Table("Cars")]
    public class Car
    {
        public int Id { get; set; }

        [Column("Model")]
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public CarDetail CarDetail { get; set; }

        public AdditionalDetails AdditionalDetails { get; set; }
    }


    [Table("Cars")]
    public class CarDetail
    {
        public int Id { get; set; }

        //Shared 
        [Column("Model")]
        public string Model { get; set; }

        //CarDetails own manufacturer
        public string Manufacturer { get; set; }

        public Transmission Transmission { get; set; }
        
        public FuelType Fuel { get; set; }

        public AdditionalDetails AdditionalDetails { get; set; }

        public List<OwnerInfo> PreviousOwners { get; set; }
    }

    public enum FuelType
    {
        Gasoline,
        Diesel
    }

    public enum Transmission
    {
        Manual,
        Automatic
    }
}
