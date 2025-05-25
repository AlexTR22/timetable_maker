namespace TimetableBackend.Model
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public University() { }
        public University(University other) 
        {
            Id = other.Id;
            Name = other.Name;
            CityName = other.CityName;
        }

    }
}
