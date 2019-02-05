using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Infrastructure;
using RestaurantReviews.Domain;
using RestaurantReviews.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantReviews.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {

        IResturantRepository _resturantRepository;

        public RestaurantController(ResturantRepository resturantRepository)
        {
            _resturantRepository = resturantRepository;
        }

        [Route("searchcity/{searchString:minlength(1)}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CityDTO>>> SearchCityByName(string searchString)
        {
            return (new List<City>(await _resturantRepository.SearchCitiesAsync(searchString))).ConvertAll(a => Assembler.MapToDTO(a));
        }

        [Route("getresturantsbycity/{cityId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Resturant>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ResturantDTO>>> GetResturantsByCity(Guid cityId)
        {
            return (new List<Resturant>(await _resturantRepository.GetResturantsByCityAsync(cityId))).ConvertAll(a => Assembler.MapToDTO(a));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> AddRestustrant([FromBody] ResturantDTO resturantDTO)
        {
            if (resturantDTO is null)
            {
                return BadRequest();
            }

            var resturant = Assembler.MapToModel(resturantDTO);

            await _resturantRepository.AddAsync(resturant);

            return CreatedAtAction(nameof(AddRestustrant), new { id = resturant.ResturantId }, null);
        }

        [HttpPut("{resturantId:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateRestustrant(Guid resturantId, [FromBody] ResturantDTO resturantDTO)
        {
            if (resturantId == Guid.Empty || resturantDTO is null)
            {
                return BadRequest();
            }

            var resturantToUpdate = await _resturantRepository.FindAsync(resturantId);
            if (resturantToUpdate is null)
            {
                return NotFound();
            }

            resturantToUpdate.Name = resturantDTO.Name;
            resturantToUpdate.CityId = resturantDTO.CityId;
            resturantToUpdate.FullAddress = resturantDTO.FullAddress;

            await _resturantRepository.UpdateAsync(resturantToUpdate);

            return CreatedAtAction(nameof(UpdateRestustrant), new { id = resturantToUpdate.ResturantId }, null);
        }
    }
}