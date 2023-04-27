using System.Text.RegularExpressions;

namespace taxTelecomTasks.Core;

public class StringProcessor
{
    public static string ReverseString(string userString)
    {

        return userString.Length % 2 != 0 ? ReverseSubString(userString) + userString : oddCase(userString);

        // Функция для нечетного кол-ва символов в строке
        string oddCase(string toReverseStr)
        {
            var firstHalf = ReverseSubString(toReverseStr[..(toReverseStr.Length / 2)]); // Развернутая первая половина входной строки
            var secondHalf = ReverseSubString(toReverseStr[(toReverseStr.Length / 2)..]); // Развернутая вторая половина входной строки

            return firstHalf + secondHalf;
        }

        //Функция разворота строки
        string ReverseSubString(string toReverseStr)
        {
            var toReverseList = toReverseStr.ToCharArray();

            Array.Reverse(toReverseList);

            return new string(toReverseList);
        }
    }

    //Задание 2
    public static bool IsAllLower(string checkString, out string invalidChars)
    {
        //Регулярное выражение, проверяющее на то, что строка от начала и до конца
        //является набором символов от a до z 
        var lowLetExpr = new Regex(@"(^[a-z]*$)");
        invalidChars = lowLetExpr.Replace(checkString, "");
        return lowLetExpr.IsMatch(checkString);
    }


    //Задание 3
    public static Dictionary<char, int> GetLetterMatches(string inString)
    {
        var matchesDictionary = new Dictionary<char, int>();
        // Множество всех символов
        var allLettersSet = new HashSet<char>(inString.ToCharArray());

        // Цикл вывода символв и кол-ва их вхождений в строку
        foreach (var letter in allLettersSet)
        {
            // Создаём регулярное выражение по символу
            // и получем кол-во вхождений этого символа в строку
            var letterMatches = new Regex(letter.ToString()).Matches(inString).Count();
            matchesDictionary.Add(letter, letterMatches);
        }
        return matchesDictionary;
    }

    // Задание 4
    public static string GetLongestSubString(string inString)
    {
        var vowelStartEnd = new Regex(@"[aeiouy](.*[aeiouy])?");

        return vowelStartEnd.Match(inString).Value;
    }
}