namespace TimetableBackend.Model
{
    public class Course 
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Year {  get; set; }
        public int IdProfessor { get; set; }
    }
}
