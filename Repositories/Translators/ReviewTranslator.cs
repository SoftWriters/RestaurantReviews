using DTOs;
using ORMLayer.TableDefinitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Translators
{
  public class ReviewTranslator : ITranslator<ReviewDTO, Review>
  {
    public Review DtoToEntity(ReviewDTO dto)
    {
      return new Review
      {
        RestaurantId = dto.RestaurantId,
        UserId = dto.UserId,
        Stars = dto.Stars,
        ReviewText = dto.ReviewText
      };
    }

    public ReviewDTO EntityToDto(Review entity)
    {
      return new ReviewDTO
      {
        Id = entity.Id,
        ReviewText = entity.ReviewText,
        Stars = entity.Stars,
        UserId = entity.UserId,
        RestaurantId = entity.RestaurantId
      };
    }
  }
}
