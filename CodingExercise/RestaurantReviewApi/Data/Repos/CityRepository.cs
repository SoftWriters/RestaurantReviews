using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public City ReadCity(int id)
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                City result = conn.Query<City>($"SELECT Id, Name FROM City WHERE Id= @id", new { id }).FirstOrDefault();

                return result;
            }
        }

        public IList<City> ReadAllCities()
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                var result = conn.Query<City>($"SELECT Id, Name FROM City").ToList();

                return result;
            }
        }

        public void CreateCity(City city)
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                city.Id = conn.Query<int>(
                    $"INSERT INTO (Name) " +
                    $"VALUES (@Name); " +
                    $"SELECT last_insert_rowid()", city).First();
            }
        }
    }
}
