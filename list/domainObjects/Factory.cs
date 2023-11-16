using System;
using System.Diagnostics;
using list;

namespace domainObjects
{

    /// <summary>
    /// Creates collections of objects 
    /// </summary>
    public class Factory {

        /// <summary>
        /// Creats a collection of people
        /// </summary>
        public static Person[] CreatePeople() {
            string[] firstNames = { "María", "Juan", "Pepe", "Luis", "Carlos", "Miguel", "Cristina", "María", "Juan" };
            string[] surnames = { "Díaz", "Pérez", "Hevia", "García", "Rodríguez", "Pérez", "Sánchez", "Díaz", "Hevia" };
            string[] idNumbers = { "9876384A", "103478387F", "23476293R", "4837649A", "67365498B", "98673645T", "56837645F", "87666354D", "9376384K" };

            Debug.Assert(firstNames.Length == surnames.LongLength && surnames.Length == idNumbers.Length);
            Person[] people = new Person[firstNames.Length];
            for (int i = 0; i < people.Length; i++)
                people[i] = new Person(firstNames[i], surnames[i], idNumbers[i]);
            return people;
        }

        public static List<Person> CreatePeopleList()
        {
            List<Person> list = new List<Person>();
            foreach (Person person in CreatePeople())
                list.Add(person);
            return list;
        }

        /// <summary>
        /// Creates a collection of angles from 0 to 360 degrees
        /// </summary>
        public static Angle[] CreateAngles() {
            Angle[] angles = new Angle[361];
            for(int angle=0; angle<=360; angle++)
                angles[angle] = new Angle(angle/180.0*Math.PI);
            return angles;
        }

        public static List<Angle> CreateAnglesList()
        {
            List<Angle> list = new List<Angle>();
            foreach (Angle angle in CreateAngles())
                list.Add(angle);
            return list;
        }

    }
}
