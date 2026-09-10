# Proyecto listo para GitHub

Este repositorio contiene la solución .NET (Razor Pages) lista para subir a GitHub.

Qué hice:
- Eliminé .gitignore.txt incorrecto.
- Añadí .gitignore con reglas para Visual Studio y .NET.

Pasos recomendados antes de subir (ejecutar en PowerShell o Git Bash en la raíz del repo):

1. Inicializar repositorio (si no existe):
   git init
2. Añadir remoto (si aún no lo ha hecho):
   git remote add origin https://github.com/USUARIO/REPO.git
3. Evitar subir artefactos ya rastreados (si los hay):
   git rm -r --cached bin obj
   git rm -r --cached .vs
   git commit -m "Remove build artifacts from tracking and add proper .gitignore"
4. Añadir y subir todo:
   git add .
   git commit -m "Initial commit"
   git branch -M main
   git push -u origin main

Notas:
- Si existen archivos bin/ u obj ya versionados en la historia, considere reescribir historial con BFG o git filter-repo.
- Revise el archivo .gitignore si desea incluir o excluir archivos específicos (por ejemplo, paquetes restaurables o librerías cliente en wwwroot/lib).