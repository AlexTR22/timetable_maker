namespace TimetableBackend.Model
{
    public class Course 
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Year {  get; set; }
        public int IdProfessor { get; set; }
        public Course() { }
        public Course(Course other)
        {
            Id = other.Id;
            Name = other.Name;
            Year = other.Year;
            IdProfessor= other.IdProfessor;
        }
    }
}
