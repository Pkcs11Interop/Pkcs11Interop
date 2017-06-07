@setlocal

@rem Argument "--with-tests" forces the build of test project
@set arg1=%1

@rem Add signtool.exe location to PATH
@set PATH=%PATH%;C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool\

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
rmdir /S /Q sl5

@rem Clean solution
msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.sln ^
	/p:Configuration=Release /p:Platform="Any CPU" /target:Clean || goto :error

@rem Build Pkcs11Interop.Silverlight project
msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@rem Build Pkcs11Interop.Silverlight.StrongName project
msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.StrongName\Pkcs11Interop.Silverlight.StrongName.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@if "%arg1%"=="--with-tests" (
	@rem Build Pkcs11Interop.Silverlight.Tests project
	msbuild ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.Tests\Pkcs11Interop.Silverlight.Tests.csproj ^
		/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error
)

@rem Copy result to output directory
mkdir sl5 || goto :error
copy ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight\bin\Release\Pkcs11Interop.Silverlight.dll sl5 || goto :error
copy ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight\bin\Release\Pkcs11Interop.Silverlight.xml sl5 || goto :error
copy ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.StrongName\bin\Release\Pkcs11Interop.Silverlight.StrongName.dll sl5 || goto :error
copy ..\src\Pkcs11Interop.Silverlight\Pkcs11Interop.Silverlight.StrongName\bin\Release\Pkcs11Interop.Silverlight.StrongName.xml sl5 || goto :error

@echo *** BUILD SL5 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD SL5 FAILED ***
@endlocal
@exit /b 1
