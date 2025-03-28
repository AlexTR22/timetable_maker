﻿using System.Collections.Generic;
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
        private int MutationProbability {  get; set; }

        private const int DAYS= 5;
        private const int HOURS = 6;
        
        //private int RoomsNumber { get; set; }
        private List<Room> _rooms;

        ChromosomeService ChromosomeService { get; set; }

        public UniversityTimetableMaker(int generations,int nrChromosomes, string universityName)
        {
            _population = new List<Chromosome>();
            Generations = generations;
            NrChromosomes = nrChromosomes;
            MutationProbability = 3;
            ChromosomeService = new ChromosomeService(universityName);

            _rooms = new List<Room>();
            _rooms= ChromosomeService.GetRoomsByUniversity();

            if (_population.Count == 0)
            {
                Chromosome c = ChromosomeService.GetCourseClaseesByUniversity();
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
            //initialize population
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
            //aici o sa trebuiasca sa se puna zi ora si sala pentru fiecare cromozom. Adica sa se initializeze cromozomii cu valori random.


            //foreach (Chromosome c in _population)
            //{ 
            //    c.Fitness =  CalculateFitness(c);
            //}
            
            //aici o sa incerc sa fac sa iau primii cei mai buni 2 cromozomii si sa le fac crossover, de asemenea functia de crossover aici o
            //sa imparta cromozomul in 3 parti egale si apoi pentru urmatoarea generatie o sa ie ia aleatoriu segmente din cei 2 cromozomi parinti
            // de asemenea crossover o sa returneze o lista de cromozomi care reprezinta noua generatie
            for (int generation = 1; generation <= 100; generation++)
            {
                foreach (Chromosome c in _population)
                {
                    c.Fitness = CalculateFitness(c);
                }

                _population.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));

                Chromosome parent1 = _population[0];
                Chromosome parent2 = _population[1];

                _population.Clear();

                _population = CrossoverFunction(parent1, parent2);
                for (int i = 0; i < _population.Count; i++)
                {
                    MutationFunction(_population[i]);
                }

                

            
            }



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

                       // aici o sa trebuiasca sa schimb chestiile care sunt puse random (sala, ora, zi)
            for (int i = 0; i < NrChromosomes; i++)
            {
                Chromosome newChromosome = new Chromosome();

                for (int j = 0; j < 3; j++)
                {
                    int aux = rng.Next(0, 2); // Alegem între parent1 și parent2
                    List<CourseClass> selectedGenes;

                    if (j == 0)
                    {
                        selectedGenes = (aux == 0)
                            ? parent1.Genes.GetRange(0, firstSplitPos)
                            : parent2.Genes.GetRange(0, firstSplitPos);
                    }
                    else if (j == 1)
                    {
                        selectedGenes = (aux == 0)
                            ? parent1.Genes.GetRange(firstSplitPos, secondSplitPos - firstSplitPos + 1)
                            : parent2.Genes.GetRange(firstSplitPos, secondSplitPos - firstSplitPos + 1);
                    }
                    else
                    {
                        selectedGenes = (aux == 0)
                            ? parent1.Genes.GetRange(secondSplitPos, parent1.Genes.Count - secondSplitPos)
                            : parent2.Genes.GetRange(secondSplitPos, parent2.Genes.Count - secondSplitPos);
                    }

                    newChromosome.Genes.AddRange(selectedGenes);
                }

              
                newPopulation.Add(newChromosome);
            }
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
