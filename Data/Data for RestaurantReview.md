Data for RestaurantReview
-------------------------


Steps:

1. Create Machine
2. Install PostGis apt-get install postgis
3. Create user : restdb
4. Create Database : restdb
5. Setup PostGIS

```restdb=# CREATE EXTENSION postgis;
CREATE EXTENSION
restdb=# SELECT PostGIS_version();
            postgis_version
---------------------------------------
 2.2 USE_GEOS=1 USE_PROJ=1 USE_STATS=1
(1 row)
```

6. Create Table Restaurant Table.

This will generate the Restaurant table and setup the PostGIS portions.  The rest of the tables will be generated in code by Gorm.


```
DROP TABLE restaurants;

CREATE TABLE restaurants
(
  gid serial NOT NULL,
  name character varying(150),
  address character varying(150),
  latitude numeric(12,8),
  longitude numeric(12,8),
  avg_rating numeric(3,2),
  the_geom geometry,
  the_geog geography(POINT,4326),
  CONSTRAINT restaurants_pkey PRIMARY KEY (gid),
  CONSTRAINT enforce_dims_the_geom CHECK (st_ndims(the_geom) = 2),
  CONSTRAINT enforce_geotype_geom CHECK (geometrytype(the_geom) = 'POINT'::text OR the_geom IS NULL),
  CONSTRAINT enforce_srid_the_geom CHECK (st_srid(the_geom) = 4326)
);

-- Index: restaurants_the_geom_gist

-- DROP INDEX restaurants_the_geom_gist;

CREATE INDEX restaurants_the_geom_gist
  ON restaurants
  USING gist
  (the_geom );

CREATE INDEX restaurants_the_geog_gist
  ON restaurants
  USING gist
  (the_geog );

```

8. Prep the Data:

Got the data from <https://catalog.data.gov/dataset/allegheny-county-food-facilities>

a) Tailor it down to Restaurants
grep Restaurant RestaurentData.csv > Restaurants_only.csv

b)
cat RestaurentData.csv |grep Restaurant | grep -v without > Restaurants_noLiquor

c) Scripts to extract address information and format it for use with curl.

```
awk -F "\"*,\"*" '{print "address="$2 " "  $3 , $4 ", " $5 " " $6 "key=<GOOGLE GEO CODE KEY>" }' Restaurants_noLiquor.csv > RestAddresses.csv

awk -F "\"*,\"*" '{print $1 "|" $2 " "  $3 , $4 ", " $5 " " $6 "|" $9 "|" $10}' Restaurants_LiquorWLatLong.csv > Restaurants_LiquorWLatLongEdite.csv
```


d) Create a CSV file that includes Lat/Long
```
cat Downloads/15217Rests.txt| xargs -I {} curl -G https://maps.googleapis.com/maps/api/geocode/json --data-urlencode {} |jq '.results| map(.formatted_address, .geometry.location.lat, .geometry.location.lng ) |@csv ' >test.csv

```
Some manual editing is required to verify that the results have not returned an extra field.



8. Populate the tables.

restdb# \copy restaurants(name,address,latitude,longitude) FROM '/tmp/Restaurants_LiquorWLatLongEdite.csv' DELIMITERS E'\t' CSV HEADER;

restdb# UPDATE restaurants
SET the_geom = ST_GeomFromText('POINT(' || longitude || ' ' || latitude || ')',4326);


Example Proximity Queries
-------------------------

```SELECT COUNT(*)
FROM restaurants
WHERE ST_DWithin(the_geom, ST_SetSRID(ST_Point(-79.936378, 40.425579), 4326), 30000);
```

```
SELECT * FROM restaurants WHERE ST_DWithin(the_geom,  ST_GeomFromText('POINT(-79.936378, 40.425579)', 4326)::geography, 1000) AND NAME!=%s;
```


```
SELECT * FROM restaurants WHERE GeometryType(ST_Centroid(the_geom)) = 'POINT' AND ST_Distance_Sphere( ST_Point(ST_X(ST_Centroid(the_geom)), ST_Y(ST_Centroid(the_geom))), (ST_MakePoint(-79.936378, 40.425579))) <= 1609.34
```

```
select *
 from restaurants s
 where ST_DWITHIN(Geography(ST_Transform(s.geom,4326)), ST_Point(-79.936378 40.425579) ,1);`
```