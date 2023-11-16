using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TPP.Laboratory.Functional.Lab08
{
    class Query
    {
        private Model model = new Model();

        private static void Show<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Number of items in the collection: {0}.", collection.Count());
        }

        static void Main(string[] args)
        {
            Query query = new Query();
            query.Query1();
            query.Query2a();
            query.Query2b();
            query.Query2c();
            query.Query3();
            query.Query4a();
            query.Query4b();
            query.Query4c();
            query.Query5();
            query.Query6();
        }

        private void Query1()
        {
            // Modify this query to show the names of the employees older than 50 years
            var employees = model.Employees
                .Where(e => e.Age > 50)
                .Select(e => e.Name);

            Console.WriteLine("Query1_Employees:");
            Show(employees);
        }

        private void Query2a()
        {
            // Show the name and email of the employees who work in Asturias

            // Recomendado: Usar .ToLower() para evitar problemas

            var employees = model.Employees
                .Where(e => e.Province.ToLower().Equals("asturias"))
                .Select(e => new NameAndEmail { Name = e.Name, Email = e.Email });
            
            // Como se repiten (Name -> .Name), se pueden obviar los nombres de los campos... 2b

            Console.WriteLine("\nQuery2a_Employees:");
            Show(employees);
        }

        private void Query2b()
        {
            // Show the name and email of the employees who work in Asturias

            // Se declara una clase anónima: new { e.Name, e.Email }

            var employees = model.Employees
                .Where(e => e.Province.ToLower().Equals("asturias"))
                .Select(e => new { e.Name, e.Email });

            Console.WriteLine("\nQuery2b_Employees:");
            Show(employees);
        }

        private void Query2c()
        {
            // Show the name and email of the employees who work in Asturias

            // Usando tuplas:

            var employees = model.Employees
                .Where(e => e.Province.ToLower().Equals("asturias"))
                .Select(e => (e.Name, e.Email));

            // .Select(e => (NameTupla: e.Name, EmailTupla: e.Email));

            Console.WriteLine("\nQuery2c_Employees:");
            Show(employees);
        }

        // Notice: from now on, check out http://msdn.microsoft.com/en-us/library/9eekhta0.aspx

        private void Query3()
        {
            // Show the names of the departments with more than one employee 18 years old and beyond; 
            // the department should also have any office number starting with "2.1"
            var departments = model.Departments
               .Where(d => d.Employees.Count(e => e.Age >= 18) > 1)
               .Where(d => d.Employees.Any(e => e.Office.Number.StartsWith("2.1")))
               .Select(d => d.Name);

            // El primer Where simplifica a este: .Where(e => e.Age >= 18).Count()

            Console.WriteLine("\nQuery3_Departments:");
            Show(departments);            
        }

        private void Query4a()
        {
            // Show the phone calls of each employee. 
            // Each line should show the name of the employee and the phone call duration in seconds.
            var phoneCalls = model.PhoneCalls
                .Where(c => model.Employees.Any(e => e.TelephoneNumber == c.SourceNumber))
                .Select(c => (model.Employees.First(e => e.TelephoneNumber == c.SourceNumber).Name, c.Seconds));

            Console.WriteLine("\nQuery4a_PhoneCalls:");
            Show(phoneCalls);
        }

        private void Query4b()
        {
            // Show the phone calls of each employee. 
            // Each line should show the name of the employee and the phone call duration in seconds.            
            var phoneCalls = model.Employees
                .SelectMany(e => model.PhoneCalls.Where(c => c.SourceNumber == e.TelephoneNumber))
                .Select(c => (model.Employees.First(e => e.TelephoneNumber == c.SourceNumber).Name, c.Seconds));

            // Como Select retornaría un IE<IE<PC>>... usar SelectMany que retorna un IE<PC>

            Console.WriteLine("\nQuery4b_PhoneCalls:");
            Show(phoneCalls);
        }

        private void Query4c()
        {
            // Show the phone calls of each employee. 
            // Each line should show the name of the employee and the phone call duration in seconds.
            
            // LA mejor solución - usando join

            var phoneCalls = model.Employees.Join(model.PhoneCalls,
                e => e.TelephoneNumber,
                c => c.SourceNumber,
                (e, c) => (e.Name, c.Seconds)
            );

            Console.WriteLine("\nQuery4c_PhoneCalls:");
            Show(phoneCalls);
        }

        private void Query5()
        {
            // Show, grouped by each province, the name of the employees 
            // (both province and employees must be lexicographically ordered)
            var employees = model.Employees.OrderBy(e => e.Province)
                .ThenBy(e => e.Name)
                .GroupBy(e => e.Province.ToLower());

            Console.WriteLine("\nQuery5_Employees:");
            foreach (var esByProvince in employees)
            {
                Console.WriteLine(esByProvince.Key);
                Show(esByProvince);
            }
        }

        private void Query6()
        {
            // Rank the calls by duration. Show rank position and duration
            var phoneCalls = model.PhoneCalls.OrderByDescending(c => c.Seconds)
                .Zip(Enumerable.Range(1, model.PhoneCalls.Count() + 1),
                (c, i) => $"Rank: {i} ({c.Seconds}) {c.SourceNumber} -> {c.DestinationNumber}");

            Console.WriteLine("\nQuery6_PhoneCalls:");
            Show(phoneCalls);
        }


        /************ Homework **********************************/

        private void Homework1()
        {
            // Show, ordered by age, the names of the employees in the Computer Science department, 
            // who have an office in the Faculty of Science, 
            // and who have done phone calls longer than one minute
            var employees = model.Employees.Where(e => e.Department.Name.ToLower().Equals("computer science"))
                .Where(e => e.Office.Building.ToLower().Equals("faculty of science"))
                .Where(e => e.TelephoneNumber.Equals(model.PhoneCalls.Where(p => p.Seconds > 60).Select(p => p.SourceNumber).First()))
                .OrderBy(e => e.Age)
                .Select(e => (e.Name, e.Age));

            Console.WriteLine("Homework1_Employees:");
            Show(employees);
        }

        private void Homework2()
        {
            // Show the summation, in seconds, of the phone calls done by the employees of the Computer Science department
            var csemployees = model.Employees.Where(e => e.Department.Name.ToLower().Equals("computer science"));
            var calls_join = csemployees.Join(model.PhoneCalls,
                e => e.TelephoneNumber,
                c => c.SourceNumber,
                (e, c) => (e.Name, c.Seconds)
            );
            var summation = calls_join.Sum(c => c.Seconds);

            Console.WriteLine($"\nHomework2_Summation:\n {summation}");
        }

        private void Homework3()
        {
            // Show the phone calls done by each department, ordered by department names. 
            // Each line must show “Department = <Name>, Duration = <Seconds>”
            var join = model.Employees.Join(model.PhoneCalls,
                e => e.TelephoneNumber,
                c => c.SourceNumber,
                (e, c) => (e.Department.Name, c.Seconds)
            ).OrderBy(j => j.Name);

            Console.WriteLine("\nHomework3_Departments:");
            foreach (var j in join)
                Console.WriteLine("Department = " + j.Name + ", Duration = " + j.Seconds);
        }

        private void Homework4()
        {
            // Show the departments with the youngest employee, 
            // together with the name of the youngest employee and his/her age 
            // (more than one youngest employee may exist)
            var youngest = model.Employees.Where(e => e.Age < 40).Select(e => new { Departamento = e.Department.Name, Nombre = e.Name, Edad = e.Age });

            Console.WriteLine("\nHomework4_Youngest:");
            Show(youngest);
        }

        private void Homework5()
        {
            // Show the greatest summation of phone call durations, in seconds, 
            // of the employees in the same department, together with the name of the department 
            // (it can be assumed that there is only one department fulfilling that condition)
            var join = model.Employees.Join(model.PhoneCalls,
                e => e.TelephoneNumber,
                c => c.SourceNumber,
                (e, c) => (e.Department.Name, c.Seconds)
            ).GroupBy(j => j.Name);

            Console.WriteLine("\nHomework5_Seconds:");
            foreach (var department in join)
            {
                int sum = 0;
                foreach (var d in department)
                {
                    sum += d.Seconds;
                }
                Console.WriteLine($"Department = {department.Key}, Summation= {sum}");
            }
        }

        private class NameAndEmail
        {
            public string Name { get; set; }
            public string Email { get; set; }

            public override string ToString()
            {
                return "{" + Name + ", " + Email + "}";
            }
        }
    }

}
