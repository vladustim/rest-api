using PostalApi.Models;

namespace PostalApi.Services;

public class PostingService : IPostingService
{
    private static readonly List<Posting> _postings = new();

    public Posting Create(Posting newPosting)
    {
        var maxId = 1;

        if (_postings.Count > 0)
        {
            maxId = _postings.Max(p => p.Id) + 1;
        }

        newPosting.Id = maxId;
        newPosting.CreatedAt = DateTime.UtcNow;

        newPosting.Price = CalculatePrice(
            newPosting.Weight,
            newPosting.DeliveryType);

        _postings.Add(newPosting);

        return newPosting;
    }

    public List<Posting> GetAll()
    {
        return _postings;
    }

    public Posting? Find(int postingId)
    {
        return _postings.FirstOrDefault(p => p.Id == postingId);
    }

    public Posting? Update(Posting posting)
    {
        var existingPosting = _postings
            .FirstOrDefault(p => p.Id == posting.Id);

        if (existingPosting is null)
        {
            return null;
        }

        existingPosting.From = posting.From;
        existingPosting.To = posting.To;
        existingPosting.Content = posting.Content;
        existingPosting.DeliveryType = posting.DeliveryType;
        existingPosting.Weight = posting.Weight;
        existingPosting.Width = posting.Width;
        existingPosting.Height = posting.Height;
        existingPosting.Depth = posting.Depth;
        existingPosting.Value = posting.Value;

        return existingPosting;
    }

    public int Delete(int postingId)
    {
        var posting = _postings
            .FirstOrDefault(p => p.Id == postingId);

        if (posting is null)
        {
            return 0;
        }

        _postings.Remove(posting);

        return 1;
    }

    private static float CalculatePrice(
        float weight,
        DeliveryType deliveryType)
    {
        float pricePerKg = deliveryType switch
        {
            DeliveryType.Department => 10f,
            DeliveryType.Courier => 20f,
            DeliveryType.ExpressCourier => 30f,
            _ => 10f
        };

        return weight * pricePerKg;
    }
}