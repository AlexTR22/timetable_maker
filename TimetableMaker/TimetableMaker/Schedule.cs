using System.Drawing;

namespace TimetableMaker
{
    class Schedule
    {
        int DAYS_NUM = 2;
        int DAY_HOURS = 5;
        private int _numberOfCrossoverPoints;
        public int NumberOfCrossoverPoints
        {

            get { return _numberOfCrossoverPoints; }
            set { _numberOfCrossoverPoints = value; }
        }

        //nr of courseClasses that are moved randomly, mutat
        //ex: if i have the cromosome 1 2 3 4 5 6, and mutationSize = 1, then we can change 
        private int _mutationSize;
        public int MutationSize
        {
            get { return _mutationSize; }
            set { _mutationSize = value; }
        }

        private float _crossoverProbability;
        public float CrossoverProbability
        {
            get { return _crossoverProbability; }
            set { _crossoverProbability = value; }
        }


        private float _mutationProbability;
        public float MutationProbability
        {
            get { return _mutationProbability; }
            set { _mutationProbability = value; }
        }

        private int size;
        //aici ma gandesc la mutationSize ca ge se face doar o data intershimbarea a 2 biti din chromozom, daca ar fi 2 atunci s-ar luat o 
        // pereche de biti random si s-ar schimba apoi alta pozitie random si s-ar schimba
        public Schedule(Config configuration, int numberOfCrossoverPoints=1, int mutationSize=1, float crossoverProbability=0.4f, float mutationProbability=0.03f)
        {
            NumberOfCrossoverPoints = numberOfCrossoverPoints;
            CrossoverProbability = crossoverProbability;
            MutationSize = mutationSize;
            MutationProbability = mutationProbability;
            size = DAYS_NUM * DAY_HOURS * configuration.Rooms.Count;
        }


        public /*List<CourseClass>*/Chromosome MakeNewChromosome(Config configuration)
        {
            Random rnd= new Random();
            
            Chromosome newChromosome = new Chromosome(size); //= new List<CourseClass>(size);
            for(int i=0;i<size;i++)
            {
                newChromosome.Gene.Add(null);
            }
            List<CourseClass> courseClasses = new List<CourseClass>();
            courseClasses = configuration.CourseClasses;

            for(int it=0;it<courseClasses.Count;it++)
            {
                int nrRooms = configuration.Rooms.Count;
                int dur = courseClasses[it].Duration;
                int day = rnd.Next(32767)% DAYS_NUM;
                int room = rnd.Next(32767)% nrRooms;
                int time = rnd.Next(32767)%(DAY_HOURS + 1 - dur);
                int pos = day * nrRooms * DAY_HOURS + room * DAY_HOURS + time;

                if (pos + dur - 2 < size)
                {
                    //foru asta era daca sunt duratii diferite, da nu imi trebuie asa ceva o sa fie duratie fixa de o ora
                    //for (int i = 0; i < dur; i++)
                    //{

                    if (newChromosome.Gene[pos] == null)
                    {
                        newChromosome.Gene[pos] = courseClasses[it];
                    }
                    else
                    {
                        it--;
                    }
                    //}
                }
                //else
                //{
                //    it--;
                //}
                //Slots.Add(newChromosome);
            }

            return newChromosome;
        }

        public (Chromosome, Chromosome) CrossoverFunction(Chromosome chromosome1, Chromosome chromosome2)
        {
            Random rnd = new Random();
            if (rnd.Next(32767) % 100 < CrossoverProbability)
            {
                Chromosome aux1 = new Chromosome(size);
                Chromosome aux2 = new Chromosome(size);
                int prevPos = 0;
                for (int i=0;i<NumberOfCrossoverPoints;i++)
                {
                    int pos = rnd.Next(32767) % chromosome1.Gene.Count;

                    if (prevPos % 2 == 0)
                    {
                        aux1.Gene.AddRange(chromosome1.Gene.GetRange(prevPos,pos));
                        aux2.Gene.AddRange(chromosome2.Gene.GetRange(prevPos,pos));
                    }
                    else
                    {
                        aux1.Gene.AddRange(chromosome2.Gene.GetRange(prevPos,pos));
                        aux2.Gene.AddRange(chromosome1.Gene.GetRange(prevPos, pos));
                    }
                    prevPos = pos;

                }
                chromosome1 = aux1; chromosome2 = aux2;
            }
            return (chromosome1, chromosome2);
        }

        public Chromosome MutationFunction(Chromosome chromosome, Config config)
        {
            Random rnd = new Random();
            if (rnd.Next(32767) % 100 < MutationProbability)
            {
                int pos1= rnd.Next(0,chromosome.Gene.Count);
                int pos2= rnd.Next(0,chromosome.Gene.Count);

                var auxPos1 = chromosome.Gene[pos1];
                chromosome.Gene[pos1] = chromosome.Gene[pos2];
                chromosome.Gene[pos2] = auxPos1;
            }
            return chromosome;
        }
        //in algoritml asta se ia un nr intre 0 si nr total de courseClasses, si se face o randomizare si se pun in alte locatii, si se sterg din pozitia veche
        //ar trebui sa se ia o pozitie random din chromozom (un bit/ sau mutationSize de biti) si sa isi schimbe pozitia random
        //public Chromosome MutationFunction(Chromosome chromosome, Config configuration)
        //{

