using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimetableMaker
{
    class CourseClass
    {
        private Professor _professor;
        public Professor Professor
        {
            get { return _professor; }
            set { _professor = value; }
        }
        private int _duration;
        public int Duration
        {
            get { return _duration; } 
            set { _duration = value; } 
        }

        //private Room _room;
        //public Room Room
        //{
        //    get { return _room; }
        //    set { _room = value; }
        //}

        private StudentGroup? _group;
        public StudentGroup? Group
        {
            get { return _group; }
            set { _group = value; }
        }

        private Course _course;
        public Course Course
        {
            get { return _course; }
            set { _course = value; }
        }

        public CourseClass()
        {
            Duration= 0;
            Group = new StudentGroup();
            Course = new Course();
            Professor = new Professor();
            Duration = 1;
            Group.Name = "";
            Course.Name = "";
            Professor.Name = "";
        }
        public void WriteSelf()
        {
            Console.Write(Group.Name + " ");     
            Console.Write(Professor.Name + " ");    
            Console.Write(Course.Name + " ");    
            Console.WriteLine();
        }
    }
}
