using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Librarium;

namespace laboratory15
{
    class Program
    {
        static void Main(string[] args)
        {
            // Массив зачеток
            GradeBook<Exam>[] Students = new GradeBook<Exam>[] { new GradeBook<Exam>("Иванов Иван Иванович", 1),
                new GradeBook<Exam>("Петров Петр Петрович", 7), new GradeBook<Exam>("Семенов Семен Семенович", 4) };

            // Печать имеющейся информации
            foreach (GradeBook<Exam> Student in Students)
            {
                int SesCount = 0;
                Console.WriteLine("Студент {0}", Student.StudentsName);
                foreach (GradeBook<Exam>.Semester<Exam> Session in Student.GetBook)
                {
                    Console.WriteLine("Сессия {0}", SesCount + 1);
                    foreach (Exam Ex in Session.GetExams)
                        Console.WriteLine(Ex);
                    Console.WriteLine();
                    SesCount++;
                }                
            }

            #region Запросы на выборку
            Console.WriteLine("Количество студентов на заданном курсе (LINQ-запрос):");
            for (int Course = 1; Course < 5; Course++)
            {
                Console.WriteLine("Имена студентов {0}-го курса:", Course);
                // Выбираем всех студентов с подходящим количеством семестров
                var NamesCourseLINQ = from Book in Students
                                      where (Book.Count == Course * 2 || Book.Count == Course * 2 - 1)
                                      select Book.StudentsName;
                foreach (string Item in NamesCourseLINQ)
                    Console.WriteLine(Item);
                Console.WriteLine();
            }

            Console.WriteLine("Количество студентов на заданном курсе (метод расширения):");
            for (int Course = 1; Course < 5; Course++)
            {
                Console.WriteLine("Имена студентов {0}-го курса:", Course);
                var NamesCourseExpand = Students.Where(Book => Book.Count == Course * 2 || Book.Count == Course * 2 - 1).
                    Select(Book => Book.StudentsName);
                foreach (string Item in NamesCourseExpand)
                    Console.WriteLine(Item);
                Console.WriteLine();
            }

            Console.WriteLine("Имена студентов, сдавших в какую-либо сессию все экзамены на \"отлично\" (LINQ-запрос):");
            // Перебираем всех студентов в массиве
            var NamesMarkLINQ = from Student in Students
                                // У каждого студента проверяем его зачетку
                                let Book = Student.GetBook
                                // В зачетке перебираем все сессии и выбираем те, в которых все экзамены сданы на отлично
                                let Success = from Session in Book where Session.Excelent select Session
                                // Выбираем студентов, у которых есть сессии когда все экзамены сданы на отлично
                                where Success.Count() > 0
                                select Student.StudentsName;
            foreach (string Item in NamesMarkLINQ)
                Console.WriteLine(Item);
            Console.WriteLine();

            Console.WriteLine("Имена студентов, сдавших в какую-либо сессию все экзамены на \"отлично\" (метод расширения):");
            var NamesMarkExpand = Students.Where(Student => Student.GetBook.
                Any(Session => Session.Excelent)).Select(Student => Student.StudentsName);                
            foreach (string Item in NamesMarkExpand)
                Console.WriteLine(Item);
            Console.WriteLine();
            #endregion

            #region Получение счетчика
            Console.WriteLine("Количество студентов на заданном курсе (LINQ-запрос):");
            for (int Course = 1; Course < 5; Course++)
            {
                // Выбираем всех студентов с подходящим количеством семестров
                var CountCourseLINQ = from X in Students where (X.Count == Course * 2 || X.Count == Course * 2 - 1) select X;
                Console.WriteLine("На {0}-м курсе учится {1} студентов.", Course, CountCourseLINQ.Count());
                Console.WriteLine();
            }

            Console.WriteLine("Количество студентов на заданном курсе (метод расширения):");
            for (int Course = 1; Course < 5; Course++)
            {
                var CountCourseExpand = Students.Where(Book => Book.Count == Course * 2 || Book.Count == Course * 2 - 1).
                    Select(Book => Book.StudentsName);
                Console.WriteLine("На {0}-м курсе учится {1} студентов.", Course, CountCourseExpand.Count());
                Console.WriteLine();
            }

            Console.WriteLine("Количество студентов, сдавших в какую-либо сессию все экзамены на \"отлично\" (LINQ-запрос):");
            // Перебираем всех студентов в массиве
            var CountMarkLINQ = from Student in Students
                                // У каждого студента проверяем его зачетку
                                let Book = Student.GetBook
                                // В зачетке перебираем все сессии и выбираем те, в которых все экзамены сданы на отлично
                                let Success = from Session in Book where Session.Excelent select Session
                                // Выбираем студентов, у которых есть сессии когда все экзамены сданы на отлично
                                where Success.Count() > 0
                                select Student.StudentsName;
            Console.WriteLine(CountMarkLINQ.Count());
            Console.WriteLine();

            Console.WriteLine("Количество студентов, сдавших в какую-либо сессию все экзамены на \"отлично\" (метод расширения):");
            var CountMarkExpand = Students.Where(Student => Student.GetBook.
                Any(Session => Session.Excelent)).Select(Student => Student.StudentsName);
            Console.WriteLine(CountMarkExpand.Count());
            Console.WriteLine();

            Console.WriteLine("Количество студентов, не сдавших хотя бы один экзамен (LINQ-запрос):");
            // Перебираем всех студентов в массиве
            var CountFailLINQ = from Student in Students
                                // У каждого студента проверяем его зачетку
                                let Book = Student.GetBook
                                // В зачетке перебираем все сессии и выбираем те, в которых есть заваленные экзамены
                                let Fail = from Session in Book where Session.Failed select Session
                                // Выбираем студентов, у которых есть сессии в которых есть заваленные экзамены
                                where Fail.Count() > 0
                                select Student.StudentsName;
            Console.WriteLine(CountFailLINQ.Count());
            Console.WriteLine();

            Console.WriteLine("Количество студентов, не сдавших хотя бы один экзамен (LINQ-запрос):");
            var CountFailExpand = Students.Where(Student => Student.GetBook.
                Any(Session => Session.Failed)).Select(Student => Student.StudentsName);
            Console.WriteLine(CountFailExpand.Count());
            Console.WriteLine();
            #endregion

            #region Аггрегирование данных
            Console.WriteLine("Средние баллы студентов (LINQ-запрос):");
            foreach (GradeBook<Exam> Student in Students)
            {
                int SesCount = 0;
                Console.WriteLine("Студент {0}", Student.StudentsName);
                foreach (GradeBook<Exam>.Semester<Exam> Session in Student.GetBook)
                {
                    var Aver = from Exams in Session.GetExams select Exams.GetMark;
                    Console.WriteLine("Сессия {0} - {1}", SesCount + 1, Aver.Average());                    
                    SesCount++;
                }
            }
            Console.WriteLine();

            Console.WriteLine("Средние баллы студентов (метод расширения):");
            foreach (GradeBook<Exam> Student in Students)
            {
                int SesCount = 0;
                Console.WriteLine("Студент {0}", Student.StudentsName);
                foreach (GradeBook<Exam>.Semester<Exam> Session in Student.GetBook)
                {
                    var Aver = Session.GetExams.Select(Exams => Exams.GetMark);
                    Console.WriteLine("Сессия {0} - {1}", SesCount + 1, Aver.Average());
                    SesCount++;
                }
            }
            Console.WriteLine();
            #endregion

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}