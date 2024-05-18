namespace Webtest.Data
{
    public class WorldCarsCSV
    {
        public int manufacturer_id { get; set; }
        public string manufacturer_name { get; set; } = null!;
        public string manufacturer_country { get; set; } = null!;
        public int car_id { get; set; }
        public string car_name { get; set; } = null!;
        public string car_drivetrain { get; set; } = null!;
        public int car_year { get; set; }
    }
}