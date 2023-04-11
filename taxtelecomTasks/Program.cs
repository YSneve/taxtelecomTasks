
using System.Text.RegularExpressions;

namespace taxtelecomTasks;

internal class Program
{
    static void Main(String[] args)
    {

        Console.WriteLine("Введите строку");
        var inputStr = Console.ReadLine();


        if (IsAllLower(inputStr))
        {
        
            var processedString = FirstTaskReverse(inputStr);
            var longestString = GetLongestSubString(processedString);
            var sortedString = StringSort(processedString);
            var deletedCharString = delRandChar(processedString);


            Console.WriteLine("Результат: {0}\n", processedString);

            WriteLettersMatches(processedString);


            Console.WriteLine("Самая длинная подстрока: {0}\n" +
                "Отсортированная обработанная строка: {1}\n" +
                "Строка, в которой удалён символ на {2} позиции: {3}",  longestString, sortedString, deletedCharString.Item2, deletedCharString.Item1);   


        }
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

    
    private static void WriteErrors(string inString)
    {
        //Регулярное выражение, проверяющее на то, что ряд символов
        //Является символом из диапазона от a до z 
        var lowerLetterExpr = new Regex(@"([a-z]*)");

        //Удаление подходящих для ввода символов
        //Удаление повторяющихся неподходящих для вывода символов и оставление неповторяющихся символов путем добавления в hashset
        var wrongLettersSet = new HashSet<char>(lowerLetterExpr.
                Replace(inString, "").
                ToCharArray());

        Console.WriteLine("Ошибка! Введены не подходящие символы." +
                          "\nСледующие, из введенных символов, не являются подходящими:" +
                          "\n{0}", new string(wrongLettersSet.ToArray()));
    }

    //Задание 3
    private static void WriteLettersMatches(string inString)
    {
        // Множество всех символов
        var allLettersSet = new HashSet<char>(inString.ToCharArray());

        // Цикл вывода символв и кол-ва их вхождений в строку
        Console.WriteLine("Символ : Кол-во вхождений");
        foreach (var letter in allLettersSet)
        {
            // Создаём регулярное выражение по символу
            // и получем кол-во вхождений этого символа в строку
            var letterMatches = new Regex(letter.ToString()).Matches(inString).Count();
            Console.WriteLine("{0} : {1}", letter, letterMatches);
        }
    }

    // Задание 4
    private static string GetLongestSubString(string inString) 
    {
        var vowelStartEnd = new Regex(@"[aeiouy](.*[aeiouy])?");

        return vowelStartEnd.Match(inString).Value;
    }

    // Задание 5
    private static string StringSort(string inString)
    {
        Console.WriteLine("1 - QuickSort\n" +
            "2 - TreeSort\n" +
            "Выберите сортировку: ");

        switch (Console.ReadLine())
        {
            case "1":
                return QuickSort.SortString(inString);

            case "2":
                return TreeSort.SortString(inString);

            default:
                return "Ошибка выбора сортировки!";

        }
    }

    // Задание 6
    private static (string, int) delRandChar(string inString)
    {
        // Запрашиваем случайное число
        int delIndex = getRandNum(inString.Length);

        // Удаляем 1 элемент начиная с delIndex позиции и возвращаем полученную строку
        return (inString.Remove(delIndex, 1), delIndex);
    }  

    private static int getRandNum(int max)
    {
        // Определяем клиент
        var httpClient = new HttpClient();

        // Делаем попытку запроса на сайт randomapi
        try
        {
            // Ссылка с запросом
            // Указываем максимальное число на 1 меньше т.к Api возвращает случайное число до max включительно
            var requestString = string.Format("http://www.randomnumberapi.com/api/v1.0/random?min=0&max={0}", max - 1);

            // Получаем результат
            var result = httpClient.GetStringAsync(requestString).Result[1];

            // Конвертация в int
            return result - '0';
        }
        // При отсутствии соединения с Api
        catch (Exception ex)
        {
            Console.WriteLine("В ходе выполнения запроса возникла следующая ошибка: {0}", ex.Message);

            // Указываем max как максимальное число т.к Random возвращает число до max не включая
            return new Random().Next(max);
        }
    }
}