using PostalApi.Models;

namespace PostalApi.Services;

public interface IPostingService
{
    Posting Create(Posting newPosting);

    List<Posting> GetAll();

    Posting? Find(int postingId);

    Posting? Update(Posting posting);

    int Delete(int postingId);
}