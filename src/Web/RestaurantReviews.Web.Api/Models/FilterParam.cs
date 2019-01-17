using RestaurantReviews.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.Web.Api.Models
{
    /// <summary>
    /// proxy class for generic DbFilter to facilitate model binding
    /// </summary>
    public class FilterParam
    {
        public string Field { get; set; }
        public OperatorEnum Operator { get; set; }
        public object Value { get; set; }
        internal DbFilter<T> ToDbFilter<T>() where T: IEntity{
            return new DbFilter<T>() { Field = Field, Operator = Operator, Value = Value };
		}
    }
}