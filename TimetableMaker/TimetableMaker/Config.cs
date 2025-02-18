

namespace TimetableMaker
{
    class Config
    {
        private List<CourseClass> _courseClasses;
        public List<CourseClass> CourseClasses
        {
            get { return _courseClasses; }
            set { _courseClasses = value; }
        }

        private List<Room> _rooms;
        public List<Room> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; }
        }

        private List<StudentGroup> _studentGroups;
        public List<StudentGroup> StudentGroups
        {
            get { return _studentGroups; }
            set { _studentGroups = value; }
        }
        public Config()
        {
            Rooms = new List<Room>();
            StudentGroups = new List<StudentGroup>();
            CourseClasses = new List<CourseClass>();
        }
        public void ReadCourseCLasses(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                if (fields.Length == 5)
                {
                    try
                    {
                        CourseClass course = new CourseClass();
                        course.Group.Name = fields[0].Trim();
                        course.Professor.Name = fields[1].Trim();
                        course.Course.Name = fields[2].Trim();
                        course.Duration = int.Parse(fields[3].Trim());

                        Room room = new Room();
                        room.Name = fields[4].Trim();
                        bool ok = false;
                        for (int i = 0; i < Rooms.Count; i++) 
                        {
                            if (Rooms[i].Name==room.Name)
                            {
                                ok = true; break;
                            }
                        }
                        if (ok==false)
                        {
                            Rooms.Add(room);
                        }
                        ok = false;
                        for (int i = 0; i < StudentGroups.Count; i++)
                        {
                            if (StudentGroups[i].Name == course.Group.Name)
                            {
                                ok = true; break;
                            }
                        }
                        if (ok == false)
                        {
                            StudentGroups.Add(course.Group);
                        }
                       
                        CourseClasses.Add(course);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Linie invalidă: {line}");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong format for the input data!");
                }

            }
        }


    }
}
