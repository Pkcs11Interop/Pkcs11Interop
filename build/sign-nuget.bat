@setlocal

set NUGET=c:\nuget\nuget.exe 

@rem Define signing options
set CERTHASH=ef1bfeaa474bb078923831bf7732186673a5b5c9
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
