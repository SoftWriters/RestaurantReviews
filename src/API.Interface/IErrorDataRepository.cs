/******************************************************************************
 * Name: IErrorDataRepository.cs
 * Purpose: Error Repository that implement interface methods for managing
 *           errors
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Model.DTO;

namespace RestaurantReviews.API.Interface
{
    public interface IErrorDataRepository : IAPIDataRepository
    {
        List<ErrorTypeDTO> GetErrorTypes();
        List<ErrorDTO> GetAllErrorsFromDatabase();
        void InsertError(ErrorDTO error);
    }
}
