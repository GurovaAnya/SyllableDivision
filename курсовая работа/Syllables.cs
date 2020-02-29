using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SyllableDivision
{
    public class Syllables
    {
        //Массивы, хранящие данные о видах букв в слове.
        static char[] glasn = new char[] { 'ё', 'е', 'ы', 'а', 'о', 'э', 'я', 'и', 'ю', 'у'
            ,'Ё', 'Е', 'Ы', 'А', 'О', 'Э', 'Я', 'И', 'Ю', 'У' };
        static char[] sonorn = new char[] { 'л', 'м', 'н', 'р', 'й', 'Л', 'М', 'Н', 'Р', 'Й' };
        static char[] shumn = new char[] { 'к', 'х', 'п', 'ф', 'т', 'с', 'ш', 'ч', 'ц', 'в', 'щ', 'г', 'б', 'д', 'ж', 'з'
            ,'К', 'Х', 'П', 'Ф', 'Т', 'С', 'Ш', 'Ч', 'Ц', 'В', 'Щ', 'Г', 'Б', 'Д', 'Ж', 'З' };

        /// <summary>
        /// Деление текста на слова и вывод слогов на печать.
        /// </summary>
        /// <param name="text">текст для деления на слова</param>
        /// <returns>результат деления текста на слоги</returns>
        public static string DivideText(string text)
        {
            //Строка, хранящая результат деления.
            string result = string.Empty;
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //Применение правила деления для каждого слова.
            foreach (string word in words)
                result +=Rules(word)+" ";
            return result;
        }

        /// <summary>
        /// Применение правила деления слова.
        /// </summary>
        /// <param name="word">Слово для деления на слоги</param>
        /// <returns>Введенное слово, разделенное на слоги</returns>
        public static string Rules(string word)
        {
            //Строка, хранящая результат деления слова.
            string result = string.Empty;
            //Количество сонорных перед делением.
            int countsonorn = 0;
            //Переменные, хранящие данные о предыдущем символе.
            bool prevglasn = false,
                prevsonorn = false,
                prevj = false,
                //Первый слог слова напечатан.
                firstPrinted = false,
                //В рабочий слог добавлен гласный.
                glasnSlog = false,
                //Перед слогом изначально стоял дефис.
                divAdded = false;
            //"Рабочий" слог.
            StringBuilder slog = new StringBuilder();
            //Цикл обработки каждого символа.
            foreach (char letter in word)
            {
                //Обработка гласных символов.
                if (glasn.Contains(letter))
                {
                    //Если перед гласным стоит другой гласный.
                    if (prevglasn)
                    {
                        slog = PrintSlog(slog, ref result, ref firstPrinted);
                        divAdded = false;
                    }
                    //Если перед гласным стоит сонорный и гласный в "рабочем" слоге есть.
                    else if (prevsonorn && glasnSlog)
                    {
                        slog = PrintSlog(slog, ref result, ref firstPrinted, countsonorn);
                        divAdded = false;
                    }
                    prevglasn = true;
                    prevsonorn = false;
                    glasnSlog = true;
                    countsonorn = 0;
                }

                //Обработка шумных символов.
                else if (shumn.Contains(letter))
                {
                    //Если перед шумным стоит гласный или стоит сонорный и гласный в "рабочем" слоге есть.
                    if (prevglasn || prevsonorn && glasnSlog)
                    {
                        slog = PrintSlog(slog, ref result, ref firstPrinted);
                        glasnSlog = false;
                        divAdded = false;
                    }
                    prevglasn = false;
                    prevsonorn = false;
                    countsonorn = 0;
                }

                //Обработка сонорных символов.
                else if (sonorn.Contains(letter))
                {
                    //Если в "рабочем" слоге есть гласный.
                    if (glasnSlog)
                    {
                        //Если предыдущий символ - "й"
                        if (prevj)
                        {
                            slog = PrintSlog(slog, ref result, ref firstPrinted);
                            glasnSlog = false;
                            divAdded = false;
                        }
                        //Если предыдущий символ - 1 сонорный.
                        else if (prevsonorn && countsonorn == 1)
                        {
                            slog = PrintSlog(slog, ref result, ref firstPrinted, 1);
                            glasnSlog = false;
                            divAdded = false;
                        }
                    }
                    //Если текущий символ - "й"
                    if (letter == 'й'|| letter=='Й') prevj = true;
                    else prevj = false;
                    prevsonorn = true;
                    prevglasn = false;
                    countsonorn++;
                }

                //Если добавлен дефис или перенос, печать с учетом данных знаков.
                else if (letter == '-' || letter == '\n')
                {
                    bool temp = glasnSlog && !divAdded && firstPrinted;
                    slog = PrintSlog(slog, ref result, ref temp);
                    firstPrinted = false;
                    glasnSlog = false;
                    prevglasn = false;
                    prevsonorn = false;
                    divAdded = true;
                }
                //Если в слове другой символ, то уменишить счетчик сонорных.
                else if(countsonorn>0) countsonorn--;

                //Добавление символа в "рабочий" слог.
                slog.Append(letter);

            }
            //Обработка последнего "рабочего" слога.
            if (glasnSlog)
                PrintSlog(slog, ref result, ref firstPrinted);
            else
            {
                firstPrinted = false;
                PrintSlog(slog, ref result, ref firstPrinted);
            }
            return result;
        }    

        /// <summary>
        /// Применение правил для добавления слога к разделенной части слова.
        /// </summary>
        /// <param name="slog">Рабочий слог</param>
        /// <param name="result">Разделенная на слоги часть слова</param>
        /// <param name="firstPrinted">Напечатан ли первый слог в слове</param>
        /// <param name="cutLength">Сколько символов нужно оставить в рабочем слоге</param>
        /// <returns></returns>
        public static StringBuilder PrintSlog(StringBuilder slog, ref string result, ref bool firstPrinted, int cutLength = 0)
        {
            string newslog = Convert.ToString(slog);
            //Если удалять часть слога нужно.
            if (cutLength > 0)
                newslog = newslog.Remove(newslog.Length - cutLength);
            //Проверка на печать дефиса в начале слова.
            if (firstPrinted)
                result += "-" + newslog;
            else result += newslog;
            //Удаление из "рабочего" слога напечатонной части.
            slog.Remove(0, slog.Length - cutLength);
            firstPrinted = true;
            return slog;
        }
    }
}
