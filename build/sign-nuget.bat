@setlocal

set NUGET=c:\nuget\nuget.exe 

@rem Define signing options
set CERTHASH=9ccadd88b898155baef4c7b8ff7b17595275b1bb
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
