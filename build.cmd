@echo off

IF NOT EXIST out mkdir out

csc /target:winexe /out:.\out\prose.exe /r:System.dll /r:c:\windows\microsoft.net\Framework\v4.0.30319\WPF\WindowsBase.dll /r:C:\windows\microsoft.net\Framework\v4.0.30319\WPF\PresentationCore.dll /r:C:\windows\microsoft.net\Framework\v4.0.30319\WPF\PresentationFramework.dll /recurse:src\*.cs /nologo