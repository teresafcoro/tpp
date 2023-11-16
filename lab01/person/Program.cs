using System;

namespace lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonTO p1 = new PersonTO { FirstName = "Pepe", Surname = "Gomez", DateOfBirth = new DateTime(1999, 1, 1) };
            PersonTO p2 = new PersonTO { FirstName = "Maria", Surname = "Gomez", Gender = Gender.Female };
            Console.WriteLine("Person p1: ", p1);
            Console.WriteLine("Person p2: ", p2);
        }
    }
}