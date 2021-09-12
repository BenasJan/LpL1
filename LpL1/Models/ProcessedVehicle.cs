namespace LpL1.Models
{
    public class ProcessedVehicle : Vehicle
    {
        public string Hash { get; set; }

        public override string ToString()
        {
            return $"|{Manufacturer, -15}|{Model, -30}|{YearManufactured, 4}|{Price, 7}|{VinNumber, 17}|{Hash, 5}|";
        }
    }
}