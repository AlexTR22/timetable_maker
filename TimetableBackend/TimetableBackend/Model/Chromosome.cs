namespace TimetableBackend.Model
{
    public class Chromosome
    {
        //aici trebuie sa fie HashSet
        public List<SubjectClass> Genes { get; set; }
        public float Fitness {  get; set; }

        public Chromosome() 
        {
            Genes = new List<SubjectClass>();
            Fitness = 0;
        }

        public Chromosome(Chromosome other)
        {
            // Deep copy the Genes using the SubjectClass copy constructor
            Genes = Genes = other.Genes.Select(Subject => new SubjectClass(Subject)).ToList();

            // Copy the Fitness value
            Fitness = other.Fitness;
        }
    }
}
