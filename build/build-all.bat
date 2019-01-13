@setlocal

@rem Argument "--with-tests" forces the build of test projects
@set arg1=%1

call build-net20.bat %arg1% || goto :error
call build-net40.bat %arg1% || goto :error
call build-net45.bat %arg1% || goto :error
call build-netstandard1.3.bat %arg1% || goto :error
call build-netstandard2.0.bat %arg1% || goto :error
call build-monoandroid2.3.bat %arg1% || goto :error
call build-xamarinios1.0.bat %arg1% || goto :error
call build-xamarinmac2.0.bat %arg1% || goto :error

@echo *** BUILD ALL SUCCESSFUL ***
@endlocal
@exit /b 0

:error
@echo *** BUILD ALL FAILED ***
@endlocal
@exit /b 1
