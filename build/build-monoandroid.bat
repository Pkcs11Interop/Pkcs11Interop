@setlocal

@rem Initialize build environment of Visual Studio 2015
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"

@rem Delete output directory
rmdir /S /Q monoandroid

@rem Clean project
msbuild ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\Pkcs11Interop.Android.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Clean || goto :error

@rem Build project
msbuild ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\Pkcs11Interop.Android.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@rem Copy result to output directory
mkdir monoandroid || goto :error
copy ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\bin\Release\Pkcs11Interop.Android.dll monoandroid || goto :error
copy ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\bin\Release\Pkcs11Interop.Android.xml monoandroid || goto :error

@echo *** BUILD MONOANDROID SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD MONOANDROID FAILED ***
@endlocal
@exit /b 1
