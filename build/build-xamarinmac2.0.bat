@setlocal

@rem Argument "--with-tests" forces the build of test project
@set arg1=%1

@rem Argument "--skip-cleaning" skips solution cleaning
@set arg2=%2

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
rmdir /S /Q xamarinmac2.0

@if not "%arg2%"=="--skip-cleaning" (
	@rem Clean solution
	msbuild ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard.sln ^
		/p:Configuration=Release /p:Platform="Any CPU" /target:Clean || goto :error
)

@rem Restore dependencies for the solution
msbuild ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard.sln ^
	/p:Configuration=Release /p:Platform="Any CPU" /target:Restore || goto :error

@rem Build Pkcs11Interop project
msbuild ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop\Pkcs11Interop.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /p:TargetFramework=xamarinmac2.0 ^
	/target:Build || goto :error

@if "%arg1%"=="--with-tests" (
	@rem There is no test project available for Xamarin.Mac
)

@rem Copy result to output directory
mkdir xamarinmac2.0 || goto :error
copy ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop\bin\Release\xamarinmac2.0\Pkcs11Interop.dll xamarinmac2.0 || goto :error
copy ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop\bin\Release\xamarinmac2.0\Pkcs11Interop.xml xamarinmac2.0 || goto :error

@echo *** BUILD XAMARINMAC2.0 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD XAMARINMAC2.0 FAILED ***
@endlocal
@exit /b 1
