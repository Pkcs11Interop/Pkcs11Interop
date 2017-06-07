@setlocal

@rem Argument "--with-tests" forces the build of test project
@set arg1=%1

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

@rem Clean solution
msbuild ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard.sln ^
	/p:Configuration=Release /p:Platform="Any CPU" /p:TargetFrameworkVersion=v2.0 ^
	/target:Clean || goto :error

@rem Build Pkcs11Interop.NetStandard project
msbuild ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@rem Build Pkcs11Interop.NetStandard.StrongName project
msbuild ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard.StrongName\Pkcs11Interop.NetStandard.StrongName.csproj ^
	/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error

@if "%arg1%"=="--with-tests" (
	@rem Build Pkcs11Interop.DotNetCore.Tests project
	msbuild ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.DotNetCore.Tests\Pkcs11Interop.DotNetCore.Tests.csproj ^
		/p:Configuration=Release /p:Platform=AnyCPU /target:Build || goto :error
)

@rem Copy result to output directory
mkdir netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard\bin\Release\Pkcs11Interop.NetStandard.dll netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard\bin\Release\Pkcs11Interop.NetStandard.xml netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard.StrongName\bin\Release\Pkcs11Interop.NetStandard.StrongName.dll netstandard1.3 || goto :error
copy ..\src\Pkcs11Interop.NetStandard\Pkcs11Interop.NetStandard.StrongName\bin\Release\Pkcs11Interop.NetStandard.StrongName.xml netstandard1.3 || goto :error

@echo *** BUILD NETSTANDARD1.3 SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD NETSTANDARD1.3 FAILED ***
@endlocal
@exit /b 1
