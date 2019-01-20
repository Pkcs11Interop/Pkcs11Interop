@setlocal

@rem Argument "--with-tests" forces the build of test project
@set arg1=%1

@rem Initialize Visual Studio build environment:
@rem - Visual Studio 2017 Community/Professional/Enterprise is the preferred option
@rem - Visual Studio 2015 is the fallback option (which might or might not work)
@set tools=
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"
@if exist %tmptools% set tools=%tmptools%
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
rmdir /S /Q net20

@rem Clean solution
msbuild ..\src\Pkcs11Interop.Legacy\Pkcs11Interop.sln ^
	/p:Configuration=Release /p:Platform="Any CPU" /p:TargetFrameworkVersion=v2.0 ^
	/target:Clean || goto :error

@rem Build Pkcs11Interop project
msbuild ..\src\Pkcs11Interop.Legacy\Pkcs11Interop\Pkcs11Interop.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /p:TargetFrameworkVersion=v2.0 ^
	/target:Build || goto :error

@if "%arg1%"=="--with-tests" (
	@rem Build Pkcs11InteropTests project
	msbuild ..\src\Pkcs11Interop.Legacy\Pkcs11InteropTests\Pkcs11InteropTests.csproj ^
		/p:Configuration=Release /p:Platform=AnyCPU /p:TargetFrameworkVersion=v2.0 ^
		/target:Build || goto :error
)

@rem Copy result to output directory
mkdir net20 || goto :error
copy ..\src\Pkcs11Interop.Legacy\Pkcs11Interop\bin\Release\Pkcs11Interop.dll net20 || goto :error
copy ..\src\Pkcs11Interop.Legacy\Pkcs11Interop\bin\Release\Pkcs11Interop.xml net20 || goto :error

@echo *** BUILD NET20 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD NET20 FAILED ***
@endlocal
@exit /b 1
