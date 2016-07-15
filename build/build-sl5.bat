@setlocal

@rem Initialize build environment of Visual Studio 2015
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"

@rem Delete output directory
rmdir /S /Q sl5

@rem Clean project
msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Clean || goto :error

@rem Build project
msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@rem Copy result to output directory
mkdir sl5 || goto :error
copy ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight\bin\Release\Pkcs11Interop.Silverlight.dll sl5 || goto :error
copy ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight\bin\Release\Pkcs11Interop.Silverlight.xml sl5 || goto :error

@echo *** BUILD SL5 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD SL5 FAILED ***
@endlocal
@exit /b 1
