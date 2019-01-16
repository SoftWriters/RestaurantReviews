$scriptPath = Split-Path -parent $PSCommandPath
Invoke-Expression "& `"$scriptPath\Setup.ps1`" -connString `"$connString`" -dbName `"RestaurantReviewsCI`""