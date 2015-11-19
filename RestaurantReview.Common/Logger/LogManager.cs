using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReview.Common.Logger
{
    public class LogManager : LoggerBase
    {
        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        public void LogError(Type type, Exception e)
        {
            this.LogError(type.FullName, e);
        }

        public void LogInfo(Type type, Exception e)
        {
            this.LogInfo(type.FullName);
        }
    }
}
