# For creating self-signed test certificates
# NOTE: Needs run as administrator
# NOTE: $dnsName must match the host name of the server \ uri of the service

$dnsName = "DESKTOP-JJLKN7I"
$rootDnsName = "RootCA for " + $dnsName
$outputDir = "C:\testCertificates\" + $dnsName + "\"
$rootPfxPath = $outputDir + 'root.pfx'
$rootCrtPath = $outputDir + 'root.crt'
$pfxPath = $outputDir + 'cert.pfx'
$crtPath = $outputDir + 'cert.crt'

New-Item -Path $outputDir -ItemType Directory

$rootCert = New-SelfSignedCertificate -CertStoreLocation Cert:\LocalMachine\My -DnsName $rootDnsName -FriendlyName "BryanRootCA" -NotAfter (Get-Date).AddYears(1) -KeyUsage CertSign
[System.Security.SecureString]$rootcertPassword = ConvertTo-SecureString -String "password" -Force -AsPlainText
[String]$rootCertPath = Join-Path -Path 'cert:\LocalMachine\My\' -ChildPath "$($rootcert.Thumbprint)"

Export-PfxCertificate -Cert $rootCertPath -FilePath $rootPfxPath -Password $rootcertPassword
Export-Certificate -Cert $rootCertPath -FilePath $rootCrtPath

$testCert = New-SelfSignedCertificate -CertStoreLocation Cert:\LocalMachine\My -DnsName $dnsName -KeyExportPolicy Exportable -KeyLength 2048 -KeyUsage DigitalSignature,KeyEncipherment -Signer $rootCert
[String]$testCertPath = Join-Path -Path 'cert:\LocalMachine\My\' -ChildPath "$($testCert.Thumbprint)"
Export-PfxCertificate -Cert $testCertPath -FilePath $pfxPath -Password $rootcertPassword
Export-Certificate -Cert $testCertPath -FilePath $crtPath