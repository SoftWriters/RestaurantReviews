namespace Models
{
    public class CityModel : ICityModel
    {
        public CityModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}