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

        public Chromosome(Chromosome other)
        {
            // Deep copy the Genes using the CourseClass copy constructor
            Genes = Genes = other.Genes.Select(course => new CourseClass(course)).ToList();

            // Copy the Fitness value
            Fitness = other.Fitness;
        }
    }
}
