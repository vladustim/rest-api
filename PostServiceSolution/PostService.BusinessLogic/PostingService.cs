using PostService.CommonTypes;
using PostService.DataAccess;
using PostService.Models;

namespace PostService.BusinessLogic;

public class PostingService(IPostingRepository repository) : IPostingService
{
    public Posting Create(Posting newPosting)
    {
        newPosting.CreatedAt = DateTime.UtcNow;
        newPosting.Price = CalculatePrice(newPosting.Weight, newPosting.DeliveryType);
        newPosting.Id = repository.Create(newPosting);
        return newPosting;
    }

    public List<Posting> GetAll() => repository.GetList();

    public Posting? Find(int postingId) => repository.GetById(postingId);

    public Posting? Update(Posting posting)
    {
        posting.Price = CalculatePrice(posting.Weight, posting.DeliveryType);
        return repository.Update(posting);
    }

    public int Delete(int postingId) => repository.Delete(postingId);

    private static float CalculatePrice(float weight, DeliveryType deliveryType)
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
