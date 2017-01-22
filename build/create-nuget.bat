@setlocal

@rem Delete output directory
rmdir /S /Q nuget

@rem Create output directories for classic package
mkdir nuget\classic\lib\net20 || goto :error
mkdir nuget\classic\lib\net40 || goto :error
mkdir nuget\classic\lib\net45 || goto :error
mkdir nuget\classic\lib\sl5 || goto :error
mkdir nuget\classic\lib\netstandard1.3 || goto :error
mkdir nuget\classic\lib\monoandroid2.3 || goto :error
mkdir nuget\classic\lib\xamarinios1.0 || goto :error

@rem Create output directories for strongly named package
mkdir nuget\strongname\lib\net20 || goto :error
mkdir nuget\strongname\lib\net40 || goto :error
mkdir nuget\strongname\lib\net45 || goto :error
mkdir nuget\strongname\lib\sl5 || goto :error
mkdir nuget\strongname\lib\netstandard1.3 || goto :error

@rem Copy assemblies to classic package
copy net20\Pkcs11Interop.dll nuget\classic\lib\net20 || goto :error
copy net20\Pkcs11Interop.xml nuget\classic\lib\net20 || goto :error
copy net40\Pkcs11Interop.dll nuget\classic\lib\net40 || goto :error
copy net40\Pkcs11Interop.xml nuget\classic\lib\net40 || goto :error
copy net45\Pkcs11Interop.dll nuget\classic\lib\net45 || goto :error
copy net45\Pkcs11Interop.xml nuget\classic\lib\net45 || goto :error
copy sl5\Pkcs11Interop.Silverlight.dll nuget\classic\lib\sl5 || goto :error
copy sl5\Pkcs11Interop.Silverlight.xml nuget\classic\lib\sl5 || goto :error
copy netstandard1.3\Pkcs11Interop.DotNetCore.dll nuget\classic\lib\netstandard1.3 || goto :error
copy netstandard1.3\Pkcs11Interop.DotNetCore.xml nuget\classic\lib\netstandard1.3 || goto :error
copy monoandroid2.3\Pkcs11Interop.Android.dll nuget\classic\lib\monoandroid2.3 || goto :error
copy monoandroid2.3\Pkcs11Interop.Android.xml nuget\classic\lib\monoandroid2.3 || goto :error
copy xamarinios1.0\Pkcs11Interop.iOS.dll nuget\classic\lib\xamarinios1.0 || goto :error
copy xamarinios1.0\Pkcs11Interop.iOS.xml nuget\classic\lib\xamarinios1.0 || goto :error

@rem Copy assemblies to strongly named package
copy net20\Pkcs11Interop.StrongName.dll nuget\strongname\lib\net20 || goto :error
copy net20\Pkcs11Interop.StrongName.xml nuget\strongname\lib\net20 || goto :error
copy net40\Pkcs11Interop.StrongName.dll nuget\strongname\lib\net40 || goto :error
copy net40\Pkcs11Interop.StrongName.xml nuget\strongname\lib\net40 || goto :error
copy net45\Pkcs11Interop.StrongName.dll nuget\strongname\lib\net45 || goto :error
copy net45\Pkcs11Interop.StrongName.xml nuget\strongname\lib\net45 || goto :error
copy sl5\Pkcs11Interop.Silverlight.StrongName.dll nuget\strongname\lib\sl5 || goto :error
copy sl5\Pkcs11Interop.Silverlight.StrongName.xml nuget\strongname\lib\sl5 || goto :error
copy netstandard1.3\Pkcs11Interop.DotNetCore.StrongName.dll nuget\strongname\lib\netstandard1.3 || goto :error
copy netstandard1.3\Pkcs11Interop.DotNetCore.StrongName.xml nuget\strongname\lib\netstandard1.3 || goto :error

@rem Copy license to classic package
copy ..\src\Pkcs11Interop\Pkcs11Interop\LICENSE.txt nuget\classic || goto :error
copy ..\src\Pkcs11Interop\Pkcs11Interop\NOTICE.txt nuget\classic || goto :error

@rem Copy license to strongly named package
copy ..\src\Pkcs11Interop\Pkcs11Interop\LICENSE.txt nuget\strongname || goto :error
copy ..\src\Pkcs11Interop\Pkcs11Interop\NOTICE.txt nuget\strongname || goto :error

@rem Create classic package
copy Pkcs11Interop.nuspec nuget\classic || goto :error
nuget pack nuget\classic\Pkcs11Interop.nuspec || goto :error

@rem Create strongly named package
copy Pkcs11Interop.StrongName.nuspec nuget\strongname || goto :error
nuget pack nuget\strongname\Pkcs11Interop.StrongName.nuspec || goto :error

@echo *** CREATE NUGET SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** CREATE NUGET FAILED ***
@endlocal
@exit /b 1
