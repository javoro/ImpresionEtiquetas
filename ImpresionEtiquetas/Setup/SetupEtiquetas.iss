; Script de Inno Setup para Impresion de Etiquetas v2.0
; Descarga Inno Setup desde: https://jrsoftware.org/isdl.php

[Setup]
AppId={{9F6F42FA-28A4-47DE-BD67-E5E2EC64C1F4}
AppName=Impresion de Etiquetas
AppVersion=2.3.0.0
AppPublisher=Javier Orona
AppCopyright=Copyright © 2025 Javier Orona
DefaultDirName={autopf}\Impresion de Etiquetas
DefaultGroupName=Impresion de Etiquetas
OutputDir=..\..\Instalador
OutputBaseFilename=SetupEtiquetas_v2
SetupIconFile=..\print.ico
Compression=lzma
SolidCompression=yes
UninstallDisplayIcon={app}\ImpresionEtiquetas.exe
PrivilegesRequired=admin
WizardStyle=modern
; Permitir actualización sobre instalación existente
UsePreviousAppDir=yes
CloseApplications=yes
RestartApplications=yes

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "Crear acceso directo en el escritorio"; GroupDescription: "Iconos adicionales:"

[Files]
; Ejecutable principal
Source: "..\bin\Release\ImpresionEtiquetas.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\ImpresionEtiquetas.exe.config"; DestDir: "{app}"; Flags: ignoreversion

; Plantillas y BAT (archivos de solo lectura en la carpeta de instalacion)
Source: "..\bin\Release\Plantilla.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Plantilla.xlsx"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\PrintEtiqueta.bat"; DestDir: "{app}"; Flags: ignoreversion

; Dependencias (DLLs)
Source: "..\bin\Release\EPPlus.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\EPPlus.Interfaces.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\EPPlus.System.Drawing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Microsoft.IO.RecyclableMemoryStream.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\System.ComponentModel.Annotations.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\System.Drawing.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Microsoft.Bcl.HashCode.dll"; DestDir: "{app}"; Flags: ignoreversion

; Lectura de PDF (PdfPig)
Source: "..\bin\Release\UglyToad.PdfPig.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\UglyToad.PdfPig.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\UglyToad.PdfPig.Fonts.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\UglyToad.PdfPig.Tokenization.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\UglyToad.PdfPig.Tokens.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\Impresion de Etiquetas"; Filename: "{app}\ImpresionEtiquetas.exe"
Name: "{group}\Desinstalar Impresion de Etiquetas"; Filename: "{uninstallexe}"
Name: "{autodesktop}\Impresion de Etiquetas"; Filename: "{app}\ImpresionEtiquetas.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\ImpresionEtiquetas.exe"; Description: "Ejecutar Impresion de Etiquetas"; Flags: nowait postinstall skipifsilent
