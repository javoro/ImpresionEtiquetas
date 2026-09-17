---
name: Sistema de Impresion de Etiquetas
description: Modulo nativo WinForms de alta precision para etiquetado optico y despacho termico Zebra
colors:
  dispatch-emerald: "#15803d"
  dispatch-emerald-hover: "#166534"
  dispatch-emerald-foreground: "#ffffff"
  prep-teal: "#0f766e"
  prep-teal-hover: "#115e59"
  prep-teal-foreground: "#ffffff"
  sapphire-blue: "#2563eb"
  sapphire-blue-hover: "#1d4ed8"
  sapphire-blue-foreground: "#ffffff"
  excel-sky: "#0284c7"
  excel-sky-hover: "#0369a1"
  excel-sky-foreground: "#ffffff"
  danger-crimson: "#dc2626"
  danger-crimson-hover: "#b91c1c"
  danger-crimson-foreground: "#ffffff"
  neutral-bg: "#f8fafc"
  neutral-card: "#ffffff"
  neutral-muted-panel: "#f0fdf4"
  neutral-border: "#e2e8f0"
  neutral-divider: "#cbd5e1"
  text-primary: "#0f172a"
  text-secondary: "#475569"
  text-subtle: "#64748b"
  warning-amber: "#d97706"
typography:
  display:
    fontFamily: "Segoe UI, system-ui, sans-serif"
    fontSize: "11pt"
    fontWeight: 700
    lineHeight: 1.2
    letterSpacing: "normal"
  title:
    fontFamily: "Segoe UI, system-ui, sans-serif"
    fontSize: "9.75pt"
    fontWeight: 700
    lineHeight: 1.3
    letterSpacing: "normal"
  body:
    fontFamily: "Segoe UI, system-ui, sans-serif"
    fontSize: "9.5pt"
    fontWeight: 400
    lineHeight: 1.4
    letterSpacing: "normal"
  label:
    fontFamily: "Segoe UI, system-ui, sans-serif"
    fontSize: "9pt"
    fontWeight: 400
    lineHeight: 1.4
    letterSpacing: "normal"
  caption:
    fontFamily: "Segoe UI, system-ui, sans-serif"
    fontSize: "8.5pt"
    fontWeight: 400
    lineHeight: 1.3
    letterSpacing: "normal"
rounded:
  none: "0px"
  sm: "2px"
  md: "4px"
spacing:
  xs: "4px"
  sm: "8px"
  md: "12px"
  lg: "16px"
  xl: "24px"
components:
  button-dispatch:
    backgroundColor: "{colors.dispatch-emerald}"
    textColor: "{colors.dispatch-emerald-foreground}"
    rounded: "{rounded.sm}"
    padding: "8px 20px"
  button-prep:
    backgroundColor: "{colors.prep-teal}"
    textColor: "{colors.prep-teal-foreground}"
    rounded: "{rounded.sm}"
    padding: "8px 20px"
  button-pdf:
    backgroundColor: "{colors.sapphire-blue}"
    textColor: "{colors.sapphire-blue-foreground}"
    rounded: "{rounded.sm}"
    padding: "8px 18px"
  button-excel:
    backgroundColor: "{colors.excel-sky}"
    textColor: "{colors.excel-sky-foreground}"
    rounded: "{rounded.sm}"
    padding: "8px 18px"
  button-danger:
    backgroundColor: "{colors.danger-crimson}"
    textColor: "{colors.danger-crimson-foreground}"
    rounded: "{rounded.sm}"
    padding: "8px 18px"
  button-neutral:
    backgroundColor: "{colors.neutral-bg}"
    textColor: "{colors.text-primary}"
    rounded: "{rounded.sm}"
    padding: "6px 16px"
  input-text:
    backgroundColor: "{colors.neutral-card}"
    textColor: "{colors.text-primary}"
    rounded: "{rounded.none}"
    padding: "4px 8px"
  grid-cell:
    backgroundColor: "{colors.neutral-card}"
    textColor: "{colors.text-primary}"
    padding: "4px 8px"
---

# Design System: Sistema de Impresion de Etiquetas

## Overview

**Creative North Star: "The Optical Dispatch Desk"**

