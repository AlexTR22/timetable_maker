namespace TimetableBackend.Model
{
    public class Chromosome
    {
        
        public List<SubjectClass> Genes { get; set; }
        public float Fitness {  get; set; }

        public Chromosome() 
        {
            Genes = new List<SubjectClass>();
            Fitness = 0;
        }

        public Chromosome(Chromosome other)
        {            
            Genes = Genes = other.Genes.Select(Subject => new SubjectClass(Subject)).ToList();
            Fitness = other.Fitness;
        }
    }
}
