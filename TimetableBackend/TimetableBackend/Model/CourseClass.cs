using Microsoft.AspNetCore.Http.HttpResults;

namespace TimetableBackend.Model
{
    public class CourseClass
    {
        public CourseClass() 
        {
            Professor = new Professor();
            Group = new Group();
            Course = new Course();
            Room = new Room();
        }
        public CourseClass(CourseClass other)
        {
            Professor = new Professor(other.Professor); // Assuming Professor has a copy constructor
            Duration = other.Duration;
            Group = new Group(other.Group); // Assuming Group has a copy constructor
            Course = new Course(other.Course); // Assuming Course has a copy constructor
            Room = new Room(other.Room); // Assuming Room has a copy constructor
            Day = other.Day;
            Hour = other.Hour;
        }

        private Professor _professor;
        public Professor Professor
        {
            get { return _professor; }
            set { _professor = value; }
        }
        private int _duration;
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        private Group _group;
        public Group Group
        {
            get { return _group; }
            set { _group = value; }
        }

        private Course _course;
        public Course Course
        {
            get { return _course; }
            set { _course = value; }
        }

        private Room _room;
        public Room Room
        {
            get { return _room; }
            set { _room = value; }
        }

        private int _day;
        public int Day
        {
            get { return _day; }
            set { _day = value; }
        }

        private int _hour;
        public int Hour
        {
            get { return _hour; }
            set { _hour = value; }
        }

        //aici o sa fie fals daca oricare dintre campuri se aseamana cu unul din campurile lui obj respectiv
        //asta rezulta intr-o verificare mai putin amanuntita si deci, care poate sa dea un fitness mai putin exact
        // !!!!!!!!!! de retinut ca sunt sanse sa necesite o schimbare daca vreau o acuratete mai buna !!!!!!!!!!!!!
        public override bool Equals(object? obj)
        {
            if (obj is CourseClass courseClass2)
            {
                // Compara ziua, ora, camera, profesorul și grupul
                return this.Day == courseClass2.Day &&
                       this.Hour == courseClass2.Hour &&
                       this.Room.Name == courseClass2.Room.Name &&
                       this.Professor.Name == courseClass2.Professor.Name &&
                       this.Group.Name == courseClass2.Group.Name;
            }

            return false;
        }


        public override int GetHashCode()
        {
            unchecked // Permite overflow fără aruncare de excepții
            {
                int hash = 17;
                hash=hash* 23 + Day.GetHashCode();
                hash=hash* 23 + Hour.GetHashCode();
                hash=hash* 23 + (Professor!=null ? Professor.Name.GetHashCode():0);
                hash=hash* 23 + (Course!=null ? Course.Name.GetHashCode():0);
                hash=hash* 23 + (Room!=null ? Room.Name.GetHashCode():0);
                hash=hash* 23 + (Group!=null ? Group.Name.GetHashCode():0);
                return hash;
            }
        }



    }
}
