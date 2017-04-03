@setlocal

@rem Delete output directory
rmdir /S /Q netstandard1.3

@rem Clean project
rmdir /S /Q ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\bin
rmdir /S /Q ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\obj
del ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\project.lock.json
rmdir /S /Q ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\bin
rmdir /S /Q ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\obj
del ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\project.lock.json

@rem Build project
copy /Y ..\src\Pkcs11Interop.DotNetCore\global.json .
dotnet restore ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\ || goto :error
dotnet build ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\ --configuration Release || goto :error
dotnet restore ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\ || goto :error
dotnet build ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\ --configuration Release || goto :error

@rem Copy result to output directory
mkdir netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\bin\Release\netstandard1.3\Pkcs11Interop.DotNetCore.dll netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\bin\Release\netstandard1.3\Pkcs11Interop.DotNetCore.xml netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\bin\Release\netstandard1.3\Pkcs11Interop.DotNetCore.StrongName.dll netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\bin\Release\netstandard1.3\Pkcs11Interop.DotNetCore.StrongName.xml netstandard1.3 || goto :error

@echo *** BUILD NETSTANDARD1.3 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD NETSTANDARD1.3 FAILED ***
@endlocal
@exit /b 1
