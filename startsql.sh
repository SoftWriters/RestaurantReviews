#!/bin/bash

# this is a throwaway password - if this were something more secure, 
#  we would not check this in or use an environment variable
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=5g5R%pY!G8kt' --name restaurant-server -p 1433:1433 \
    -d microsoft/mssql-server-linux:2017-latest

docker exec -it restaurant-server /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '5g5R%pY!G8kt' \
   -Q 'CREATE DATABASE RestaurantDB'

docker exec -it restaurant-server /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '5g5R%pY!G8kt' \
   -Q 'CREATE LOGIN RestaurantService WITH PASSWORD = "MyReallyBadPassword1"'

docker exec -it restaurant-server /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '5g5R%pY!G8kt' \
   -d RestaurantDB \
   -Q 'CREATE USER [RestaurantService] FOR LOGIN [RestaurantService]'

docker exec -it restaurant-server /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '5g5R%pY!G8kt' \
   -d RestaurantDB \
   -Q "EXEC sp_addrolemember N'db_owner', N'RestaurantService'"
