using System;

namespace TPP.Laboratory.ObjectOrientation.Lab03 {

    // Al crear un método extensor, la clase debe ser static.
    static class Algorithms {

        // Exercise: Implement an IndexOf method that finds the first position (index) 
        // of an object in an array of objects. It should be valid for Person, Angle and future classes.
        public static int? IndexOf(this object[] elements, object target) {
            int index = 0;
            foreach (object element in elements) {
                if (element.Equals(target))
                    return index;
                index++;
            }
            return null;
        }

        public static int? IndexOf2(this object[] elements, object target, IEqualityPredicate predicate = null)
        {
            int index = 0;
            foreach (object element in elements)
            {
                if (SelectAndCheck(element, target, predicate))
                    return index;
                index++;
            }
            return null;
        }

        private static bool SelectAndCheck(object element, object target, IEqualityPredicate predicate)
        {
            if (predicate == null)
                return element.Equals(target);
            else
                return predicate.Compare(element, target);
        }

        static Person[] CreatePeople() {
            string[] firstnames = { "María", "Juan", "Pepe", "Luis", "Carlos", "Miguel", "Cristina" };
            string[] surnames = { "Díaz", "Pérez", "Hevia", "García", "Rodríguez", "Pérez", "Sánchez" };
            int numberOfPeople = 100;

            Person[] printOut = new Person[numberOfPeople];
            Random random = new Random();
            for (int i = 0; i < numberOfPeople; i++)
                printOut[i] = new Person(
                    firstnames[random.Next(0, firstnames.Length)],
                    surnames[random.Next(0, surnames.Length)],
                    random.Next(9000000, 90000000) + "-" + (char)random.Next('A', 'Z')
                    );
            return printOut;
        }

        static Angle[] CreateAngles() {
            Angle[] angles = new Angle[(int)(Math.PI * 2 * 10)];
            int i = 0;
            while (i < angles.Length) {
                angles[i] = new Angle(i / 10.0);
                i++;
            }
            return angles;
        }

        static void Main() {
            // Person
            var people = CreatePeople();
            var p = people[10];
            var i = people.IndexOf(p);
            Console.WriteLine($"i = {i}");
            if (i.HasValue)
            {
                Console.WriteLine($"p = {p}");
                Console.WriteLine($"People[{i.Value}] = {people[i.Value]}");
            }

            // Angle
            // Tengo que sobreescribir equals, hashCode y toString en Angle, Person ya lo tenía
            var angles = CreateAngles();
            var a = angles[21];
            i = angles.IndexOf(a);
            Console.WriteLine($"i = {i}");
            if (i.HasValue)
            {
                Console.WriteLine($"a = {a}");
                Console.WriteLine($"Angles[{i.Value}] = {angles[i.Value]}");
            }

            // Usando Index2

            // Person
            i = people.IndexOf2(p);
            Console.WriteLine($"i = {i}");
            if (i.HasValue)
            {
                Console.WriteLine($"p = {p}");
                Console.WriteLine($"People[{i.Value}] = {people[i.Value]}");
            }

            // Angle
            i = angles.IndexOf2(a);
            Console.WriteLine($"i = {i}");
            if (i.HasValue)
            {
                Console.WriteLine($"a = {a}");
                Console.WriteLine($"Angles[{i.Value}] = {angles[i.Value]}");
            }

            // Person
            i = people.IndexOf2(p, new SameName());
            Console.WriteLine($"i = {i}");
            if (i.HasValue)
            {
                Console.WriteLine($"p = {p}");
                Console.WriteLine($"People[{i.Value}] = {people[i.Value]}");
            }

            // Angle
            i = angles.IndexOf2(a, new SameQuadrant());
            Console.WriteLine($"i = {i}");
            if (i.HasValue)
            {
                Console.WriteLine($"a = {a}");
                Console.WriteLine($"Angles[{i.Value}] = {angles[i.Value]}");
            }
        }

    }

    public interface IEqualityPredicate
    {
        bool Compare(object x, object y);
    }

    internal class SameName : IEqualityPredicate
    {
        public bool Compare(object x, object y)
        {
            var personX = x as Person;
            if (personX == null)
                return false;
            var personY = y as Person;
            if (personY == null)
                return false;
            // Usar siempre ToLower para evitar problemas con mayus
            return personX.FirstName.ToLower() == personY.FirstName.ToLower();
        }
    }

    internal class SameQuadrant : IEqualityPredicate
    {
        public bool Compare(object x, object y)
        {
            var angleX = x as Angle;
            if (angleX == null)
                return false;
            var angleY = y as Angle;
            if (angleY == null)
                return false;
            return Quadrant(angleX.Radians) == Quadrant(angleY.Radians);
        }

        private int Quadrant(double angle)
        {
            if (angle > 2 * Math.PI)
                return Quadrant(angle % (2 * Math.PI));
            if (angle < 0)
                return Quadrant(angle + 2 * Math.PI);
            if (angle < Math.PI / 2)
                return 1;
            if (angle < Math.PI)
                return 2;
            if (angle < 3 * Math.PI / 2)
                return 3;
            return 4;
        }
    }
}
