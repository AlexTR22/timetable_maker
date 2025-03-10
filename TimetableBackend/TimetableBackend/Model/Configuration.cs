namespace TimetableBackend.Model
{
    public class Configuration
    {
        //private List<CourseClass> _courseClasses;
        //public List<CourseClass> CourseClasses
        //{
        //    get { return _courseClasses; }
        //    set { _courseClasses = value; }
        //}

        private List<Room> _rooms;
        public List<Room> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; }
        }

        private List<Group> _groups;
        public List<Group> Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }

        public List<int> Days {  get; set; }

        public List<int> Hours { get; set; }
    }
}
