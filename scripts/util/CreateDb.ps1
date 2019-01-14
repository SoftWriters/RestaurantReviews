Param (
  [String]$connString = "",
  [String]$dbName = ""
)

function ExecuteCommand ($con, $sql){
  try{
    $cmd = $con.CreateCommand(); #New-Object Data.SqlClient.SqlCommand $sql, $con;
    $cmd.CommandText = $sql;
    $cmd.ExecuteNonQuery();
   }
   finally{
    if($cmd){
      $cmd.Dispose();
    }
   }
}

try {
    #connect to db server
    $con = New-Object Data.SqlClient.SqlConnection;
    $con.ConnectionString = $connString;
    $con.Open();
 
 
    #create the db if doesn't already exist
    Write-Host "Dropping and recreating $dbName";
    $sql = "IF EXISTS ( SELECT name FROM sys.databases WHERE name = '$dbName' )
            ALTER DATABASE [$dbname] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE"
    ExecuteCommand $con $sql;        
    
    $sql = "IF EXISTS ( SELECT name FROM sys.databases WHERE name = '$dbName' )
            DROP DATABASE [$dbname]"
    ExecuteCommand $con $sql;       
    

    $sql = "CREATE DATABASE [$dbname];"
    ExecuteCommand $con $sql;           
}
finally{ 
    # Close & Clear all objects.
    $con.Close();
    $con.Dispose();
}