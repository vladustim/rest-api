using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using PostService.Dtos;

namespace PostService.App;

internal static class PostingApi
{
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri("http://localhost:5036/"),
        Timeout = TimeSpan.FromSeconds(10)
    };

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public static async Task<PostingGetDto[]> GetListAsync(bool waitForServer)
    {
        int retries = waitForServer ? 5 : 0;
        while (true)
        {
            try
            {
                return await Client.GetFromJsonAsync<PostingGetDto[]>("postings", JsonOptions)
                    ?? throw new JsonException("Expected a list of postings.");
            }
            catch (HttpRequestException ex) when (ex.HttpRequestError == HttpRequestError.ConnectionError && retries-- > 0)
            {
                await Task.Delay(400);
            }
        }
    }

    public static async Task<PostingGetDto> GetAsync(int id)
    {
        return await Client.GetFromJsonAsync<PostingGetDto>($"postings/{id}", JsonOptions)
            ?? throw new JsonException("Expected a posting.");
    }

    public static async Task CreateAsync(PostingPostDto posting)
    {
        using var response = await Client.PostAsJsonAsync("postings", posting, JsonOptions);
        response.EnsureSuccessStatusCode();
    }

    public static async Task UpdateAsync(PostingPutDto posting)
    {
        using var response = await Client.PutAsJsonAsync($"postings/{posting.Id}", posting, JsonOptions);
        response.EnsureSuccessStatusCode();
    }

    public static async Task DeleteAsync(int id)
    {
        using var response = await Client.DeleteAsync($"postings/{id}");
        response.EnsureSuccessStatusCode();
    }

    public static bool IsOperationError(Exception error) =>
        error is HttpRequestException or OperationCanceledException or JsonException;

    public static string DescribeError(Exception error) => error switch
    {
        HttpRequestException { StatusCode: HttpStatusCode.NotFound } =>
            "Відправлення більше не існує на сервері.",
        HttpRequestException { StatusCode: HttpStatusCode.BadRequest or HttpStatusCode.UnprocessableEntity } =>
            "Сервер відхилив дані. Перевірте заповнені поля.",
        HttpRequestException { StatusCode: { } status } =>
            $"Сервер повернув помилку HTTP {(int)status}.",
        HttpRequestException => "Перевірте, чи запущено сервер PostService на порту 5036.",
        OperationCanceledException => "Сервер не відповів вчасно. Оновіть список, щоб перевірити стан даних.",
        _ => "Сервер повернув дані в невідомому форматі."
    };
}
