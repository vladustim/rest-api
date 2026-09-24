using PostalApi.Dtos;
using PostalApi.Models;

namespace PostalApi.Mappings;

public static class PostingMapper
{
    public static Posting ToPosting(PostingPostDto dto)
    {
        return new Posting
        {
            From = dto.From,
            To = dto.To,
            Content = dto.Content,
            DeliveryType = dto.DeliveryType,
            Weight = dto.Weight,
            Width = dto.Width,
            Height = dto.Height,
            Depth = dto.Depth,
            Value = dto.Value
        };
    }

    public static Posting ToPosting(PostingPutDto dto)
    {
        return new Posting
        {
            Id = dto.Id,
            From = dto.From,
            To = dto.To,
            Content = dto.Content,
            DeliveryType = dto.DeliveryType,
            Weight = dto.Weight,
            Width = dto.Width,
            Height = dto.Height,
            Depth = dto.Depth,
            Value = dto.Value
        };
    }

    public static PostingGetDto ToPostingGetDto(Posting posting)
    {
        return new PostingGetDto
        {
            Id = posting.Id,
            From = posting.From,
            To = posting.To,
            Content = posting.Content,
            DeliveryType = posting.DeliveryType,
            Weight = posting.Weight,
            Width = posting.Width,
            Height = posting.Height,
            Depth = posting.Depth,
            Value = posting.Value,
            Price = posting.Price,
            CreatedAt = posting.CreatedAt
        };
    }

    public static List<PostingGetDto> ToPostingGetDtoList(
        List<Posting> postings)
    {
        return postings.Select(ToPostingGetDto).ToList();
    }
}