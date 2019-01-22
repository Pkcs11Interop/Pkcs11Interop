@setlocal

set NUGET=c:\nuget\nuget.exe 

@rem Delete output directory
rmdir /S /Q nuget

@rem Create output directories
mkdir nuget\lib\net20 || goto :error
mkdir nuget\lib\net40 || goto :error
mkdir nuget\lib\net45 || goto :error
mkdir nuget\lib\sl5 || goto :error
mkdir nuget\lib\netstandard1.3 || goto :error
mkdir nuget\lib\monoandroid2.3 || goto :error
mkdir nuget\lib\xamarinios1.0 || goto :error

@rem Copy assemblies to output directories
copy net20\Pkcs11Interop.dll nuget\lib\net20 || goto :error
copy net20\Pkcs11Interop.xml nuget\lib\net20 || goto :error
copy net40\Pkcs11Interop.dll nuget\lib\net40 || goto :error
copy net40\Pkcs11Interop.xml nuget\lib\net40 || goto :error
copy net45\Pkcs11Interop.dll nuget\lib\net45 || goto :error
copy net45\Pkcs11Interop.xml nuget\lib\net45 || goto :error
copy sl5\Pkcs11Interop.dll nuget\lib\sl5 || goto :error
copy sl5\Pkcs11Interop.xml nuget\lib\sl5 || goto :error
copy netstandard1.3\Pkcs11Interop.dll nuget\lib\netstandard1.3 || goto :error
copy netstandard1.3\Pkcs11Interop.xml nuget\lib\netstandard1.3 || goto :error
copy monoandroid2.3\Pkcs11Interop.dll nuget\lib\monoandroid2.3 || goto :error
copy monoandroid2.3\Pkcs11Interop.xml nuget\lib\monoandroid2.3 || goto :error
copy xamarinios1.0\Pkcs11Interop.dll nuget\lib\xamarinios1.0 || goto :error
copy xamarinios1.0\Pkcs11Interop.xml nuget\lib\xamarinios1.0 || goto :error

@rem Copy license to output directory
copy ..\src\Pkcs11Interop\Pkcs11Interop\LICENSE.txt nuget || goto :error
copy ..\src\Pkcs11Interop\Pkcs11Interop\NOTICE.txt nuget || goto :error

@rem Create package
copy Pkcs11Interop.nuspec nuget || goto :error
%NUGET% pack nuget\Pkcs11Interop.nuspec || goto :error

@echo *** CREATE NUGET SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** CREATE NUGET FAILED ***
@endlocal
@exit /b 1
