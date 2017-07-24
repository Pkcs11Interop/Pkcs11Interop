@setlocal

@rem Add COVERITY tools to PATH
@set PATH=%PATH%;d:\temp\coverity\cov-analysis-win64-8.7.0\bin\

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

@rem Clean solution
msbuild ..\src\Pkcs11Interop\Pkcs11Interop.sln ^
	/p:Configuration=Release /p:Platform="Any CPU" /p:TargetFrameworkVersion=v4.0 ^
	/target:Clean || goto :error

@rem Build Pkcs11Interop project
cov-build --dir cov-int msbuild ..\src\Pkcs11Interop\Pkcs11Interop\Pkcs11Interop.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /p:TargetFrameworkVersion=v4.0 ^
	/p:DefineConstants=COVERITY /target:Build || goto :error

@echo *** BUILD FOR COVERITY SCAN SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD FOR COVERITY SCAN FAILED ***
@endlocal
@exit /b 1
