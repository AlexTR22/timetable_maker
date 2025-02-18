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

        public void WriteSelf()
        {
            for (int i = 0; i < Gene.Count; i++)
            {
                if (Gene[i] != null)
                {
                    Gene[i].WriteSelf();
                }
                else
                {
                    Console.WriteLine("null");
                }
                
            }
        }
    }
}
