using System.Collections.Generic;
using System.Diagnostics;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
   // public class Chromosome;
    //this is the algorithm for making the timetable
    public class UniversityTimetableMaker
    {
        public List<Chromosome> _population;
        private Helper _helper;

        private int Generations { get; set; }
        private int NrChromosomes{ get; set; }
        private int ChromosomeSize { get; set; }
        private int MutationProbability {  get; set; }

        private const int DAYS= 4;
        private const int HOURS = 6;
        
        //private int RoomsNumber { get; set; }
        private List<Room> _rooms;

        ChromosomeService ChromosomeService { get; set; }

        public UniversityTimetableMaker(int generations,int nrChromosomes, string collegeName, bool semester, int year, Helper helper)
        {
            _helper = helper;

            _population = new List<Chromosome>();
            Generations = generations;
            NrChromosomes = nrChromosomes;
            MutationProbability = 3;
            ChromosomeService = new ChromosomeService(helper, collegeName, semester, year);

            //_rooms = new List<Room>();
            _rooms= ChromosomeService.GetRoomsByCollege();

            if (_population.Count == 0)
            {
                Chromosome c = ChromosomeService.GetSubjectClassesByUniversity();
                _population.Add(c);
            }

            //initializarea asta de la populatie pot sa o fac si in algoritmul propriu zis, pentru ca nu are legatura in mod special cu constructorul
            //for (int it = 1; it < NrChromosomes; it++)
            //{
            //    _population.Add(_population[0]);
            //}
        }


        public Chromosome Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int it = 1; it < NrChromosomes; it++)
            {
                _population.Add(_population[0]);
            }
            Random rng = new Random();
            for (int i = 0; i < NrChromosomes; i++)
            { 
                for(int j = 0; j < _population[i].Genes.Count;j++)
                {
                    _population[i].Genes[j].Room=_rooms[rng.Next(_rooms.Count)];
                    _population[i].Genes[j].Day= rng.Next(DAYS);
                    _population[i].Genes[j].Hour= rng.Next(HOURS);
                }
            }
            

            foreach (Chromosome c in _population)
            { 
                c.Fitness =  CalculateFitness(c);
            }
            
            for (int generation = 1; generation <= 100; generation++)
            {
               

                _population.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));

                Chromosome parent1 = new Chromosome(_population[0]);
                Chromosome parent2 = new Chromosome(_population[1]);

                //_population.Clear();
                var chromosomes= CrossoverFunction(parent1, parent2);
                for (int i=0;i<chromosomes.Count;i++)
                {
                    _population[i] = new Chromosome(chromosomes[i]);
                }
                
                for (int i = 0; i < _population.Count; i++)
                {
                    _population[0]=new Chromosome(MutationFunction(_population[i]));
                }


                foreach (Chromosome c in _population)
                {
                    c.Fitness = CalculateFitness(c);
                }

            }

            _population.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));
            
            stopwatch.Stop();
            Console.WriteLine($"Timp de execuție: {stopwatch.ElapsedMilliseconds} ms");

            return _population[0];
        }

        public float CalculateFitness(Chromosome chromosome)
        {
            
            float result = chromosome.Genes.Count;
            float total = result;

            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                for (int j = i + 1; j < chromosome.Genes.Count; j++)
                {
                    
                    if (chromosome.Genes[i].Equals(chromosome.Genes[j]))
                    {
                        result--;
                        //verify constraints
                        break;
                        
                    }
                }
            }

            return result / total* 100.0f;
        }


        //for now i will make it so that each chromosome genes are split into 3

        //wring adding in the gene class
        public List<Chromosome> CrossoverFunction(Chromosome parent1, Chromosome parent2)
        {
            Random rng = new Random();
            int firstCrossoverPoint = rng.Next(1, parent1.Genes.Count);  
            int secondCrossoverPoint = rng.Next(firstCrossoverPoint + 1, parent1.Genes.Count);
            List<Chromosome> newPopulation = new List<Chromosome>();

            Chromosome child1 = new Chromosome();
            Chromosome child2 = new Chromosome();

            
            for (int i = 0; i < parent1.Genes.Count; i++)
            {
                // Dacă indexul este în intervalul de crossover, schimbă doar câmpurile zi, ora, room
                if (i >= firstCrossoverPoint && i < secondCrossoverPoint)
                {
                    // Schimbă câmpurile zi, ora și room între părinți
                    var tempZi = parent1.Genes[i].Day;
                    var tempOra = parent1.Genes[i].Hour;
                    var tempRoom = parent1.Genes[i].Room;

                    child1.Genes.Add(new SubjectClass
                    {
                        Professor= parent1.Genes[i].Professor,
                        Group= parent1.Genes[i].Group,
                        Subject=parent1.Genes[i].Subject,
                        Day = parent2.Genes[i].Day,
                        Hour = parent2.Genes[i].Hour,
                        Room = parent2.Genes[i].Room
                    });

                    child2.Genes.Add(new SubjectClass
                    {
                        Professor = parent1.Genes[i].Professor,
                        Group = parent1.Genes[i].Group,
                        Subject = parent1.Genes[i].Subject,
                        Day = tempZi,
                        Hour = tempOra,
                        Room = tempRoom
                    });
                }
                else
                {
                    // Altfel, lasă genele nemodificate (copiază direct din părinți)
                    child1.Genes.Add(new SubjectClass(parent1.Genes[i]));
                    child2.Genes.Add(new SubjectClass(parent2.Genes[i]));
                }
            }
            newPopulation.Add(child1);
            newPopulation.Add(child2);
            //           // aici o sa trebuiasca sa schimb chestiile care sunt puse random (sala, ora, zi)
            //for (int i = 0; i < NrChromosomes; i++)
            //{
            //    Chromosome newChromosome = new Chromosome();

            //    for (int j = 0; j < 3; j++)
            //    {
            //        int aux = rng.Next(0, 2); // Alegem între parent1 și parent2
            //        List<SubjectClass> selectedGenes;

            //        if (j == 0)
            //        {
            //            selectedGenes = (aux == 0)
            //                ? parent1.Genes.GetRange(0, firstSplitPos)
            //                : parent2.Genes.GetRange(0, firstSplitPos);
            //        }
            //        else if (j == 1)
            //        {
            //            selectedGenes = (aux == 0)
            //                ? parent1.Genes.GetRange(firstSplitPos, secondSplitPos - firstSplitPos + 1)
            //                : parent2.Genes.GetRange(firstSplitPos, secondSplitPos - firstSplitPos + 1);
            //        }
            //        else 
            //        {
            //            selectedGenes = (aux == 0)
            //                ? parent1.Genes.GetRange(secondSplitPos, parent1.Genes.Count - secondSplitPos)
            //                : parent2.Genes.GetRange(secondSplitPos, parent2.Genes.Count - secondSplitPos);
            //        }

            //        newChromosome.Genes.AddRange(selectedGenes);
            //    }


            //    newPopulation.Add(newChromosome);
            //}
            return newPopulation;
        }

        public Chromosome MutationFunction(Chromosome chromosome)
        {
       
            Random rng = new Random();
            if (rng.Next(32767) % 100 < MutationProbability)
            {

                //aici ca si in algoritmul propriuzis o sa se seteze toate astea 3 cu valori random dar intai trebuie sa vad cum iau datele din baza de date
                int pos = rng.Next(0, chromosome.Genes.Count);
                chromosome.Genes[pos].Room = _rooms[rng.Next(_rooms.Count)];
                chromosome.Genes[pos].Day = rng.Next(DAYS);
                chromosome.Genes[pos].Hour = rng.Next(HOURS);

            }
            return chromosome;
        
        }


    }
}
