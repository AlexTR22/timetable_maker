using Microsoft.AspNetCore.Http.HttpResults;

namespace TimetableBackend.Model
{
    public class SubjectClass
    {
        public SubjectClass() 
        {
            Professor = new Professor();
            Group = new Group();
            Subject = new Subject();
            Room = new Room();
        }
        public SubjectClass(SubjectClass other)
        {
            Professor = new Professor(other.Professor); // Assuming Professor has a copy constructor
            Duration = other.Duration;
            Group = new Group(other.Group); // Assuming Group has a copy constructor
            Subject = new Subject(other.Subject); // Assuming Subject has a copy constructor
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

        private Subject _Subject;
        public Subject Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
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
            if (obj is SubjectClass SubjectClass2)
            {
                // Compara ziua, ora, camera, profesorul și grupul
                return this.Day == SubjectClass2.Day &&
                       this.Hour == SubjectClass2.Hour &&
                       this.Room.Name == SubjectClass2.Room.Name &&
                       this.Professor.Name == SubjectClass2.Professor.Name &&
                       this.Group.Name == SubjectClass2.Group.Name;
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
                hash=hash* 23 + (Subject!=null ? Subject.Name.GetHashCode():0);
                hash=hash* 23 + (Room!=null ? Room.Name.GetHashCode():0);
                hash=hash* 23 + (Group!=null ? Group.Name.GetHashCode():0);
                return hash;
            }
        }



    }
}
