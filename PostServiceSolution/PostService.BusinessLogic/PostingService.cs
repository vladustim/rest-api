using PostService.CommonTypes;
using PostService.DataAccess;
using PostService.Models;

namespace PostService.BusinessLogic;

public class PostingService(IPostingRepository repository) : IPostingService
{
    public Posting Create(Posting newPosting)
    {
        newPosting.CreatedAt = DateTime.UtcNow;
        newPosting.Price = CalculatePrice(newPosting);
        newPosting.Id = repository.Create(newPosting);
        return newPosting;
    }

    public List<Posting> GetAll() => repository.GetList();

    public Posting? Find(int postingId) => repository.GetById(postingId);

    public Posting? Update(Posting posting)
    {
        posting.Price = CalculatePrice(posting);
        return repository.Update(posting);
    }

    public int Delete(int postingId) => repository.Delete(postingId);

    private static float CalculatePrice(Posting posting)
    {
        float billableWeight = DeliveryPrice.GetBillableWeight(
            posting.Weight, posting.Depth, posting.Width, posting.Height);
        return DeliveryPrice.Calculate(billableWeight, posting.DeliveryType);
    }
}
