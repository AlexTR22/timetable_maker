using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class ChromosomeToTimetable
    {
        public class TimetableSlot
        {
            public string Subject { get; set; }
            public string Professor { get; set; }
            public string Room { get; set; }
            public string Group { get; set; }
        }

        public class TimetableGrid
        {
            public Dictionary<int, List<TimetableSlot>> WeekdaySlots { get; set; }

            public TimetableGrid()
            {
                WeekdaySlots = new Dictionary<int, List<TimetableSlot>>();

                for (int day = 0; day < 5; day++) // Luni - Vineri
                {
                    var slots = new List<TimetableSlot>();

                    // Inițializează fiecare zi cu 6 sloturi goale (null sau obiecte goale)
                    for (int i = 0; i < 6; i++)
                    {
                        slots.Add(new TimetableSlot()); // sau slots.Add(new TimetableSlot()); dacă vrei obiecte necompletate
                    }

                    WeekdaySlots[day] = slots;
                }
            }
        }

        public class GroupTimetable
        {
            public string GroupName { get; set; }
            public TimetableGrid Timetable { get; set; }
        }

        public List<GroupTimetable> GetTimetableForFrontend(List<CourseClass> courseClasses)
        {
           
            var groupTimetables = new List<GroupTimetable>();

            
            var groupedCourses = courseClasses.GroupBy(cc => cc.Group.Name);


            foreach (var group in groupedCourses)
            {

                var timetable = new TimetableGrid();

                foreach (var courseClass in group)
                {
                    int hour = courseClass.Hour;
                    int dayOfWeek = courseClass.Day;

                    timetable.WeekdaySlots[dayOfWeek][hour].Subject = courseClass.Course.Name;
                    timetable.WeekdaySlots[dayOfWeek][hour].Professor = courseClass.Professor.Name;
                    timetable.WeekdaySlots[dayOfWeek][hour].Room = courseClass.Room.Name;          
                    timetable.WeekdaySlots[dayOfWeek][hour].Group = courseClass.Group.Name;
                }

                // Add this group's timetable to the result list
                groupTimetables.Add(new GroupTimetable
                {
                    GroupName = group.Key,  // Group name (e.g., "CS101")
                    Timetable = timetable   // The actual timetable for the group
                });
            }

            return groupTimetables;
        }
    }
}
