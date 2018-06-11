/******************************************************************************
 * Name: APIBaseController.cs
 * Purpose: Base API Controller abstract class for all other Controllers
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.API.Model.DTO;

namespace RestaurantReviews.API.Controllers
{
    public abstract class APIBaseController : Controller
    {
        protected static APIResponseDTO GetErrorDTO(Exception ex)
        {
            return new APIResponseDTO()
            {
                Error = new ErrorDTO()
                {
                    ErrorID = ex.HResult,
                    RequestID = 0,
                    ErrorDateTime = DateTime.Now,
                    ErrorType = new ErrorTypeDTO()
                    {
                        ErrorTypeID = 1,
                        ErrorName = string.Empty,
                        Description = ex.Source
                    },
                    ErrorMessage = ex.Message,
                    ErrorDetail = ex.StackTrace
                },
                Data = null
            };
        }

        protected static APIResponseDTO GetDataDTO(APIBaseDTO data)
        {
            return new APIResponseDTO()
            {
                Error = null,
                Data = data
            };
        }
    }
}
