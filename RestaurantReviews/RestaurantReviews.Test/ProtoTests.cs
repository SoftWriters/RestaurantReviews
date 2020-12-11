using RestaurantReviews.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RestaurantReviews.Test
{
    public class ProtoTests
    {
        private readonly ITestOutputHelper log;

        public ProtoTests(ITestOutputHelper log)
        {
            this.log = log;
        }

        /// <summary>
        /// I downloaded the original states 
        /// </summary>
        /// <returns></returns>
        [DebugOnlyFact]
        public Task Generate_StatesWithIds()
        {
            throw new NotImplementedException();
        }
    }
}
