@echo off
:: variables
set drive=X:\Backup\Daily_Work
set backupcmd=robocopy /e /purge
set mypath=%cd%

@echo ### Backing up %mypath%
%backupcmd% %mypath% "%drive%"
echo Backup Complete!
@pause
