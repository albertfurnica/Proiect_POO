namespace ProiectPO.Domain.Vehicles
{
    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public Car(int id, string brand, string model, double pricePerHour, int numberOfDoors) : base(id, brand, model,
            pricePerHour)
        {
            NumberOfDoors = numberOfDoors;
        }

        public override string GetVehicleType()
        {
            return "Car";
        }
    }
}