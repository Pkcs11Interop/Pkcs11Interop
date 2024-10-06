@setlocal

@rem Define paths to necessary directories
set workingdir=%~dp0
set outputdir=%workingdir%nuget-unsigned

@rem Initialize build environment of Visual Studio 2022 Community/Professional/Enterprise
@set tools=
@set tmptools="c:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@set tmptools="c:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@set tmptools="c:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@if not defined tools goto :error
call %tools%
@echo on

@rem Clean output directory
rmdir /S /Q %outputdir%
mkdir %outputdir% || goto :error

@rem Remove leftovers of any previous builds
rmdir /S /Q ..\src\Pkcs11Interop\bin
rmdir /S /Q ..\src\Pkcs11Interop\obj
rmdir /S /Q ..\src\Pkcs11Interop.Mock\bin
rmdir /S /Q ..\src\Pkcs11Interop.Mock\obj
rmdir /S /Q ..\src\Pkcs11Interop.Tests\bin
rmdir /S /Q ..\src\Pkcs11Interop.Tests\obj

@rem Restore dependencies
msbuild ..\src\Pkcs11Interop.sln /p:Configuration=Release /p:Platform="Any CPU" /target:Restore || goto :error

@rem Clean solution
msbuild ..\src\Pkcs11Interop.sln /p:Configuration=Release /p:Platform="Any CPU" /target:Clean || goto :error

@rem Build solution
msbuild ..\src\Pkcs11Interop.sln /p:Configuration=Release /p:Platform="Any CPU" /target:Build || goto :error

@rem Copy packages to output directory
copy ..\src\Pkcs11Interop\bin\Release\*.nupkg %outputdir% || goto :error
copy ..\src\Pkcs11Interop\bin\Release\*.snupkg %outputdir% || goto :error

@echo *** BUILD SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD FAILED ***
@endlocal
@exit /b 1
