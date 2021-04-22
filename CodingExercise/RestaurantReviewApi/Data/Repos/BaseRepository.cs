using System;
using System.Data.SQLite;
using Dapper;
using Domain;

namespace Data
{
    public class BaseRepository
    {
        public static string ResturantReviewDbFile
        {
            get { return Environment.CurrentDirectory + "\\ResturauntReviewDb.sqlite"; }
        }

        public static SQLiteConnection ResturauntDbConnection()
        {
            return new SQLiteConnection("Data Source=" + ResturantReviewDbFile);
        }

        public static void CreateDatabase()
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                conn.Execute(
                    @"create table City
                      (
                         Id     integer primary key AUTOINCREMENT,
                         Name   varchar(100) not null
                      )");

                conn.Execute(
                    @"create table User
                      (
                         Id     integer primary key AUTOINCREMENT,
                         Name   varchar(100) not null
                      )");

                conn.Execute(
                    @"create table Restaurant
                      (
                         Id     integer primary key AUTOINCREMENT,
                         Name   varchar(100) not null,
                         Description varchar(2000) null,
                         CityId integer not null
                      )");

                conn.Execute(
                    @"create table Review
                      (
                         Id     integer primary key AUTOINCREMENT,
                         RestaurantId integer not null,
                         UserId integer not null,
                         DateSubmitted DateTime not null,
                         Text text null,
                         Rating int not null
                      )");


            }
        }

        public static void InsertCityData()
        {
           
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                conn.Execute(
                    @"
                    INSERT INTO City (Name) VALUES ('Pittsburgh');
                    INSERT INTO City (Name) VALUES ('Youngstown');
                    INSERT INTO City (Name) VALUES ('New Castle');
                    INSERT INTO City (Name) VALUES ('Beaver');
                    INSERT INTO City (Name) VALUES ('Johnstown');
                    INSERT INTO City (Name) VALUES ('Cleveland');
                    ");
            }
        }

        public static void InsertUserData()
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                conn.Execute(
                    @"
                    INSERT INTO User (Name) VALUES ('Karl Tester');
                    INSERT INTO User (Name) VALUES ('Michelle Test');
                    INSERT INTO User (Name) VALUES ('Bob Testman');
                    INSERT INTO User (Name) VALUES ('Uncle Pennybags');
                    ");
            }
        }


    }
}
