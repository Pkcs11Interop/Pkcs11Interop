@setlocal

@rem Initialize build environment of Visual Studio 2015
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"

@rem Delete output directory
rmdir /S /Q xamarinios

@rem Clean project
msbuild ..\src\Pkcs11Interop.iOS\Pkcs11Interop.iOS\Pkcs11Interop.iOS.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Clean || goto :error

@rem Build project
msbuild ..\src\Pkcs11Interop.iOS\Pkcs11Interop.iOS\Pkcs11Interop.iOS.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@rem Copy result to output directory
mkdir xamarinios || goto :error
copy ..\src\Pkcs11Interop.iOS\Pkcs11Interop.iOS\bin\Release\Pkcs11Interop.iOS.dll xamarinios || goto :error
copy ..\src\Pkcs11Interop.iOS\Pkcs11Interop.iOS\bin\Release\Pkcs11Interop.iOS.xml xamarinios || goto :error

@echo *** BUILD XAMARINIOS SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD XAMARINIOS FAILED ***
@endlocal
@exit /b 1
