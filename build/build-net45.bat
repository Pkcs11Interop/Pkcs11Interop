@setlocal

@rem Initialize build environment of Visual Studio 2015
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"

@rem Delete output directory
rmdir /S /Q net45

@rem Clean project
msbuild ..\src\Pkcs11Interop\Pkcs11Interop\Pkcs11Interop.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /p:TargetFrameworkVersion=v4.5 ^
/target:Clean || goto :error

@rem Build project
msbuild ..\src\Pkcs11Interop\Pkcs11Interop\Pkcs11Interop.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /p:TargetFrameworkVersion=v4.5 ^
/target:Build || goto :error

@rem Copy result to output directory
mkdir net45 || goto :error
copy ..\src\Pkcs11Interop\Pkcs11Interop\bin\Release\Pkcs11Interop.dll net45 || goto :error
copy ..\src\Pkcs11Interop\Pkcs11Interop\bin\Release\Pkcs11Interop.xml net45 || goto :error

@echo *** BUILD NET45 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD NET45 FAILED ***
@endlocal
@exit /b 1
