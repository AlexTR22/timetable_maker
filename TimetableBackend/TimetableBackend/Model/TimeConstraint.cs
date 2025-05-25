using System.Xml.Linq;

namespace TimetableBackend.Model
{
    public class TimeConstraint
    {
        public int Id {  get; set; }
        public int ProfessorID { get; set; }
        public int FromHour {  get; set; }
        public int ToHour { get; set; }
        public int Day { get; set; }
        public int College { get; set; }

        public TimeConstraint()
        {
        }

        public TimeConstraint(TimeConstraint other)
        {
            Id = other.Id;
            ProfessorID = other.ProfessorID;
            FromHour = other.FromHour; 
            ToHour = other.ToHour;
            Day = other.Day;
            College = other.College;
        }
    }
}
