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
            GradeBook<Exam> Book1 = new GradeBook<Exam>("Иванов Иван Иванович");
            GradeBook<Exam> Book2 = new GradeBook<Exam>("Петров Петр Петрович");
            GradeBook<Exam> Book3 = new GradeBook<Exam>("Семенов Семен Семенович");

            #region Запросы на выборку
            Console.WriteLine("Имена студентов заданного курса:");
            Console.WriteLine();
            Console.WriteLine("Имена студентов, сдавших все экзамены на \"отлично\":");
            Console.WriteLine();
            #endregion

            #region Получение счетчика
            Console.WriteLine("Количество студентов на заданном курсе:");
            Console.WriteLine();
            Console.WriteLine("Количество студентов, сдавших все экзамены на \"отлично\":");
            Console.WriteLine();
            Console.WriteLine("Количество студентов, не сдавших хотя бы один экзамен:");
            Console.WriteLine();
            #endregion

            #region Аггрегирование данных
            Console.WriteLine("Средний балл за сессию студента Иванов Иван Иванович:");
            Console.WriteLine();
            Console.WriteLine("Средний балл за сессию студента Петров Петр Петрович:");
            Console.WriteLine();
            Console.WriteLine("Средний балл за сессию студента Семенов Семен Семенович:");
            Console.WriteLine();
            #endregion

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