"The Optical Dispatch Desk" encarna la disciplina, exactitud y velocidad de un mostrador de recepcion y despacho optico industrial de alta tecnologia. En este entorno de alta rotacion de mercancia, el operador de bodega no busca ornamentos decorativos ni micro-animaciones distractoras, sino certeza inmediata: codigos de barras legibles al primer escaneo, clasificacion visual instantanea de armazones (modelo, color, medida), sincronizacion estricta con el catalogo oficial de precios y transmision termica infalible a la impresora Zebra.

La atmosfera visual combina la sobriedad utilitaria de Windows Forms con una jerarquia cromatica basada en propositos operacionales claros. El verde esmeralda industrial senaliza compromiso de hardware y persistencia de datos; el teal profundo rige la preparacion y generacion ZPL; el azul zafiro identifica la inteligencia de extraccion documental de pedidos PDF; y el azul cielo agrupa herramientas de hoja de calculo Excel.

**Key Characteristics:**
- **Claridad de alta densidad:** Diseno optimizado para cuadriculas de datos extensas con escaneo visual rapido de SKU, descripciones y precios.
- **Jerarquia cromatica basada en accion:** Cada color de boton comunica el nivel de impacto de la operacion (despacho termico vs. preparacion ZPL vs. analisis documental vs. utileria).
- **Respeto a las convenciones de escritorio:** Maximo soporte de foco por teclado, atajos de acceso rapido y navegacion fluida por tabulacion.
- **Invarianza de escala:** Estructura modular en paneles apilados para evitar solapamientos visuales bajo cualquier resolucion estandar de terminal operativa.

## Colors

La paleta se divide estrictamente por el rol funcional y nivel de compromiso de cada accion del operador.

