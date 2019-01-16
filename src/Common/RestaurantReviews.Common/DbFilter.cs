using RestaurantReviews.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace RestaurantReviews.Common
{
    public class DbFilter<T> where T:IEntity
    {
        IEnumerable<PropertyInfo> _props;
        Type _type;
        public string Field { get; set; }
        public OperatorEnum Operator { get; set; }
        public object Value { get; set; }
        public DbFilter()
        {
            _type = typeof(T);
            _props = _type.GetProperties().ToList();
            
        }

        public string GetFilterSql(string paramName)
        {
            var prop = _props.FirstOrDefault(x => x.Name.Equals(Field, StringComparison.InvariantCultureIgnoreCase));
            if (prop == null)
            {
                throw new InvalidFilterFieldException(string.Format("the field {0} does not exist on the entity {1}", Field, _type.Name));
            }
            //todo: this is overly simplified
            //supporting ranges of values is one example where this could be made much more robust
            var formatString = " {0} {1} @{2}";
            var sqlColumn = GetSqlColumnName(prop);
            return string.Format(formatString, sqlColumn, GetOperatorText(Operator), paramName);
        }

        

        private string GetOperatorText(OperatorEnum operatorEnum)
        {
            switch (operatorEnum)
            {
                case OperatorEnum.Equal:
                    return "=";
                case OperatorEnum.Like:
                    return "like";
                default:
                    throw new NotSupportedException(string.Format("{0} is not supported", operatorEnum));
            }

        }

        private object GetSqlColumnName(PropertyInfo prop)
        {
            if (prop == null) return null;

            var attrib = (ColumnAttribute)Attribute.GetCustomAttribute(prop, typeof(ColumnAttribute), false);
            return attrib == null ? prop.Name : attrib.Name;
        }

        private string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return attrib?.Description;
        }

    }

    //todo: this is very rudimentary
    //ideally the type of field would be mapped to supported operators
    //and custom attributes could be decorated on specific fields to denote overrides
    public enum OperatorEnum
    {
        Equal,
        Like
    }
}