
void main(String[] args)
{
    Console.WriteLine("Введите строку");
    string inputStr = Console.ReadLine();

    Console.WriteLine(firstTaskReverse(inputStr));
}
// Задание 1
string firstTaskReverse(string userString)
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