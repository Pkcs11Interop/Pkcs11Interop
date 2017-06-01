@setlocal

@rem Initialize build environment of Visual Studio 2017 Community/Professional/Enterprise
@set tools=
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@if not defined tools goto :error
call %tools%
@echo on

@rem Delete output directory
rmdir /S /Q netstandard1.3

@rem Clean project
msbuild ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\Pkcs11Interop.DotNetCore.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Clean || goto :error
msbuild ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\Pkcs11Interop.DotNetCore.StrongName.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Clean || goto :error

@rem Restore dependencies
msbuild ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\Pkcs11Interop.DotNetCore.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Restore || goto :error
msbuild ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\Pkcs11Interop.DotNetCore.StrongName.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Restore || goto :error

@rem Build project
msbuild ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\Pkcs11Interop.DotNetCore.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error
msbuild ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\Pkcs11Interop.DotNetCore.StrongName.csproj ^
/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@rem AppVeyor troubleshooting
@rem tree /a /f ..

@rem Copy result to output directory
mkdir netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\bin\Release\Pkcs11Interop.DotNetCore.dll netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore\bin\Release\Pkcs11Interop.DotNetCore.xml netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\bin\Release\Pkcs11Interop.DotNetCore.StrongName.dll netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.DotNetCore\src\Pkcs11Interop.DotNetCore.StrongName\bin\Release\Pkcs11Interop.DotNetCore.StrongName.xml netstandard1.3 || goto :error

@echo *** BUILD NETSTANDARD1.3 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD NETSTANDARD1.3 FAILED ***
@endlocal
@exit /b 1
