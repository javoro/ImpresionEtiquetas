; Script de Inno Setup para Impresion de Etiquetas v2.0
; Descarga Inno Setup desde: https://jrsoftware.org/isdl.php

[Setup]
AppName=Impresion de Etiquetas
AppVersion=2.0.0.0
AppPublisher=Visualiza+
DefaultDirName={autopf}\Impresion de Etiquetas
DefaultGroupName=Impresion de Etiquetas
OutputDir=..\Instalador
OutputBaseFilename=SetupEtiquetas_v2
SetupIconFile=..\ImpresionEtiquetas\print.ico
Compression=lzma
SolidCompression=yes
UninstallDisplayIcon={app}\ImpresionEtiquetas.exe
PrivilegesRequired=admin
WizardStyle=modern

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "Crear acceso directo en el escritorio"; GroupDescription: "Iconos adicionales:"

[Files]
; Ejecutable principal
Source: "..\ImpresionEtiquetas\bin\Release\ImpresionEtiquetas.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\ImpresionEtiquetas.exe.config"; DestDir: "{app}"; Flags: ignoreversion

; Plantillas y BAT (archivos de solo lectura en la carpeta de instalacion)
Source: "..\ImpresionEtiquetas\bin\Release\Plantilla.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\Plantilla.xlsx"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\PrintEtiqueta.bat"; DestDir: "{app}"; Flags: ignoreversion

; Dependencias (DLLs)
Source: "..\ImpresionEtiquetas\bin\Release\EPPlus.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\EPPlus.Interfaces.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\EPPlus.System.Drawing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\Microsoft.IO.RecyclableMemoryStream.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\System.ComponentModel.Annotations.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\System.Drawing.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\ImpresionEtiquetas\bin\Release\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\Impresion de Etiquetas"; Filename: "{app}\ImpresionEtiquetas.exe"
Name: "{group}\Desinstalar Impresion de Etiquetas"; Filename: "{uninstallexe}"
Name: "{autodesktop}\Impresion de Etiquetas"; Filename: "{app}\ImpresionEtiquetas.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\ImpresionEtiquetas.exe"; Description: "Ejecutar Impresion de Etiquetas"; Flags: nowait postinstall skipifsilent
