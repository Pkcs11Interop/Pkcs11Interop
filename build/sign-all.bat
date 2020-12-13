@setlocal

set SIGNTOOL="C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool\signtool.exe"

@rem Define signing options
set CERTHASH=9ccadd88b898155baef4c7b8ff7b17595275b1bb
set TSAURL=http://time.certum.pl/
set LIBNAME=Pkcs11Interop
set LIBURL=https://www.pkcs11interop.net/

@rem Sign all assemblies using SHA1withRSA algorithm
%SIGNTOOL% sign /sha1 %CERTHASH% /fd sha1 /tr %TSAURL% /td sha1 /d %LIBNAME% /du %LIBURL% ^
net20\Pkcs11Interop.dll ^
net40\Pkcs11Interop.dll ^
net45\Pkcs11Interop.dll ^
sl5\Pkcs11Interop.dll ^
netstandard1.3\Pkcs11Interop.dll ^
netstandard2.0\Pkcs11Interop.dll ^
monoandroid2.3\Pkcs11Interop.dll ^
xamarinios1.0\Pkcs11Interop.dll ^
xamarinmac2.0\Pkcs11Interop.dll || goto :error

@rem Sign all assemblies using SHA256withRSA algorithm
%SIGNTOOL% sign /sha1 %CERTHASH% /as /fd sha256 /tr %TSAURL% /td sha256 /d %LIBNAME% /du %LIBURL% ^
net20\Pkcs11Interop.dll ^
net40\Pkcs11Interop.dll ^
net45\Pkcs11Interop.dll ^
sl5\Pkcs11Interop.dll ^
netstandard1.3\Pkcs11Interop.dll ^
netstandard2.0\Pkcs11Interop.dll ^
monoandroid2.3\Pkcs11Interop.dll ^
xamarinios1.0\Pkcs11Interop.dll ^
xamarinmac2.0\Pkcs11Interop.dll || goto :error

@echo *** SIGN ALL SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** SIGN ALL FAILED ***
@endlocal
@exit /b 1
