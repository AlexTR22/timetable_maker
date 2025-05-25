namespace TimetableBackend.Model
{
    public class Subject 
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Year {  get; set; }
        public int IdProfessor { get; set; }
        public int IdCollege { get; set; }
        public bool Semester { get; set; }

        public Subject() { }
        public Subject(Subject other)
        {
            Id = other.Id;
            Name = other.Name;
            Year = other.Year;
            IdProfessor= other.IdProfessor;
            IdCollege = other.IdCollege;
            Semester = other.Semester;
        }
    }
}
