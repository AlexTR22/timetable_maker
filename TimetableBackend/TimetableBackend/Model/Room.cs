namespace TimetableBackend.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int CollegeId { get; set; }

        public Room()
        { 
        }

        public Room(Room other)
        {
            Id = other.Id;
            Name = other.Name;
            Capacity = other.Capacity;
            CollegeId = other.CollegeId;
        }
    }
}
