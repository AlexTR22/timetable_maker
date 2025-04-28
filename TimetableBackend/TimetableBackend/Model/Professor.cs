namespace TimetableBackend.Model
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Professor()
        {
        }

        public Professor(Professor other)
        {
            Id = other.Id;
            Name = other.Name;
        }
    }
}
