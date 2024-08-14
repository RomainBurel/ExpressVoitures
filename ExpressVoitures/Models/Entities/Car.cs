using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Car
    {
        [Key]
        public int IdCar { get; set; }

        public string? VIN { get; set; }

        public int Year { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Finish { get; set; }

        public DateTime DateOfBuy { get; set; }

        public double Price { get; set; }

        public string? RepairDescription { get; set; }

        public DateTime? DateOfRepair { get; set; }

        public double RepairCost { get; set; }

        public bool NoMoreAvailable { get; set; }
    }
}
