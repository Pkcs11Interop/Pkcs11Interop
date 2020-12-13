@setlocal

@rem Argument "--with-tests" forces the build of test project
@set arg1=%1

@rem Initialize build environment of Visual Studio 2019 Community/Professional/Enterprise
@set tools=
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@if not defined tools goto :error
call %tools%
@echo on

@rem Delete output directory
rmdir /S /Q net40

@rem Clean solution
msbuild ..\src\Pkcs11Interop\Pkcs11Interop.sln ^
	/p:Configuration=Release /p:Platform="Any CPU" /p:TargetFrameworkVersion=v4.0 ^
	/target:Clean || goto :error

@rem Build Pkcs11Interop project
msbuild ..\src\Pkcs11Interop\Pkcs11Interop\Pkcs11Interop.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /p:TargetFrameworkVersion=v4.0 ^
	/target:Build || goto :error

@if "%arg1%"=="--with-tests" (
	@rem Build Pkcs11InteropTests project
	msbuild ..\src\Pkcs11Interop\Pkcs11InteropTests\Pkcs11InteropTests.csproj ^
		/p:Configuration=Release /p:Platform=AnyCPU /p:TargetFrameworkVersion=v4.0 ^
		/target:Build || goto :error
)

@rem Copy result to output directory
mkdir net40 || goto :error
copy ..\src\Pkcs11Interop\Pkcs11Interop\bin\Release\Pkcs11Interop.dll net40 || goto :error
copy ..\src\Pkcs11Interop\Pkcs11Interop\bin\Release\Pkcs11Interop.xml net40 || goto :error

@echo *** BUILD NET40 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD NET40 FAILED ***
@endlocal
@exit /b 1
