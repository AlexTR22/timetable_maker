using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMaker
{
    class Chromosome
    {
        public Chromosome(int size) 
        {
            Gene = new List<CourseClass>(size);
        }

        public List<CourseClass> Gene {  get; set; }
        public float Fitness {  get; set; }

        public void WriteSelf(Config config)
        {
            int lab = 0;
            for (int i = 0; i < Gene.Count; i++)
            {

                if (i % config.Rooms.Count == 0 && i!=0)
                {
                    if ((lab + 1) % config.Rooms.Count == 0)
                    {
                        lab = 0;
                    }
                    else
                    {
                        lab++;
                    }
                }
                if (Gene[i] != null)
                {
                   
                    Console.Write(lab + "    ");
                    Gene[i].WriteSelf();
                }
                else
                {
                    Console.Write(lab + "    ");
                    Console.WriteLine("null");
                }
                
            }
        }
    }
}
