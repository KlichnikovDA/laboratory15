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
            // Зачетка
            GradeBook<Exam> Student = new GradeBook<Exam>("Петров Петр Петрович", 7);
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


            #region Запросы на выборку
            Console.WriteLine("Экзамены, сданные на \"отлично\" (LINQ-запрос):");
            // Перебираем все сессии, которые студент сдавал
            var ExcelentLINQ = from Session in Student.GetBook
                                   // В каждой сессии перебираем экзамены и выбираем те, оценка за которые больше 8
                               from Exam in Session.GetExams where Exam.GetMark >= 8 select Exam;
            foreach (Exam Item in ExcelentLINQ)
                Console.WriteLine(Item);
            Console.WriteLine();

            Console.WriteLine("Экзамены, сданные на \"отлично\" (метод расширения):");
            var ExcelentExpand = Student.GetBook.SelectMany(Session => Session.GetExams).Where(Exam => Exam.GetMark >= 8).Select(Exam => Exam);
            foreach (Exam Item in ExcelentExpand)
                    Console.WriteLine(Item);
            Console.WriteLine();
            #endregion

            #region Получение счетчика
            Console.WriteLine("Количество экзаменов, сданных на \"отлично\" (LINQ-запрос):");
            // Перебираем все сессии, которые студент сдавал
            var ExcelentCountLINQ = (from Session in Student.GetBook
                                   // В каждой сессии перебираем экзамены и выбираем те, оценка за которые больше 8
                               from Exam in Session.GetExams where Exam.GetMark >= 8 select Exam).Count();
            Console.WriteLine(ExcelentCountLINQ);
            Console.WriteLine();

            Console.WriteLine("Количество экзаменов, сданных на \"отлично\" (метод расширения):");
            var ExcelentCountExpand = Student.GetBook.SelectMany(Session => Session.GetExams).Where(Exam => Exam.GetMark >= 8).Select(Exam => Exam).Count();
            Console.WriteLine(ExcelentCountExpand);
            Console.WriteLine();

            Console.WriteLine("Количество сессий, сданных на \"хорошо\" и \"отлично\" (LINQ-запрос):");
            var SessionCountLINQ = (from Session in Student.GetBook where Session.Excelent select Session).Count();
            Console.WriteLine(SessionCountLINQ);
            Console.WriteLine();

            Console.WriteLine("Количество сессий, сданных на \"хорошо\" и \"отлично\" (метод расширения):");
            var SessionCountExpand = Student.GetBook.Where(Session => Session.Excelent).Select(Session => Session).Count();
            Console.WriteLine(SessionCountExpand);
            Console.WriteLine();
            #endregion

            #region Операции над множествами
            Console.WriteLine("Предметы, сданные в разные сессии на \"отлично\" и \"неудовлетворительно\" (LINQ-запрос):");
            // Перебираем все сессии, которые студент сдавал
            var NotBadLINQ1 = from Session in Student.GetBook
                               // В каждой сессии перебираем экзамены и выбираем те, оценка за которые больше 8
                               from Exam in Session.GetExams
                               where Exam.GetMark >= 8
                               select Exam.GetSubject;

            var NotBadLINQ2 = from Session in Student.GetBook
                                  // В каждой сессии перебираем экзамены и выбираем те, оценка за которые меньше 4
                              from Exam in Session.GetExams
                              where Exam.GetMark <= 4
                              select Exam.GetSubject;

            var NotBadLINQ = NotBadLINQ1.Intersect(NotBadLINQ2);
            foreach (string Item in NotBadLINQ)
                Console.WriteLine(Item);
            Console.WriteLine();

            Console.WriteLine("Предметы, сданные в разные сессии на \"отлично\" и \"неудовлетворительно\" (метод расширения):");
            var NotBadExpand1 = Student.GetBook.SelectMany(Session => Session.GetExams).Where(Exam => Exam.GetMark >= 8).Select(Exam => Exam.GetSubject);
            var NotBadExpand2 = Student.GetBook.SelectMany(Session => Session.GetExams).Where(Exam => Exam.GetMark <= 4).Select(Exam => Exam.GetSubject);
            var NotBadExpand = NotBadExpand1.Intersect(NotBadExpand2);
            foreach (string Item in NotBadExpand)
                Console.WriteLine(Item);
            Console.WriteLine();
            #endregion

            #region Аггрегирование данных
            Console.WriteLine("Средние баллы за сессии данного студента (LINQ-запрос):");
            var AverageLINQ = from Session in Student.GetBook 
                              let Marks = (from Exam in Session.GetExams select Exam.GetMark).Average()
                              select Marks;
            int SessionNumber = 1;
            foreach (double Item in AverageLINQ)
            {
                Console.WriteLine("Сессия {0} - {1}", SessionNumber++, Item);
            }
            Console.WriteLine();

            Console.WriteLine("Средние баллы за сессии данного студента (метод расширения):");
            var AverageExpand = Student.GetBook.Select(Session => Session.GetExams.Average(Exam => Exam.GetMark));
            SessionNumber = 1;
            foreach (double Item in AverageExpand)
            {
                Console.WriteLine("Сессия {0} - {1}", SessionNumber++, Item);
            }
            Console.WriteLine();
            #endregion

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}