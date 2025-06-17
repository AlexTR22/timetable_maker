namespace TimetableBackend.Model
{
    public class Subject 
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Year {  get; set; }
        public int CollegeId { get; set; }
        public int ProfessorId { get; set; }
        public bool Semester { get; set; }

        public Subject() { }
        public Subject(Subject other)
        {
            Id = other.Id;
            Name = other.Name;
            Year = other.Year;
            ProfessorId = other.ProfessorId;
            CollegeId = other.CollegeId;
            Semester = other.Semester;
        }
    }
}
    