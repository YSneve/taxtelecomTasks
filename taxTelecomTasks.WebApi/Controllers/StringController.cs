using Microsoft.AspNetCore.Mvc;
using taxTelecomTasks.Core;
namespace taxTelecomTasks.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StringController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public StringController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> ProceedString(string inputString, [FromQuery] SortingModel sortModel)
    {
        if (!StringProcessor.IsAllLower(inputString, out var invalidChars))
        {
            return BadRequest(new
            {
                message = "Были введены неподходящие символы",
                invalidCharacters = invalidChars.Select(x => x)
            });
        }
        
        var reversedString = StringProcessor.ReverseString(inputString);

        var sortedProceededString = sortModel.Sorting switch
        {
            SortingEnum.Quick => QuickSort.SortString(reversedString),
            SortingEnum.Tree => TreeSort.SortString(reversedString),
            _ => string.Empty
        };

        var stringWithRemovedIndex = await DelRandChar(reversedString);

        return Ok(new
        {
            proceededString = reversedString,

            countCharacters = StringProcessor.GetLetterMatches(reversedString),

            longestVowelSubstring = StringProcessor.GetLongestSubString(reversedString),

            sortedString = sortedProceededString,

            stringWithRemovedChar = stringWithRemovedIndex.Item1,

            removalIndex = stringWithRemovedIndex.Item2
        });
    }

   
    private async Task<(string, int)> DelRandChar(string inString)
    {
        // Запрашиваем случайное число
        int delIndex = await GetRandNum(inString.Length);

        // Удаляем 1 элемент начиная с delIndex позиции и возвращаем полученную строку
        return (inString.Remove(delIndex, 1), delIndex);
    }

    private async Task<int> GetRandNum(int max)
    {
            
        // Делаем попытку запроса на сайт randomapi
        try
        {
            // Ссылка с запросом
            // Указываем максимальное число на 1 меньше т.к Api возвращает случайное число до max включительно
            var requestString = string.Format("http://www.randomnumberapi.com/api/v1.0/random?min=0&max={0}", max - 1);

            // Получаем результат
            var response = await _httpClient.GetAsync(requestString);
            var reslut = await response.Content.ReadAsStringAsync();

            // Конвертация в int
            return int.Parse(reslut);
        }
        // При отсутствии соединения с Api
        catch (Exception ex)
        {
            // Указываем max как максимальное число т.к Random возвращает число до max не включая
            return new Random().Next(max);
        }
    }
}