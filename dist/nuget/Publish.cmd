@ECHO OFF

SET NUGETAPI=2df05807-86fc-4284-888c-5d3037274747

CALL :UPDATEPACKAGE Babaganoush.Sitefinity

GOTO:EOF

:UPDATEPACKAGE
SET PROJECT=%~1

ECHO Packaging NuGet's...
"../../.nuget/nuget" pack .\%PROJECT%\%PROJECT%.nuspec -OutputDirectory ./%PROJECT%

SET /P VERSION=Version to publish (leave blank to skip)?
IF "%VERSION%"=="" GOTO:EOF
ECHO Publishing NuGet's to %NUGETURL%
"../../.nuget/nuget" push .\%PROJECT%\%PROJECT%.%VERSION%.nupkg %NUGETAPI%
SET VERSION=""
GOTO:EOF