using DTOs;
using ORMLayer.TableDefinitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Translators
{
  public class RestaurantTranslator : ITranslator<RestaurantDTO, Restaurant>
  {
    public Restaurant DtoToEntity(RestaurantDTO dto)
    {
      return new Restaurant { 
        Name = dto.Name,
        Address = dto.Address,
        Cuisine = dto.Cuisine,
        CityId = dto.CityId,
      };
    }

    public RestaurantDTO EntityToDto(Restaurant entity)
    {
      return new RestaurantDTO { 
        Name = entity.Name,
        Address = entity.Address,
        Cuisine = entity.Cuisine,
        CityId = entity.CityId,
        Id = entity.Id
      };
    }
  }
}
