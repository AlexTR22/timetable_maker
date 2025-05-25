namespace TimetableBackend.Model
{
    public class College
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UniverisyId { get; set; }

        public College(){}
        public College(College other)
        {
            Id = other.Id;
            Name = other.Name;
            UniverisyId = other.UniverisyId;
        }

    }
}
