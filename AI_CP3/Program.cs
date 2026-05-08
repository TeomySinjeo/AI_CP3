using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using var client = new HttpClient();

        while (true)
        {
            Console.Write("Вы: ");
            var input = Console.ReadLine();

            var request = new
            {
                model = "gpt-oss:120b-cloud",
                prompt = input,
                stream = false
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:11434/api/generate", content);
            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);
            var result = doc.RootElement.GetProperty("response").GetString();

            Console.WriteLine($"ChatGPT: {result}");
        }
    }
}