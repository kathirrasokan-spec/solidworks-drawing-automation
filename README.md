# SOLIDWORKS Drawing Automation

A C#/.NET Framework demo project that automates parts of the SOLIDWORKS drawing workflow through the SOLIDWORKS API.

## What it demonstrates

- Preparing drawings from part documents, including a batch workflow
- Placing drawing views and section views
- Adding basic, hole, fillet, and pattern dimensions
- Validating documents before processing
- Selecting drawing sheet sizes from A0 through A4

The project includes a user manual, an error-handling guide, sample drawing PDFs, and screen recordings. The sample title blocks use generic demo placeholders.

## Requirements

- Windows
- SOLIDWORKS installed locally (the recordings show SOLIDWORKS 2020)
- Visual Studio with the .NET Framework 4.7.2 developer pack
- SOLIDWORKS API interop assemblies available from the local SOLIDWORKS installation

SOLIDWORKS API compatibility can vary by version. You may need to update the interop references in the project to match your installed release.

## Setup

1. Open `Source Code/DrawingAutomation/DrawingAutomation.sln` in Visual Studio.
2. Confirm that the three SOLIDWORKS interop references resolve on your machine.
3. Place your own drawing templates in a `Templates` folder beside the built assembly. The demo expects these filenames: `A0 Drawing.DRWDOT`, `A1 Drawing.DRWDOT`, `A2 Drawing.DRWDOT`, `A3 Drawing.DRWDOT`, `A4 Landscape.DRWDOT`, and `A4 Portrait.DRWDOT`.
4. Build the solution and register/load the add-in using the SOLIDWORKS add-in setup appropriate for your Windows installation.

The template files are not included. Use templates that you are authorized to use.

## Repository layout

| Folder | Contents |
|---|---|
| `Documentation` | User manual and error-handling guide |
| `Drawing Sample` | Example drawing PDFs |
| `Source Code` | Visual Studio solution and C# source |
| `Videos` | Demo recordings |

## Notes

This is a demonstration project, not a production-ready installer. It requires SOLIDWORKS and locally available API assemblies. No SOLIDWORKS interop binaries or drawing templates are redistributed here.
