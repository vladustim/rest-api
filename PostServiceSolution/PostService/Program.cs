using System.Text.Json.Serialization;
using PostService.CommonTypes;
using PostService.Dtos;
using PostService.Mappings;
using PostService.BusinessLogic;
using PostService.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Дозволяємо передавати DeliveryType як число:
// 0 = Department
// 1 = Courier
// 2 = ExpressCourier
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

// Реєстрація сервісу
builder.Services.AddScoped<IPostingService, PostingService>();
var connectionString = builder.Configuration.GetConnectionString("Postings")
    ?? $"Data Source={System.IO.Path.Combine(builder.Environment.ContentRootPath, "PostingDb.db")}";
builder.Services.AddScoped<IPostingRepository>(_ => new PostingRepository(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IPostingRepository>().CreateDb();
}

// Використовуємо порт 5036
app.Urls.Add("http://localhost:5036");

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/postings/quote", (float weight, int depth, int width, int height, DeliveryType deliveryType) =>
{
    if (!float.IsFinite(weight) || weight <= 0 || depth <= 0 || width <= 0 || height <= 0 ||
        !Enum.IsDefined(deliveryType))
    {
        return Results.BadRequest();
    }

    float billableWeight = DeliveryPrice.GetBillableWeight(weight, depth, width, height);
    float price = DeliveryPrice.Calculate(billableWeight, deliveryType);
    return float.IsFinite(price) ? Results.Ok(new { billableWeight, price }) : Results.BadRequest();
});

// GET: отримати всі відправлення
app.MapGet("/postings", (IPostingService postingService) =>
{
    var postings = postingService.GetAll();

    var resultDto = PostingMapper.ToPostingGetDtoList(postings);

    return Results.Ok(resultDto);
});

// GET: отримати відправлення за ID
app.MapGet("/postings/{id}", (
    int id,
    IPostingService postingService) =>
{
    var posting = postingService.Find(id);

    if (posting is null)
    {
        return Results.NotFound();
    }

    var resultDto = PostingMapper.ToPostingGetDto(posting);

    return Results.Ok(resultDto);
});

// POST: створити нове відправлення
app.MapPost("/postings", (
    PostingPostDto postDto,
    IPostingService postingService) =>
{
    var newPosting = PostingMapper.ToPosting(postDto);

    var savedPosting = postingService.Create(newPosting);

    var resultDto = PostingMapper.ToPostingGetDto(savedPosting);

    return Results.Created(
        $"/postings/{resultDto.Id}",
        resultDto);
});

// PUT: оновити відправлення
app.MapPut("/postings/{id}", (
    int id,
    PostingPutDto putDto,
    IPostingService postingService) =>
{
    putDto.Id = id;

    var posting = PostingMapper.ToPosting(putDto);

    var updatedPosting = postingService.Update(posting);

    if (updatedPosting is null)
    {
        return Results.NotFound();
    }

    var resultDto = PostingMapper.ToPostingGetDto(updatedPosting);

    return Results.Ok(resultDto);
});

// DELETE: видалити відправлення
app.MapDelete("/postings/{id}", (
    int id,
    IPostingService postingService) =>
{
    var deleted = postingService.Delete(id);

    if (deleted == 0)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});

app.Run();
