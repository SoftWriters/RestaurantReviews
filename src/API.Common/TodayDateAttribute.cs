/******************************************************************************
 * Name: TodayDateAttribute.cs
 * Purpose: Validate Attribute to used on DateTime properties to enforce today's
 *            date on the property value
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.API.Common
{
    public class TodayDateAttribute : RangeAttribute
    {
        public TodayDateAttribute() : base(typeof(DateTime), 
            DateTime.Today.ToString(),
            DateTime.Now.AddMinutes(1).ToString())
        {

        }
    }
}
