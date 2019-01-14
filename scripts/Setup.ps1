
$scriptPath = Split-Path -parent $PSCommandPath
$config = Get-Content -Raw -Path $scriptPath\config.json |
    ConvertFrom-Json 

$dbServer = $config.dbServer
$connString = "Data Source=$dbServer;Initial Catalog=master;Integrated Security=True;"; 
Invoke-Expression "& `"$scriptPath\util\CreateDb.ps1`" -connString `"$connString`" -dbName `"RestaurantReviews`""


$connString = "Data Source=$dbServer;Initial Catalog=RestaurantReviews;Integrated Security=True;";

$tablesToReCreate = @();
Get-ChildItem "$scriptPath\table" -Filter *.sql | 
Foreach-Object {
    $basename = $_.BaseName;
    $fullname = $_.FullName;
    $obj = [PSCustomObject]@{
        Basename  = $basename
        Fullname = $fullname
    }
    try{
      Invoke-Expression "& `"$scriptPath\util\CreateTable.ps1`" -connString `"$connString`" -tableName `"$basename`" -script `"$fullname`""
    }
    catch{
      $tablesToReCreate += $obj;
    }
}
$tablesToCreate = $tablesToReCreate;
$tablesToReCreate = @();
$i=0;
while (($tablesToCreate.Length -gt 0) -and ($i -lt 10)){
  $tablesToCreate | Foreach-Object  {
    try{
      $basename=$_.Basename;
      $fullname=$_.Fullname;
      
      Invoke-Expression "& `"$scriptPath\util\CreateTable.ps1`" -connString `"$connString`" -tableName `"$basename`" -script `"$fullname`""
    }
    catch{
    Write-Host "creating table failed";
      Write-Host "failed,  adding to retry list";
      $tablesToReCreate += $_;
    }
  }
  $tablesToCreate = $tablesToReCreate;
  $tablesToReCreate = @();
  $i = $i+1;
  
}


    




