; ============================================================
; Inno Setup Script - Impresión de Etiquetas
; ============================================================
; Para generar el instalador:
; 1. Descarga e instala Inno Setup desde https://jrsoftware.org/isdl.php
; 2. Abre este archivo (Setup.iss) con Inno Setup Compiler
; 3. Presiona Ctrl+F9 o clic en "Compile"
; 4. El instalador se generará en la carpeta Installer\Output\
; ============================================================

#define MyAppName "Impresion de Etiquetas"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Javier Orona"
#define MyAppExeName "ImpresionEtiquetas.exe"
#define BuildDir "..\ImpresionEtiquetas\bin\Release"
#define ProjectDir "..\ImpresionEtiquetas"

[Setup]
AppId={{A7F3B2C1-9D4E-4F5A-8B6C-1E2D3F4A5B6C}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=Output
OutputBaseFilename=ImpresionEtiquetas_Setup_{#MyAppVersion}
SetupIconFile={#ProjectDir}\print.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesAllowed=x86 x64
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "Crear acceso directo en el &Escritorio"; GroupDescription: "Accesos directos:"

[Files]
; Ejecutable principal y configuración
Source: "{#BuildDir}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\{#MyAppExeName}.config"; DestDir: "{app}"; Flags: ignoreversion

; Bibliotecas de dependencias
Source: "{#BuildDir}\EPPlus.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\EPPlus.Interfaces.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\EPPlus.System.Drawing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\Microsoft.IO.RecyclableMemoryStream.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\System.ComponentModel.Annotations.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\System.Drawing.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion

; Archivos de operación (plantilla ZPL, script de impresión, layout Excel)
Source: "{#BuildDir}\Plantilla.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\PrintEtiqueta.bat"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}\Plantilla.xlsx"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Desinstalar {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Ejecutar {#MyAppName}"; Flags: nowait postinstall skipifsilent
