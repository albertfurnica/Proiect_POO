using ProiectPO.Domain.Enums;

namespace ProiectPO.Domain.Vehicles
{
    public abstract class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public double PricePerHour { get; set; }
        public VehicleStatus Status { get; set; }

        public Vehicle(int id, string model, string brand, double pricePerHour)
        {
            Id = id;
            Model = model;
            Brand = brand;
            PricePerHour = pricePerHour;
            Status=VehicleStatus.Available;
        }

        public abstract string GetVehicleType();
    }
    
}