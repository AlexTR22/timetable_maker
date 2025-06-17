using Microsoft.AspNetCore.Http.HttpResults;

namespace TimetableBackend.Model
{
    public class SubjectClass
    {
        public Professor Professor { get; set; }

        public Group Group { get; set; }

        public Subject Subject { get; set; }

        public Room Room { get; set; }

        public int Day { get; set; }

        public int Hour { get; set; }

        
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
            
            Group = new Group(other.Group); // Assuming Group has a copy constructor
            Subject = new Subject(other.Subject); // Assuming Subject has a copy constructor
            Room = new Room(other.Room); // Assuming Room has a copy constructor
            Day = other.Day;
            Hour = other.Hour;
        }

        
      
       
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
