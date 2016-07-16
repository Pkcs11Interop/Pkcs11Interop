@setlocal

@rem Delete output directory
rmdir /S /Q nuget

@rem Create output directory
mkdir nuget\lib\net20 || goto :error
mkdir nuget\lib\net40 || goto :error
mkdir nuget\lib\net45 || goto :error
mkdir nuget\lib\sl5 || goto :error
mkdir nuget\lib\netstandard1.3 || goto :error
mkdir nuget\lib\monoandroid2.3 || goto :error
mkdir nuget\lib\xamarinios1.0 || goto :error

@rem Copy assemblies
xcopy net20 nuget\lib\net20 || goto :error
xcopy net40 nuget\lib\net40 || goto :error
xcopy net45 nuget\lib\net45 || goto :error
xcopy sl5 nuget\lib\sl5 || goto :error
xcopy netstandard1.3 nuget\lib\netstandard1.3 || goto :error
xcopy monoandroid2.3 nuget\lib\monoandroid2.3 || goto :error
xcopy xamarinios1.0 nuget\lib\xamarinios1.0 || goto :error

@rem Copy license
copy ..\src\Pkcs11Interop\Pkcs11Interop\LICENSE.txt nuget || goto :error
copy ..\src\Pkcs11Interop\Pkcs11Interop\NOTICE.txt nuget || goto :error

@rem Create nuget package
copy Pkcs11Interop.nuspec nuget || goto :error
nuget pack nuget\Pkcs11Interop.nuspec || goto :error

@echo *** CREATE NUGET SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** CREATE NUGET FAILED ***
@endlocal
@exit /b 1
