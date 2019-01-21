$file = 'src\Pkcs11Interop.Android\Pkcs11Interop\Pkcs11Interop.csproj'
$s1 = '<AndroidUseLatestPlatformSdk>False</AndroidUseLatestPlatformSdk>'
$s2 = '<AndroidUseLatestPlatformSdk>True</AndroidUseLatestPlatformSdk>'

(Get-Content $file).replace($s1, $s2) | Set-Content $file