        //    Random rnd = new Random();
        //    if (rnd.Next(32767) % 100 < MutationProbability)
        //    {
        //        List<CourseClass> courseClasses = new List<CourseClass>();
        //        courseClasses = configuration.CourseClasses;
        //        bool del = false;
        //        if (courseClasses.Count >= MutationSize)
        //        {
        //            del = true;
        //        }
        //        for (int i = 0; i < MutationSize; i++)
        //        {
        //            int pos = rnd.Next(32767) % courseClasses.Count;

        //            foreach (CourseClass c in courseClasses)
        //            {
        //                int nrRooms = configuration.Rooms.Count;
        //                int dur = c.Duration;
        //                int day = rnd.Next(32767) % DAYS_NUM;
        //                int room = rnd.Next(32767) % nrRooms;
        //                int time = rnd.Next(32767) % (DAY_HOURS + 1 - dur);
        //                int coursePos = day * nrRooms * DAY_HOURS + room * DAY_HOURS + time;
        //                if (chromosome.Gene[coursePos] == null)
        //                {
        //                    chromosome.Gene[coursePos] = courseClasses[pos];
        //                }
        //                for (int it = 0; it < chromosome.Gene.Count; i++)
        //                {
        //                    if (chromosome.Gene[it] == courseClasses[pos])
        //                    {
        //                        chromosome.Gene[it] = null;
        //                    }
        //                }
        //            }
        //            if (del)
        //            {
        //                courseClasses.Remove(courseClasses[pos]);
        //            }
        //        }
        //    }
        //    return chromosome;
        //}


        public float CalculateFitness(Chromosome chromosome,Config configuration)
        {
            //float fitness = 0.0f;

            int dayNum = DAYS_NUM;
            int dayHours = DAY_HOURS;
            int nrRooms = configuration.Rooms.Count;
            int nrStudentGroups = configuration.StudentGroups.Count;
            float score = (dayNum * dayHours * nrRooms);
            //List<List<List<int>>> dayRoomsHoursList = new List<List<List<int>>>();

            //if (nrRooms >= nrStudentGroups)
            //{
             //   score += 1.0f;
            //}

            Dictionary<(int day, int hour), List<CourseClass>> schedule=OrganizeChromosome(chromosome,dayNum,dayHours,nrRooms);
            for(int i = 0;i<dayNum;i++)
            {
                for(int j = 0;j<dayHours;j++)
                {
                    int result=CompareLabs(schedule, i, j);
                    //if(result != -1)
                    //{
                        score+=result;
                    //}
                }
            }

            return (score/(dayNum*dayHours*nrRooms))*100.0f;
        }

        public Dictionary<(int day, int hour), List<CourseClass>> OrganizeChromosome(
        Chromosome chromosome, int daysNum, int dayHours, int nrRooms)
        {
            // Inițializează dicționarul
            var schedule = new Dictionary<(int day, int hour), List<CourseClass>>();

            // Parcurge toate zilele, orele și sălile
            for (int day = 0; day < daysNum; day++)
            {
                for (int hour = 0; hour < dayHours; hour++)
                {
                    // Inițializează o listă pentru toate laboratoarele din săli
                    var labsAtThisTime = new List<CourseClass>();

                    for (int room = 0; room < nrRooms; room++)
                    {
                        // Calculează poziția în cromozom
                        int pos = day * (nrRooms * dayHours) + room * dayHours + hour;

                        if(chromosome.Gene.Count==20)
                        // Adaugă laboratorul din această poziție (dacă există)
                        if (chromosome.Gene[pos] != null)
                        {
                            labsAtThisTime.Add(chromosome.Gene[pos]);
                        }
                    }

                    // Adaugă informațiile pentru ziua și ora curentă în dicționar
                    schedule[(day, hour)] = labsAtThisTime;
                }
            }

            return schedule;
        }

        public int CompareLabs(Dictionary<(int day, int hour), List<CourseClass>> schedule, int day, int hour)
        {
            if (!schedule.ContainsKey((day, hour)))
            {
                Console.WriteLine($"Nu există laboratoare la ziua {day}, ora {hour}");
                return 0;
            }
            int score = 0;
            var labs = schedule[(day, hour)];
            for (int i = 0; i < labs.Count; i++)
            {
                for (int j = i + 1; j < labs.Count; j++)
                {
                    //Console.WriteLine($"Compar laboratorul {labs[i].Course} cu {labs[j].Course}");
                    if (labs[i].Professor.Name == labs[j].Professor.Name || labs[i].Group.Name == labs[j].Group.Name)
                    {
                        score--;
                    }
                    //else
                    //{
                    //    score++;
                    //}
                }
            }
            return score;
        }
    }
}