# this is a throwaway password - if this were something more secure, 
#  we would not check this in or use an environment variable
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=5g5R%pY!G8kt' -p 1433:1433 -d microsoft/mssql-server-linux:2017-latest