### Primary
- **Dispatch Emerald** (#15803d): Tono de accion primaria reservado exclusivamente para confirmar el envio termico fisico a la Zebra (`btnImprimirEtiquetas`) y transferir mercancia analizada a la linea de impresion (`btnTransferir`).
- **Dispatch Emerald Hover** (#166534): Estado activo/foco que confirma al operador la intencion de despacho.

### Secondary
- **Prep Teal** (#0f766e): Tono para generacion y validacion de comandos ZPL (`btnImprimir`) y guardado de configuraciones maestras.
- **Excel Sky** (#0284c7): Herramientas auxiliares de hoja de calculo (`btnImportar`, `btnCargarCatalogo`).
- **Sapphire Blue** (#2563eb): Acento especializado que senaliza la entrada al flujo de extraccion inteligente de pedidos PDF (`btnImportarPdf`, `btnSeleccionarPdf`).

### Tertiary
- **Danger Crimson** (#dc2626): Operaciones destructivas y de descarte (`btnBorrar`).

### Neutral
- **Neutral Background** (#f8fafc): Superficie neutra de descanso visual de la ventana principal y dialogos.
- **Neutral Card** (#ffffff): Fondo blanco puro para celdas editables de la cuadricula `DataGridView`, campos de texto `TextBox` y controles numericos.
- **Muted Panel** (#f0fdf4): Fondo suave de confirmacion para cintillos de resumen y seleccion (`pnlResumen`).
- **Border Control** (#e2e8f0): Limites estructurales tenues y lineas divisorias limpias.
- **Text Primary** (#0f172a): Texto de maximo contraste para informacion de producto y codigos.
- **Text Secondary** (#475569): Rotulos descriptivos y etiquetas de campo.

### Named Rules
**The Dispatch Exclusivity Rule.** El color verde esmeralda (`#15803d`) solo puede asignarse a controles que desencadenan una accion irreversible de hardware (impresion termica ZPL) o un traslado definitivo de lote de articulos. Nunca debe emplearse en botones de cancelacion, navegacion o filtros.

**The Contrast Floor Rule.** Todo texto operativo sobre fondos neutros debe garantizar un ratio de contraste minimo de 4.5:1 para permitir su lectura bajo iluminacion fluorescente o difusa tipica de almacenes.

## Typography

**Display Font:** Segoe UI (fallback: system-ui, sans-serif)
**Body Font:** Segoe UI (fallback: system-ui, Tahoma, sans-serif)
**Label/Numeric Font:** Segoe UI / Consolas

**Character:** Tipografia de caja rigida y legibilidad instantanea en terminales Windows empresariales, priorizando la distincion clara entre ceros, unos, letras 'O' mayuscula y 'I' en codigos SKU y UPC.

### Hierarchy
- **Display** (Bold, 11pt, line-height 1.2): Boton de impresion principal (`btnImprimirEtiquetas`), generacion (`btnImprimir`) e importaciones.
- **Title / GroupBox** (Bold, 9.75pt, line-height 1.3): Titulos de agrupacion de configuracion (`grpEmpresa`, `grpPasos`, `grpConfigLongitud`, `grpModo`).
- **Body / DataGrid** (Regular, 9.5pt, line-height 1.4): Celdas de cuadricula de datos, captura de nombre de empresa (`txtEmpresa`) y selectores numericos.
- **Label** (Regular, 9pt, line-height 1.4): Rotulos de descripcion de campo (`lblConfigMarca`, `lblConfigModelo`, `lblArchivoPdf`).
- **Caption** (Regular, 8.5pt, line-height 1.3): Avisos de estado, leyendas de version y pie de pagina de conectividad.

### Named Rules
**The Monospace Alignment Rule.** Los valores numericos de precio, cantidad y SKU en las columnas del `DataGridView` deben estar alineados consistentemente a la derecha para facilitar la suma visual vertical.

## Layout

El layout implementa un modelo de anclaje bidireccional estricto (Anchor: Top, Bottom, Left, Right) y acoplamiento por paneles (Docking):

1. **Banda Superior de Parametros (Dock Top / Fixed Height):** Contiene la identidad de empresa, el modo de origen (Manual / Excel / PDF) y la calibracion de longitud de campos ZPL.
2. **Cuadricula de Produccion Central (Dock Fill / Dynamic Expansion):** La tabla de etiquetas toma todo el espacio disponible en pantalla, asegurando que el operador visualice la mayor cantidad de filas sin necesidad de scroll excesivo.
3. **Pie de Acciones en Dos Niveles (Stacked Panels):**
   - **Panel de Resumen (`pnlResumen`, altura 38px):** Aloja el checkbox maestro de seleccion y la etiqueta de balance de cantidades calculadas (`lblResumen`), anclada a la izquierda.
   - **Panel de Comandos (`pnlAcciones`, altura 58px):** Aloja los botones de ejecucion (`btnExportarExcel`, `btnTransferir`, `btnCancelar`), anclados a la derecha.

### Named Rules
**The No-Collision Stack Rule.** Ningun rotulo dinamico de conteo de texto puede compartir la misma linea horizontal que un boton de accion con posicion anclada. Toda barra inferior debe desacoplarse en subpaneles apilados verticalmente para garantizar legibilidad en ventanas de cualquier dimension.

## Elevation & Depth

El sistema opera bajo la filosofia **Flat & Crisp Layered**. Al ser una aplicacion de escritorio nativa orientada a productividad:
- No se aplican sombras difusas artificiales en controles individuales.
- La profundidad y jerarquia espacial se logran mediante:
  1. **Tonal Layering:** Fondos diferenciados entre el marco de la ventana (`#f8fafc`), paneles de agrupacion (`#ffffff`), cintillo de confirmacion (`#f0fdf4`) y celdas activas.
  2. **Bordes Nitidos de 1px:** Delimitacion de celdas y tarjetas con bordes geometricos suaves (`#e2e8f0`).
  3. **Elevacion de Dialogos Modales:** Ventanas secundarias (`FrmImportarPdf`) que emergen en el centro visual de la pantalla principal bloqueando la interaccion previa hasta confirmar o cancelar.

### Named Rules
**The Flat-By-Default Rule.** Las superficies de trabajo permanecen totalmente planas en reposo. Los cambios de estado se comunican mediante cambio de color solido de fondo o borde perimetral, nunca por desplazamiento de sombra.

## Shapes

- **Controles Rectangulares con micro-bisel nativo (0px a 2px):** Botones, cajas de texto y grillas conservan el factor de forma rectilinea nativo de Windows, maximizando el area util de pulsacion y lectura.
- **Agrupadores GroupBox:** Bordes perimetrales de 1px con rebaje de texto superior para seccionar visualmente las etapas del flujo (Paso 1: PDF, Paso 2: Catalogo).
- **Checkboxes de Seleccion Directa:** Cuadros de verificacion integrados tanto en cabecera de panel como en la primera columna del `DataGridView` para seleccion masiva o individual.

## Components

### Action Bar & Command Buttons
| Componente | Rol Visual | Color Fondo | Tipografia | Proposito |
|---|---|---|---|---|
| `btnImprimirEtiquetas` | Despacho Primario | `#15803d` (Esmeralda) | 11pt Bold | Envio fisico a impresora Zebra |
| `btnImprimir` | Generacion ZPL | `#0f766e` (Teal) | 11pt Bold | Generar archivo de lote ZPL |
| `btnTransferir` | Transferencia de Datos | `#15803d` (Esmeralda) | 9.75pt Bold | Enviar filas del modal a impresion principal |
| `btnGuardarConfig` | Confirmacion de Ajustes | `#15803d` (Esmeralda) | 9pt Bold | Persistir longitudes maximas ZPL |
| `btnImportar` | Herramienta de Carga | `#0284c7` (Sky Blue) | 11pt Bold | Ejecutar lectura de archivo Excel |
| `btnCargarCatalogo` | Carga de Precios | `#0284c7` (Sky Blue) | 9.75pt Bold | Cargar catalogo maestro de precios |
| `btnExportarExcel` | Exportacion | `#0f766e` (Teal) | 9.75pt Bold | Generar archivo Excel de la tabla |
| `btnImportarPdf` | Integracion PDF | `#2563eb` (Azul Zafiro) | 11pt Bold | Abrir modal de desfragmentacion de PDF |
| `btnBorrar` | Operacion de Descarte | `#dc2626` (Rojo Crimson) | 11pt Bold | Limpiar cuadrícula activa |
| `btnCancelar` | Neutro / Descarte | `#f1f5f9` (Gris Slate) | 9.75pt Regular | Resetear captura o cancelar modal |

### Data Grid View (`dgvDatos` / `dgvPdf`)
- **Cabecera de Columnas:** Altura de 36px, fondo `#f1f5f9`, texto centrado/izquierdo en negrita `#1e293b`.
- **Celdas de Datos:** Altura de fila de 28px, relleno interno de 4px, texto `#0f172a` sobre fondo blanco.
- **Fila Alternada:** Fondo `#f8fafc` para contraste descansado.
- **Fila Seleccionada:** Fondo azul solido `#2563eb`, texto blanco invertido.
- **Fila Editada / Incompleta:** Advertencia visual en celda si el precio no se encontro en el catalogo.

### Subpanel de Resumen
- Contenedor de 38px de alto con fondo `#f0fdf4`.
- Contiene el checkbox `chkSeleccionarTodos` alineado a la izquierda con separacion de 12px respecto al contador `lblResumen` en texto verde `#166534`.

## Do's and Don'ts

- **Do** asignar atajos de teclado mnemotecnicos (`&Imprimir`, `&Importar`, `&Cancelar`) a todos los botones principales.
- **Do** formatear todos los precios con el signo de moneda y dos decimales (`$#,##0.00`).
- **Do** deshabilitar visualmente el boton `btnTransferir` hasta que se hayan importado datos validos desde el PDF.
- **Do** apilar los paneles de resumen y accion en secciones horizontales independientes para evitar cualquier encimado de elementos.
- **Don't** alterar el orden ni los nombres tecnicos de las columnas esperadas por la plantilla ZPL (Empresa, Modelo, Color, Medida, Precio, Codigo).
- **Don't** aplicar colores de fondo saturados en las celdas del `DataGridView` que cansen la vista durante jornadas prolongadas de captura.
- **Don't** permitir que el operador imprima si la columna de Modelo o Codigo de Barras excede los limites fisicos calibrados en la configuracion.
- **Don't** ocultar errores de conversion de catalogos; advertir explicitamente las filas que carecen de precio antes de permitir la transferencia.
