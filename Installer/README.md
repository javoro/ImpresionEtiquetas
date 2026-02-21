# Instalador - Impresión de Etiquetas

## Requisitos previos

- **Inno Setup 6** — Descárgalo gratis desde: https://jrsoftware.org/isdl.php

## Generar el instalador

### Opción 1: Automático (recomendado)
1. Ejecuta `Build.bat` (doble clic)
2. El script compila el proyecto en Release y genera el instalador
3. El archivo `.exe` del instalador se crea en `Installer\Output\`

### Opción 2: Manual
1. Compila el proyecto en modo **Release** desde Visual Studio
2. Abre `Setup.iss` con **Inno Setup Compiler**
3. Presiona `Ctrl+F9` para compilar
4. El instalador se genera en `Installer\Output\`

## ¿Qué incluye el instalador?

| Archivo | Descripción |
|---|---|
| `ImpresionEtiquetas.exe` | Aplicación principal |
| `*.dll` | Bibliotecas de dependencias (EPPlus, etc.) |
| `Plantilla.txt` | Plantilla ZPL para la etiqueta |
| `PrintEtiqueta.bat` | Script de impresión a la etiquetadora |
| `Plantilla.xlsx` | Layout Excel para carga de datos |

## ¿Qué hace el instalador?

- Instala la aplicación en `Archivos de Programa\Impresion de Etiquetas\`
- Crea acceso directo en el Escritorio (opcional)
- Crea entrada en el menú Inicio
- Incluye desinstalador
