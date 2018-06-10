@setlocal

set NUGET=c:\nuget\nuget.exe 

@rem Define signing options
set CERTHASH=6962926e92cdd663aa8f42e7211df022f1bb5ffe
set TSAURL=http://time.certum.pl/

%NUGET% sign Pkcs11Interop*.nupkg -CertificateFingerprint %CERTHASH% -Timestamper %TSAURL% || goto :error
%NUGET% verify -Signature Pkcs11Interop*.nupkg

@echo *** SIGN NUGET SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** SIGN NUGET FAILED ***
@endlocal
@exit /b 1
