@setlocal

@rem Initialize build environment of Visual Studio 2015
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"

@rem Delete output directory
rmdir /S /Q monoandroid2.3

@rem Clean project
msbuild ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\Pkcs11Interop.Android.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Clean || goto :error

@rem Build project
msbuild ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\Pkcs11Interop.Android.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@rem Copy result to output directory
mkdir monoandroid2.3 || goto :error
copy ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\bin\Release\Pkcs11Interop.Android.dll monoandroid2.3 || goto :error
copy ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\bin\Release\Pkcs11Interop.Android.xml monoandroid2.3 || goto :error

@echo *** BUILD MONOANDROID2.3 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD MONOANDROID2.3 FAILED ***
@endlocal
@exit /b 1
