using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RestaurantReviews.Data
{
    public class State
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Code { get; set; }
    }

    public static class States
    {
        private static readonly Lazy<IEnumerable<State>> _states = new Lazy<IEnumerable<State>>(() =>
        {
            using (var stream = typeof(State).Assembly.GetManifestResourceStream(typeof(State), "Seed.states.json"))
            using (var sr = new StreamReader(stream))
            {
                var txt = sr.ReadToEnd();
                return JsonSerializer.Deserialize<IEnumerable<State>>(txt);
            }
        });

        private static readonly Lazy<Dictionary<string, State>> _statesByCode = new Lazy<Dictionary<string, State>>(() => _states.Value.ToDictionary(p => p.Code));

        public static IEnumerable<State> All => _states.Value;
        public static IReadOnlyDictionary<string, State> AllByCode => _statesByCode.Value;
    }
}
