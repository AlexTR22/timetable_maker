using TimetableBackend.Model;

namespace TimetableBackend.Service
{
   // public class Chromosome;
    //this is the algorithm for making the timetable
    public class UniversityTimetableMaker
    {
        public List<Chromosome> _population;

        private int Generations { get; set; }
        private int NrChromosomes{ get; set; }
        private int ChromosomeSize { get; set; }
        private const int DAYS= 5;
        private const int HOUURS = 6;
        private int RoomsNumber { get; set; }
        private int MutationProbability {  get; set; }

        ChromosomeService ChromosomeService { get; set; }

        public UniversityTimetableMaker(int generations,int nrChromosomes, string universityName)
        {
            _population = new List<Chromosome>();
            Generations = generations;
            NrChromosomes = nrChromosomes;
            MutationProbability = 3;
            ChromosomeService = new ChromosomeService(universityName);


            if (_population.Count == 0)
            {
                Chromosome c = ChromosomeService.GetCourseClaseesByUniversity();
                _population.Add(c);
            }
            for (int it = 1; it < NrChromosomes; it++)
            {
                _population.Add(_population[0]);
            }
        }


        public Chromosome Run()
        {

            aici o sa trebuiasca sa se puna zi ora si sala pentru fiecare cromozom. Adica sa se initializeze cromozomii cu valori random.


            foreach (Chromosome c in _population)
            { 
                c.Fitness =  CalculateFitness(c);
            }
            
            //aici o sa incerc sa fac sa iau primii cei mai buni 2 cromozomii si sa le fac crossover, de asemenea functia de crossover aici o
            //sa imparta cromozomul in 3 parti egale si apoi pentru urmatoarea generatie o sa ie ia aleatoriu segmente din cei 2 cromozomi parinti
            // de asemenea crossover o sa returneze o lista de cromozomi care reprezinta noua generatie
            for (int generation = 1; generation <= 100; generation++)
            {
                _population.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));

                Chromosome parent1 = _population[0];
                Chromosome parent2 = _population[1];

                _population.Clear();

                _population = CrossoverFunction(parent1, parent2);
                for (int i = 0; i < _population.Count; i++)
                {
                    MutationFunction(_population[i]);
                }

                foreach (Chromosome c in _population)
                {
                    c.Fitness = CalculateFitness(c);
                }


                //if (generation > 10 && IsConverged(population))
                //{
                //    Console.WriteLine($"Converged at generation {generation}");
                //    break;
                //}
            }



            // deci aici iau toti membrii generatiei si le fac crossover
            //for (int generation = 1; generation <= 100; generation++)
            //{
            //    List<Chromosome> newPopulation = new List<Chromosome>(NrChromosomes * 3); 


            //    for (int i = 0; i < NrChromosomes; i++)
            //    {
            //        Chromosome parent1 = population[i];
            //        Chromosome parent2 = population[(i + 1) % NrChromosomes];


            //        var (offspring1, offspring2) = CrossoverFunction(parent1, parent2);

            //        MutationFunction(offspring1);
            //        MutationFunction(offspring2);


            //        offspring1.Fitness = CalculateFitness(offspring1);
            //        offspring2.Fitness = CalculateFitness(offspring2);

            //        newPopulation.Add(parent1);
            //        newPopulation.Add(offspring1);
            //        newPopulation.Add(offspring2);
            //    }


            //    newPopulation.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));


            //    population.Clear();
            //    for (int i = 0; i < NrChromosomes; i++)
            //    {
            //        population.Add(newPopulation[i]);
            //    }


            //    if (generation > 10 && IsConverged(population))
            //    {
            //        Console.WriteLine($"Converged at generation {generation}");
            //        break;
            //    }
            //}
            return _population[0];
        }

        public float CalculateFitness(Chromosome chromosome)
        {
            //deci o sa se verifice pentru toate genele cromozomului daca una dintre valorile din cele 3 (sala, zi, ora) daca e egal cu urmatorul
            //deci asta ar insemna 3 * nr de cromozomi care e = cu 3n .
            //intrebarea este ce scor sa fie 100% corect. 3n 

            //int nrRooms = configuration.Rooms.Count;
            //int nrStudentGroups = configuration.StudentGroups.Count;
            float result = chromosome.Genes.Count;// * 3;  //3 is the number of constraints ( right now it's only for days rooms and hours, so 3)
            float total = result;

            HashSet<CourseClass> seenGenes = new HashSet<CourseClass>();
            foreach (var gene in chromosome.Genes)
            {
                if (!seenGenes.Add(gene))
                {
                    result--;
                }
                //for(int j=i+1; j<chromosome.Genes.Count;j++)
                //{
                //    if (chromosome.Genes[i].Room == chromosome.Genes[j].Room)
                //    {
                //        result--;
                //    }
                //    if (chromosome.Genes[i].Room == chromosome.Genes[j].Room)
                //    {
                //        result--;
                //    }
                //    if (chromosome.Genes[i].Room == chromosome.Genes[j].Room)
                //    {
                //        result--;
                //    }
                //}
            }


            return result / total* 100.0f;
        }


        //for now i will make it so that each chromosome genes are split into 3
        public List<Chromosome> CrossoverFunction(Chromosome parent1, Chromosome parent2)
        {
            int firstSplitPos=parent1.Genes.Count/3, secondSplitPos= (parent1.Genes.Count-firstSplitPos)/2;
            Random rng = new Random();
            List<Chromosome> newPopulation = new List<Chromosome>();

            for (int i = 0; i < NrChromosomes; i++)
            {
                Chromosome newChromosome = new Chromosome();
                for (int j = 0; i < 3; j++)
                {
                    if (i == 0)
                    {
                        int aux = rng.Next(0, 2);
                        if (aux == 0)
                        {
                            newChromosome.Genes = parent1.Genes.GetRange(0, firstSplitPos);
                        }
                        else
                        {
                            newChromosome.Genes = parent2.Genes.GetRange(0, firstSplitPos);
                        }
                    }
                    if (i == 1)
                    {
                        int aux = rng.Next(0, 2);
                        if (aux == 0)
                        {
                            newChromosome.Genes = parent1.Genes.GetRange(firstSplitPos, secondSplitPos-firstSplitPos+1);
                        }
                        else
                        {
                            newChromosome.Genes = parent2.Genes.GetRange(firstSplitPos, secondSplitPos - firstSplitPos + 1);
                        }
                    }
                    if (i == 2)
                    {
                        int aux = rng.Next(0, 2);
                        if (aux == 0)
                        {
                            newChromosome.Genes = parent1.Genes.GetRange(secondSplitPos, parent1.Genes.Count-secondSplitPos);
                        }
                        else
                        {
                            newChromosome.Genes = parent2.Genes.GetRange(firstSplitPos, parent2.Genes.Count - secondSplitPos);
                        }
                    }
                }

                newPopulation.Add(newChromosome);
            }
            return newPopulation;
        }

        public Chromosome MutationFunction(Chromosome chromosome)
        {
       
            Random rnd = new Random();
            if (rnd.Next(32767) % 100 < MutationProbability)
            {

                aici ca si in algoritmul propriuzis o sa se seteze toate astea 3 cu valori random dar intai trebuie sa vad cum iau datele din baza de date
                int pos = rnd.Next(0, chromosome.Genes.Count);
                chromosome.Genes[pos].Day 
                chromosome.Genes[pos].Room 
                chromosome.Genes[pos].Duration 


            }
            return chromosome;
        
        }


    }
}
