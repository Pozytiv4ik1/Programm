using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_App_NEW
{
    class Program
    {
        // Вот начиная с этого места и во всех других методах у тебя везде вместо public стоит protected static
        // Для чего? Какой ты смысл в это вкладываешь?
        pubic List<Employee> EmployeeList = new List<Employee>();

        static void Main(string[] args)
        {
            InitEmployeeList();
            Console.WriteLine("Введите одну из доступных команд: \nList - показать список доступных запросов \nAdd - добавить данные по работнику \nDelete - удалить данные по работнику \nEdit - редактирование данные по работнику \nExit - выход из программы ");
            EnterCommand(Console.ReadLine());
        }

        static int EnterCommand(string Enter)
        {
            switch (Enter) //в зависимости что введено, выполняется 1 из 4х функций ниже
            {
                case "List":
                    processCommandList();
                    break;
                case "Add":
                    processCommandAdd();
                    break;
                case "Delete":
                    processCommandDelete();
                    break;
                case "Edit":
                    processCommandEdit();
                    break;
                case "Exit":
                    break;
                default:
                    Console.WriteLine("Введённая команда '{0}' не существует", Enter);
                    break;
            }

            if (Enter == "Exit")
            {
                return 0;
            }
            Console.WriteLine("Введите одну из доступных команд: \nList - показать список доступных запросов \nAdd - добавить данные работника \nDelete - удалить данные работника \nEdit - редактирование данные работника \nExit - выход из программы ");
            return EnterCommand(Console.ReadLine());
        }
        
        protected static void processCommandList()
        {
            Console.WriteLine("Нажмите следующую цифру от 1 до 7 чтобы вывести:");
            Console.WriteLine("1 - Вывести весь список сотрудников \n" +
                "2 - вывести список сотрудников по отделам в порядке убывания стажа работы \n" +
                "3 -вывести информацию о сотрудниках, имеющих зарплату ниже определенного уровня, " +
                    " отсортировав их по рабочему стажу\n" +
                "4 - вывести сотрудников по отделам \n" +
                "5 - вывести список сотрудников, включив в него, помимо начальных данных, столбец премий(50%)" +
                    " от оклада и итоговые суммы для каждого сотрудника \n " +
                "6 - вывести список сотрудников, день рождения которых в текущем месяце \n" +
                "7 - вывести два новых списка, поместив в первый тех сотрудников, которые работают меньше 5 лет," +
                " а во второй - всех прочих, подсчитать количество сотрудников каждой группы.\n");
            string NumberCommandList = Console.ReadLine();
            switch (NumberCommandList) //в зависимости что введено, выполняется 1 из 7 функций ниже
            {
                case "1":
                    outputEmployeeList(EmployeeList);
                    break;
                case "2":
                    processListEmployeesDepartmentByWorkExp();
                    break;
                //case "3":
                //    processCommandDelete();
                //    break;
                //case "4":
                //    processCommandEdit();
                //    break;
                //case "5":

                //    break;
                //case "6":
                //    break;
                //case "7":
                //    break;

                default:
                    Console.WriteLine("Введённая команда '{0}' не существует", NumberCommandList);
                    break;
            }

            Console.WriteLine("\n");
        }

        protected static void outputEmployeeList(List<Employee> EmpList)
        {
            foreach (Employee item in EmpList)
            {
                Console.WriteLine($"ID:{item.Id}, Имя: {item.Name}, Отдел №:{item.Department}, Должность: {item.Position}, ЗП: {item.Salary}, Стаж: {item.WorkExp}, Дата Рождения: {item.BirthDate.ToString("dd.MM.yyyy")}");
            }

        }

        protected static void processListEmployeesDepartmentByWorkExp()
        {
            string[] HardKodDep = { "19", "22", "23" };
            foreach (string Dep in HardKodDep)
            {
                List<Employee> DepEmploees = new List<Employee>();
                foreach (Employee employee in EmployeeList)
                {
                    if (employee.Department == Dep)
                    {
                        DepEmploees.Add(employee);
                    }
                }
                Console.WriteLine($"Работники отдела {Dep}");
                outputEmployeeList(DepEmploees);
                /*outputEmployeeList(EmployeeList.OrderBy(e => e.WorkExp).ToList());*/ // сортировка по убыванию WorkExp
                Console.WriteLine("\n");
            }
        }

        protected static void InitEmployeeList()
        {
            // Почему бы тут сразу массив Employee не задать, чем все по отдельности?
            string[] Ids = { "1", "2", "3", "4", "5" }; //обойти форичем, и=0 , 
            string[] Name = { "Alexandr", "Alex", "Bond", "Sam", "Mat" };
            string[] Department = { "22", "19", "23", "22", "19" };
            string[] Position = { "Injener", "Finansist", "Specialist", "Nachalnik", "Director" };
            double[] Salary = { 250.63, 200.3, 300.8, 400.8, 600.9 };
            int[] WorkExp = { 7, 6, 3, 2, 10 };
            string[] BirthDate = { "24.06.1990", "19.07.1985", "12.01.1984", "01.01.1984", "14.09.1989" };

            for (int i = 0; i < Ids.Length; i++)
            {
                Employee employee = new Employee()
                {
                    Id = Ids[i],
                    Name = Name[i],
                    Department = Department[i],
                    Position = Position[i],
                    Salary = Salary[i],
                    WorkExp = WorkExp[i],
                    BirthDate = DateTime.ParseExact(BirthDate[i], "dd.MM.yyyy", CultureInfo.InvariantCulture)
                };
                EmployeeList.Add(employee);
            }
        }

        protected static void processCommandDelete()
        {
            Console.WriteLine("Введите ID дела, который хотите удалить");
            int index = 0; //Чтобы не было ошибки отсутсвия переменной
            try
            {
                //Считываем индекс редактирования 
                index = Int32.Parse(Console.ReadLine()); // Тут лучше использовать TryParse, тогда и оборачивать в try-catch не придется
            }
            catch (Exception ex) // На случай, если надо будет залогировать либо вывести ошибку (ex)
            {
                //Если индекс не существует выводим сообщение об ошибке
                Console.WriteLine($"Ошибка, введено невалидное значение {index}");
                return;
            }

            //Если индекс существует удалить элемент с индексом удаления
            if (EmployeeList.Count > index && index >= 0)
            {
                Employee employee = EmployeeList[index];
                Console.WriteLine("Удалено");
                EmployeeList.RemoveAt(index);
            }
            else
            { Console.WriteLine("Данного индекса не существует"); }
        }

        protected static void processCommandEdit()
        {
            Console.WriteLine("Введите ID");
            int index = 0; //Чтобы не было ошибки отсутсвия переменной
            try
            {
                //Считываем индекс редактирования 
                index = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                //Если индекс не существует вывести сообщение об ошибке
                Console.WriteLine($"Ошибка, введено невалидное значение {index}");
                return;
            }
            //EmployeeList.Count -- количество элементов в списке,Если EmployeeList.Count больше вводимого значения с клавы и это значение больше-равно нулю то выполняется следующее
            if (EmployeeList.Count > index && index >= 0)
            {
                Employee employee = EmployeeList[index];//получение элемента из списка по индексу
                Console.WriteLine("Введите Имя");
                // Ридлайном считываю следующие данные и сохраняю его в todo
                employee.Name = Console.ReadLine();
                Console.WriteLine("Введите номер отдела");
                employee.Department = Console.ReadLine();
                Console.WriteLine("Введите должность");
                employee.Position = Console.ReadLine();
                Console.WriteLine("Введите ЗП");
                employee.Salary = double.Parse(Console.ReadLine());
                Console.WriteLine("Введите рабочий стаж");
                employee.WorkExp = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Введите дату Рождения");
                employee.BirthDate = DateTime.Parse(Console.ReadLine());

            }
            else
            { Console.WriteLine("Данного индекса не существует"); }
        }

        protected static void processCommandAdd()
        {
            //создаю новый объект Todo
            Employee employee = new Employee();
            Console.WriteLine("Введите Имя");
            // Ридлайном считываю следующие данные и сохраняю его в todo
            employee.Name = Console.ReadLine();
            Console.WriteLine("Введите номер отдела");
            employee.Department = Console.ReadLine();
            Console.WriteLine("Введите должность");
            employee.Position = Console.ReadLine();
            Console.WriteLine("Введите ЗП");
            employee.Salary = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите рабочий стаж");
            employee.WorkExp = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату Рождения");
            employee.BirthDate = DateTime.Parse(Console.ReadLine());
            EmployeeList.Add(employee);
            Console.WriteLine("Запись добавлена");
        }
    }
    
    public class Employee
    {
        ///<summary>
        ///табельный номер сотрудника
        ///</summary>
        public string Id { get; set; }

        ///<summary>
        ///Имя сотрудника
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///Номер Отдела
        ///</summary>
        public string Department { get; set; }

        ///<summary>
        ///Должность
        ///</summary>
        public string Position { get; set; }//TODO: перевести в Enum // 

        ///<summary>
        ///Заработная плата в рублях
        ///</summary>
        public double Salary { get; set; }

        ///<summary>
        ///Рабочий стаж в годах
        ///</summary>
        public int WorkExp { get; set; }

        ///<summary>
        ///Дата Рождения
        ///</summary>
        public DateTime BirthDate { get; set; }

        // Это у нас "конструктор по-умолчанию", его можно не писать, т.к. он создасться автоматически
        public Employee()
        {

        }
    }
}
