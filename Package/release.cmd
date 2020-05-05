"..\..\oqtane.framework\oqtane.package\nuget.exe" pack JimS.FAQs.Module.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\wwwroot\Modules\" /Y
