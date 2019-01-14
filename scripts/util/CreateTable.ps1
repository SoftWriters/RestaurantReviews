Param (
  [String]$connString = "",
  [String]$tableName = "",
  [String]$script = ""
)

try{
    #connect to db server
    $con = New-Object Data.SqlClient.SqlConnection;
    $con.ConnectionString = $connString;
    $con.Open();
 
 
    #create the db if doesn't already exist
    Write-Host "creating table $tableName";
    $deletion = "IF OBJECT_ID('dbo.$tableName', 'U') IS NOT NULL 
      DROP TABLE dbo.$tableName; 
      "
    $creation = Get-Content $script;

    $sql = "$deletion $creation"

    $cmd = New-Object Data.SqlClient.SqlCommand $sql, $con;
    $cmd.ExecuteNonQuery();
}
finally{ 
    # Close & Clear all objects.
    $cmd.Dispose();
    $con.Close();
    $con.Dispose();
}
