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
rmdir /S /Q monoandroid2.3

@rem Clean solution
msbuild ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android.sln ^
	/p:Configuration=Release /p:Platform="Any CPU" /target:Clean || goto :error

@if "%arg1%"=="--with-tests" (
	@rem Build both Pkcs11Interop.Android and Pkcs11Interop.Android.Tests projects via solution
	msbuild ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android.sln ^
		/p:Configuration=Release /p:Platform="Any CPU" /target:Build || goto :error
) else (
	@rem Build only Pkcs11Interop.Android project
	msbuild ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\Pkcs11Interop.Android.csproj ^
		/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error
)

@rem Copy result to output directory
mkdir monoandroid2.3 || goto :error
copy ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\bin\Release\Pkcs11Interop.Android.dll monoandroid2.3 || goto :error
copy ..\src\Pkcs11Interop.Android\Pkcs11Interop.Android\bin\Release\Pkcs11Interop.Android.xml monoandroid2.3 || goto :error

@echo *** BUILD MONOANDROID2.3 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD MONOANDROID2.3 FAILED ***
@endlocal
@exit /b 1
