@echo off
echo ============================================================
echo   Generador de Instalador - Impresion de Etiquetas
echo ============================================================
echo.

REM Buscar MSBuild
set MSBUILD=
for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe 2^>nul`) do set MSBUILD=%%i

if "%MSBUILD%"=="" (
    echo [ERROR] No se encontro MSBuild. Asegurate de tener Visual Studio instalado.
    pause
    exit /b 1
)

echo [1/2] Compilando proyecto en modo Release...
"%MSBUILD%" "..\ImpresionEtiquetas\ImpresionEtiquetas.csproj" /p:Configuration=Release /t:Build /v:minimal
if %ERRORLEVEL% neq 0 (
    echo [ERROR] La compilacion fallo.
    pause
    exit /b 1
)
echo [OK] Compilacion exitosa.
echo.

REM Buscar Inno Setup
set ISCC=
if exist "%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe" set ISCC=%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe
if "%ISCC%"=="" if exist "%ProgramFiles%\Inno Setup 6\ISCC.exe" set ISCC=%ProgramFiles%\Inno Setup 6\ISCC.exe

if "%ISCC%"=="" (
    echo [AVISO] No se encontro Inno Setup 6.
    echo Descargalo desde: https://jrsoftware.org/isdl.php
    echo Luego abre Setup.iss manualmente con Inno Setup Compiler.
    pause
    exit /b 1
)

echo [2/2] Generando instalador...
"%ISCC%" "Setup.iss"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Fallo la generacion del instalador.
    pause
    exit /b 1
)

echo.
echo ============================================================
echo   Instalador generado en: Installer\Output\
echo ============================================================
pause
