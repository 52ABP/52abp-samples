@echo off
@echo Deleting all BIN, OBJ folders...
for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"

@echo.
@echo BIN and OBJ �����ļ��ж��Ѿ�ɾ���ɾ��� :) ���Թرյ���������ˡ�
@echo.
@echo.
pause > nul