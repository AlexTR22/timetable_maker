namespace TimetableBackend.Model
{
    public class Chromosome
    {
        //aici trebuie sa fie HashSet
        public List<CourseClass> Genes { get; set; }
        public float Fitness {  get; set; }

        public Chromosome() 
        {
            Genes = new List<CourseClass>();
            Fitness = 0;
        }


    }
}
