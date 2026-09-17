# Product

<!-- impeccable:product-schema 1 -->

## Platform

web

## Users

Operadores de bodega, logística y ópticas que requieren imprimir etiquetas físicas de código de barras para armazones oftálmicos y solares, procesando órdenes de compra en PDF, catálogos en Excel o captura manual directa.

## Product Purpose

Optimizar el proceso de etiquetado de armazones mediante una aplicación nativa Windows Forms (.NET) de alto rendimiento, capaz de interpretar pedidos en PDF, cruzar precios de venta con listas de precios en Excel y despachar comandos ZPL optimizados a impresoras térmicas Zebra.

## Positioning

Módulo nativo de impresión térmica Zebra sin dependencias de servicios web, especializado en óptica y joyería, con capacidad de extracción tabular de facturas PDF, cruce de catálogos y edición ágil en cuadrícula antes del despacho térmico.

## Operating Context

- **Entorno:** Aplicación de escritorio nativa C# WinForms (.NET Framework 4.8) en estaciones de trabajo Windows.
- **Dispositivos:** Impresoras térmicas Zebra (TLP 2844, GK420t, ZD Series) vía USB o puerto de red.
- **Formatos:** Archivos PDF de pedidos de proveedores (ej. T2026875), catálogos Excel (.xlsx) y plantillas ZPL físicas de dos etiquetas por paso (mariposa/joyería).

## Capabilities and Constraints

- **Capacidades confirmadas:**
  - Extracción y normalización de pedidos desde archivos PDF tabulares mediante PdfPig (`PdfService`).
  - Desglose inteligente de descripciones en componentes: Modelo, Color y Medida.
  - Agrupación por SKU y cálculo de cantidad sugerida.
  - Cruce de datos con catálogo maestro de Excel ("Lista de Precios") mediante EPPlus (`CatalogService`).
  - Transferencia directa a la cuadrícula de impresión principal (`FrmImpresion`).
  - Exportación de cuadrícula a Excel.
  - Generación de comandos ZPL directos con envío por spooler o archivo batch a impresora Zebra.
  - Auto-actualización integrada mediante Releases de GitHub.
- **Restricciones técnicas:**
  - Compatibilidad estricta con Windows Forms y .NET Framework 4.8.
  - Dimensiones fijas de etiqueta ZPL térmica para armazones (campos: Empresa, Modelo, Color, Medida, Precio, Código de Barras).
  - Límites de caracteres por campo para prevenir desbordamientos físicos en la etiqueta.

## Brand Commitments

- Nombre: Sistema de Impresión de Etiquetas.
- Identidad visual: WinForms empresarial con controles estándar, diseño de cuadrícula de alta densidad y flujos de teclado rápidos.

## Evidence on Hand

- Archivos de prueba reales en `files-resources/`:
  - `Etiquetas Armazones.xlsx` (catálogo y plantilla).
  - `T2026875.pdf` (factura/pedido con desglose de armazones).
- Código fuente funcional en C# con servicios `PdfService`, `CatalogService` y formularios `FrmImpresion` y `FrmImportarPdf`.

## Product Principles

1. **Eficiencia en la estación de trabajo:** Mínimos clics entre la recepción de la factura/pedido y la salida física de etiquetas de la impresora Zebra.
2. **Cero discrepancias de precio:** Priorizar la concordancia con el catálogo maestro de precios oficial antes de imprimir.
3. **Escritorio robusto y autónomo:** Sin dependencia de conexión a internet o servicios externos para el procesamiento de archivos o la impresión ZPL.
4. **Claridad en la captura:** Cuadrículas con validación visual inmediata y edición inline rápida para ajustes de último minuto.

## Accessibility & Inclusion

- Interfaz operable completamente mediante atajos de teclado y tabulación para operadores con guantes o en terminales industriales.
- Contraste alto con fuentes de sistema legibles (Segoe UI / Microsoft Sans Serif).
