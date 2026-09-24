using System.Text.Json.Serialization;
using PostalApi.Dtos;
using PostalApi.Mappings;
using PostalApi.Services;

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

var app = builder.Build();

// Використовуємо порт 5036
app.Urls.Add("http://localhost:5036");

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