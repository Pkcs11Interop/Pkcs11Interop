@setlocal

@rem Initialize build environment of Visual Studio 2015
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"

@rem Delete output directory
rmdir /S /Q Xamarin.iOS10

@rem Clean project
msbuild ..\src\Pkcs11Interop.iOS\Pkcs11Interop.iOS\Pkcs11Interop.iOS.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Clean || goto :error

@rem Build project
msbuild ..\src\Pkcs11Interop.iOS\Pkcs11Interop.iOS\Pkcs11Interop.iOS.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@rem Copy result to output directory
mkdir Xamarin.iOS10 || goto :error
copy ..\src\Pkcs11Interop.iOS\Pkcs11Interop.iOS\bin\Release\Pkcs11Interop.iOS.dll Xamarin.iOS10 || goto :error
copy ..\src\Pkcs11Interop.iOS\Pkcs11Interop.iOS\bin\Release\Pkcs11Interop.iOS.xml Xamarin.iOS10 || goto :error

@echo *** BUILD XAMARIN.IOS10 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD XAMARIN.IOS10 FAILED ***
@endlocal
@exit /b 1
