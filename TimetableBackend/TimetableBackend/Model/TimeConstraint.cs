using System.Xml.Linq;

namespace TimetableBackend.Model
{
    public class TimeConstraint
    {
        public int Id {  get; set; }
        public int ProfessorId { get; set; }
        public int FromHour {  get; set; }
        public int ToHour { get; set; }
        public int Day { get; set; }
        public int CollegeId { get; set; }

        public TimeConstraint()
        {
        }

        public TimeConstraint(TimeConstraint other)
        {
            Id = other.Id;
            ProfessorId = other.ProfessorId;
            FromHour = other.FromHour; 
            ToHour = other.ToHour;
            Day = other.Day;
            CollegeId = other.CollegeId;
        }
    }
}
