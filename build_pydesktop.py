#import PyInstaller

#pyinstaller.exe -F -c -n pyDesktop -i .\pyDesktop\resource\pydesktop.ico --add-data ".\pyDesktop\resource\pydesktop.png;pydesktop\resource" --add-data ".\metric\OpenHardwareMonitorLib.dll;metric" .\media_dispaly.py

#pyinstaller.exe --onedir --windowed -c -n pyDesktop -i .\pyDesktop\resource\pydesktop.ico --add-data ".\pyDesktop\resource\pydesktop.png;pydesktop\resource" --add-data ".\metric\OpenHardwareMonitorLib.dll;metric" .\media_dispaly.pyw
