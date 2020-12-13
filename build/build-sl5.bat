@setlocal

@rem Argument "--with-tests" forces the build of test project
@set arg1=%1

@rem Add signtool.exe location to PATH
@set PATH=%PATH%;C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool\

@rem Initialize build environment of Visual Studio 2017 or 2019 Community/Professional/Enterprise
@set tools=
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
@set tmptools="c:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsMSBuildCmd.bat"
@if exist %tmptools% set tools=%tmptools%
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
rmdir /S /Q sl5

@rem Clean solution
msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.sln ^
	/p:Configuration=Release /p:Platform="Any CPU" /target:Clean || goto :error

@rem Build Pkcs11Interop project
msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop\Pkcs11Interop.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@if "%arg1%"=="--with-tests" (
	@rem Build Pkcs11Interop.Silverlight.Tests project
	msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.Tests\Pkcs11Interop.Silverlight.Tests.csproj ^
		/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error
)

@rem Copy result to output directory
mkdir sl5 || goto :error
copy ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop\bin\Release\Pkcs11Interop.dll sl5 || goto :error
copy ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop\bin\Release\Pkcs11Interop.xml sl5 || goto :error

@echo *** BUILD SL5 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD SL5 FAILED ***
@endlocal
@exit /b 1
