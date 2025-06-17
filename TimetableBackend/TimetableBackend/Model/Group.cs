namespace TimetableBackend.Model
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int CollegeId { get; set; }
       
        public Group(Group other)
        {
            Id = other.Id;
            Name = other.Name;
            Year = other.Year;
            CollegeId = other.CollegeId;
        }

        public Group()
        {
        }
    }
}
