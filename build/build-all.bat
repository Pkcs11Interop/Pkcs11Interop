@setlocal

call build-net20.bat || goto :error
call build-net40.bat || goto :error
call build-net45.bat || goto :error
call build-sl5.bat || goto :error
call build-netstandard1.3.bat || goto :error
call build-monoandroid2.3.bat || goto :error
call build-xamarinios1.0.bat || goto :error

@echo *** BUILD ALL SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD ALL FAILED ***
@endlocal
@exit /b 1
