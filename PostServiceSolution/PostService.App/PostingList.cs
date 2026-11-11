using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using PostService.Dtos;

namespace PostService.App;

public partial class PostingList : Form
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("http://localhost:5036/"),
        Timeout = TimeSpan.FromSeconds(10)
    };

    private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public PostingList()
    {
        InitializeComponent();
        gridPostings.AutoGenerateColumns = false;
    }

    private async void PostingList_Load(object? sender, EventArgs e)
    {
        await ReloadAsync(waitForServer: true);
    }

    private async void btnRefresh_Click(object? sender, EventArgs e)
    {
        await ReloadAsync();
    }

    private async Task ReloadAsync(bool waitForServer = false)
    {
        SetButtonsEnabled(false);
        try
        {
            await LoadPostingsAsync(waitForServer);
        }
        finally
        {
            SetButtonsEnabled(true);
        }
    }

    private async Task LoadPostingsAsync(bool waitForServer = false)
    {
        // The first request can arrive while the server is still starting under F5.
        int retries = waitForServer ? 5 : 0;
        while (!IsDisposed)
        {
            try
            {
                var postings = await _httpClient.GetFromJsonAsync<PostingGetDto[]>("postings", _jsonOptions);
                if (!IsDisposed)
                {
                    gridPostings.DataSource = postings ?? Array.Empty<PostingGetDto>();
                }
                return;
            }
            catch (HttpRequestException ex) when (ex.HttpRequestError == HttpRequestError.ConnectionError && retries > 0)
            {
                retries--;
                await Task.Delay(400);
            }
            catch (Exception ex) when (ex is HttpRequestException or OperationCanceledException)
            {
                ShowMessage("Не вдалося отримати дані. Перевірте, чи запущено сервер PostService.");
                return;
            }
            catch (JsonException)
            {
                ShowMessage("Сервер повернув список відправлень у невідомому форматі.");
                return;
            }
        }
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (gridPostings.SelectedRows.Count == 0 ||
            gridPostings.CurrentRow?.DataBoundItem is not PostingGetDto selectedPosting)
        {
            ShowMessage("Виберіть відправлення, яке потрібно видалити.");
            return;
        }

        SetButtonsEnabled(false);
        try
        {
            try
            {
                using var response = await _httpClient.DeleteAsync($"postings/{selectedPosting.Id}");
                if (!response.IsSuccessStatusCode)
                {
                    ShowMessage($"Не вдалося видалити відправлення. Сервер повернув статус {(int)response.StatusCode}.");
                }
            }
            catch (Exception ex) when (ex is HttpRequestException or OperationCanceledException)
            {
                ShowMessage("Не вдалося підтвердити видалення. Перевірте з’єднання із сервером.");
                return;
            }

            await LoadPostingsAsync();
        }
        finally
        {
            SetButtonsEnabled(true);
        }
    }

    private void SetButtonsEnabled(bool enabled)
    {
        if (!IsDisposed)
        {
            btnRefresh.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }
    }

    private void ShowMessage(string message)
    {
        if (!IsDisposed)
        {
            MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
