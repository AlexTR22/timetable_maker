using System.Collections.Generic;

namespace TimetableMaker
{
    class Algorithm
    {
        private List<Chromosome> _population;
        public List<Chromosome> Population
        {
            get { return _population; }
            set { _population = value; }
        }

        public int NrChromosomes {  get; set; }
        public int NrGenerations { get; set; }
        public Config config { get; set; }

        //nu e un nume bun nici pentru clasa nici pentru camp
        //probabil nu trebuie sa fie deloc camp? de vazut
        private Schedule schedule;



        //!!!! DE MODIFICAT FUNCTIA DE MUTATIE SI DUPA POTI SA FACI RESTU DE CHESTII !!!!

        public Algorithm(int nrChromosomes = 2, int nrGenerations=10)
        {
            
            config = new Config();
            config.ReadCourseCLasses("data.txt");
            NrChromosomes = nrChromosomes;
            NrGenerations = nrGenerations;

            schedule = new Schedule(config);
            Population = new List<Chromosome>();
        }


        public void Start()
        {
            

            for (int i = 0; i < NrChromosomes; i++)
            {
                Chromosome chromosome = schedule.MakeNewChromosome(config);
                chromosome.Fitness = schedule.CalculateFitness(chromosome, config);
                Population.Add(chromosome);
            }


            for (int generation = 1; generation <= 100; generation++)
            {
                PriorityQueue<Chromosome, float> populationQueue= new PriorityQueue<Chromosome, float>();

                foreach (var chrom in Population)
                {
                    populationQueue.Enqueue(chrom, -chrom.Fitness);
                }

                for (int i = 0; i < NrChromosomes; i++)
                {
                    Chromosome parent1 = Population[i];
                    Chromosome parent2 = Population[(i + 1) % NrChromosomes]; 
                   
                    var offsprings= schedule.CrossoverFunction(parent1, parent2);
                    Chromosome offspring1= offsprings.Item1;
                    Chromosome offspring2 = offsprings.Item2;

                    schedule.MutationFunction(offspring1,config);
                    schedule.MutationFunction(offspring2, config);
                    offsprings.Item1.Fitness = schedule.CalculateFitness(offspring1, config);
                    offsprings.Item2.Fitness = schedule.CalculateFitness(offspring2, config);

                    populationQueue.Enqueue(offspring1, offspring1.Fitness);
                    populationQueue.Enqueue(offspring2, offspring2.Fitness);
                }

                Population.Clear();
                for (int i = 0; i < NrChromosomes; i++)
                {
                    Population.Add(populationQueue.Dequeue());
                }

                //Random rand = new Random();
               // for (int i = Population.Count - 1; i > 0; i--)
                //{
                 //   int j = rand.Next(i + 1);
                //    (Population[i], Population[j]) = (Population[j], Population[i]);
               // }
            }

            Console.WriteLine(Population[0].Fitness);
            Population[0].WriteSelf(config);
        }

        public Chromosome GetBestChromosome()
        {
            return Population[0];
        }

    }
}
