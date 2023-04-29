using Microsoft.AspNetCore.Mvc;
using taxTelecomTasks.Core;


namespace taxTelecomTasks.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StringController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;
    
    private static SemaphoreSlim Semaphore;

    public static void initSemaphore(int limit)
    {
        Semaphore = new SemaphoreSlim(limit);
    }
    
    public StringController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    
    [HttpGet]
    public async Task<IActionResult> ProceedString(string inputString, [FromQuery] SortingModel sortModel)
    {
        if (Semaphore.CurrentCount == 0)
        {
            return StatusCode(503, "Service Unavailable");
        }
        
        await Semaphore.WaitAsync();
        try
        {
            var blackList = _configuration.GetSection("Settings:BlackList").Get<string[]>();
        
            if (StringProcessor.isBlackListed(blackList, inputString))
                return BadRequest(new
                {
                    message = "String is in blacklist"
                });

            if (!StringProcessor.IsAllLower(inputString, out var invalidChars))
            {
                return BadRequest(new
                {
                    message = "Input contains unsupported characters",
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

            //_semaphore.Release();
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
        finally
        {
            Semaphore.Release();
        }
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
            // Получаем ссылку на апи из конфигурационного файла
            var api = _configuration.GetValue<string>("RandomApi");

            // Ссылка с запросом
            // Указываем максимальное число на 1 меньше т.к Api возвращает случайное число до max включительно
            var requestString = string.Format("{0}random?min=0&max={1}", api, max - 1);
           
            // Получаем результат
            var response = await _httpClient.GetAsync(requestString);
            var reslut = await response.Content.ReadAsStringAsync();
            // Конвертация в int
            return int.Parse(reslut[1].ToString());
        }
        // При отсутствии соединения с Api
        catch (Exception ex)
        {
            Console.WriteLine("ERROR | {0}", ex.Message);
            // Указываем max как максимальное число т.к Random возвращает число до max не включая
            return new Random().Next(max);
        }
    }
}