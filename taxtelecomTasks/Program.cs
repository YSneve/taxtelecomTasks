
using System.Text.RegularExpressions;

internal class program
{
    static void Main(String[] args)
    {

        Console.WriteLine("Введите строку");
        string inputStr = Console.ReadLine();


        if (isAllLower(inputStr))
            Console.WriteLine(firstTaskReverse(inputStr));
        else
            Console.WriteLine("Ошибка! Введены не подходящие символы." +
                "\nСледующие, из введенных символов, не являются подходящими: {0}",getErrors(inputStr));
    }
    // Задание 1
    static string firstTaskReverse(string userString)
    {

        return userString.Length % 2 != 0 ? ReverseString(userString) + userString : oddCase(userString);

        // Функция для нечетного кол-ва символов в строке
        string oddCase(string toReverseStr)
        {
            string firstHalf = ReverseString(toReverseStr.Substring(0, toReverseStr.Length / 2)); // Развернутая первая половина входной строки
            string secondHalf = ReverseString(toReverseStr.Substring(toReverseStr.Length / 2, toReverseStr.Length / 2)); // Развернутая вторая половина входной строки

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
    static bool isAllLower(string checkString)
    {
        //Регулярное выражение, проверяющее на то, что строка от начала и до конца
        //является набором символов от a до z 
        var regExpr = @"(^[a-z]*$)";
        
        var lowLetExpr = new Regex(regExpr);
       
        return lowLetExpr.IsMatch(checkString);
    }
    static string getErrors(string inString)
    {
        //Регулярное выражение, проверяющее на то, что символ
        //Является символом из диапазона от a до z 
        var regExpr = @"([a-z]*)";
        var lowerLettExpr = new Regex(regExpr);
        
        //Удаление подходящих для ввода символов
        //Удаление повторяющихся неподходящих для вывода символов
        //Возвращение строки из не повторяющихся, не подходящих для ввода символов
        return string
            .Join("", new HashSet<char>(lowerLettExpr.
            Replace(inString, "").
            ToCharArray()));          
    }
}