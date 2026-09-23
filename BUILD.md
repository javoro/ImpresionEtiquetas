# Compilación y release

## Herramientas requeridas

| Herramienta | Uso | Ruta en esta máquina |
|---|---|---|
| MSBuild | Compilar `ImpresionEtiquetas.csproj` en Release | `C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe` |
| Inno Setup 6 (ISCC.exe) | Compilar el instalador `SetupEtiquetas.iss` | `C:\Users\Sis-Javier\AppData\Local\Programs\Inno Setup 6\ISCC.exe` |

Ninguna de las dos está en el `PATH`. Inno Setup se instaló con `winget install JRSoftware.InnoSetup`
(paquete `JRSoftware.InnoSetup`), pero winget lo deja en `AppData\Local\Programs`, no en
`Program Files`, así que una búsqueda rápida en `Program Files` no lo encuentra — usar la ruta
de la tabla o `winget list --id JRSoftware.InnoSetup` para confirmar que sigue instalado.

Si Inno Setup no está instalado: `winget install JRSoftware.InnoSetup` o
https://jrsoftware.org/isdl.php.

## Pasos para una nueva versión

1. Actualizar el número de versión en dos archivos (no hay un único lugar):
   - `ImpresionEtiquetas/Properties/AssemblyInfo.cs` — `AssemblyVersion` / `AssemblyFileVersion`
   - `ImpresionEtiquetas/Setup/SetupEtiquetas.iss` — `AppVersion`
2. Compilar en Release:
   ```
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" `
     "ImpresionEtiquetas\ImpresionEtiquetas.csproj" /p:Configuration=Release /t:Restore,Build
   ```
3. Compilar el instalador (toma los binarios de `ImpresionEtiquetas\bin\Release`):
   ```
   & "C:\Users\Sis-Javier\AppData\Local\Programs\Inno Setup 6\ISCC.exe" `
     "ImpresionEtiquetas\Setup\SetupEtiquetas.iss"
   ```
   Genera `Instalador\SetupEtiquetas_v2.exe`.
4. Commit del bump de versión + el `.exe` recompilado.
5. Tag y release en GitHub (`gh release create`) con `Instalador\SetupEtiquetas_v2.exe` como asset.

## Cuenta de GitHub

Los releases y pushes de este repo van con la cuenta personal `javoro`, no con `javierorona-vdv`
(ambas quedan logueadas en el keyring de `gh`). Si `gh auth status` muestra otra cuenta como activa:

```
gh auth switch --user javoro
```
