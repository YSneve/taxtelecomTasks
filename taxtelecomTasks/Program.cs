
using System.Text.RegularExpressions;

namespace taxtelecomTasks;

internal class Program
{
    static void Main(String[] args)
    {

        Console.WriteLine("Введите строку");
        var inputStr = Console.ReadLine();


        if (IsAllLower(inputStr))
            Console.WriteLine("Результат: {0}", FirstTaskReverse(inputStr));
        else
        {
            WriteErrors(inputStr);
        }
    }
    // Задание 1
    private static string FirstTaskReverse(string userString)
    {

        return userString.Length % 2 != 0 ? ReverseString(userString) + userString : oddCase(userString);

        // Функция для нечетного кол-ва символов в строке
        string oddCase(string toReverseStr)
        {
            string firstHalf = ReverseString(toReverseStr[..(toReverseStr.Length / 2)]); // Развернутая первая половина входной строки
            string secondHalf = ReverseString(toReverseStr[(toReverseStr.Length / 2)..]); // Развернутая вторая половина входной строки

            return firstHalf + secondHalf;
        }

        //Функция разворота строки
        string ReverseString(string toReverseStr)
        {
            var toReverseList = toReverseStr.ToCharArray();

            Array.Reverse(toReverseList);

            return new string(toReverseList);
        }
    }

    //Задание 2
    private static bool IsAllLower(string checkString)
    {
        //Регулярное выражение, проверяющее на то, что строка от начала и до конца
        //является набором символов от a до z 
        var lowLetExpr = new Regex(@"(^[a-z]*$)");

        return lowLetExpr.IsMatch(checkString);
    }

    //Задание 3
    private static void WriteErrors(string inString)
    {
        //Регулярное выражение, проверяющее на то, что символ
        //Является символом из диапазона от a до z 
        var lowerLetterExpr = new Regex(@"([a-z]*)");

        //Удаление подходящих для ввода символов
        //Удаление повторяющихся неподходящих для вывода символов
        //Возвращение строки из не повторяющихся, не подходящих для ввода символов
        var wrongLettersSet = new HashSet<char>(lowerLetterExpr.
                Replace(inString, "").
                ToCharArray());

        Console.WriteLine("Ошибка! Введены не подходящие символы." +
                          "\nСледующие, из введенных символов, не являются подходящими:" +
                          "\nСимвол; Кол-во повторений");
        foreach (var wrongLetter in wrongLettersSet)
        {
            var matchCount = new Regex(wrongLetter.ToString()).Matches(inString).Count;
            Console.WriteLine("{0} : {1}", wrongLetter, matchCount);
        }
    }
}