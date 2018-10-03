Param(
	[switch]$Increment,
	[switch]$Preview,
	[string]$Path
)

$NugetKey = Get-Content "nuget.key"

if ($Path.EndsWith("\")) {
	$Path = $Path.Remove($Path.Length - 1, 1)
}

$FullPath = Resolve-Path $Path
$Proj = Get-ChildItem -Path ($FullPath.Path + "/" + $Path + ".csproj") -File

$FilePath = $Proj.FullName

Push-Location $Path

$XmlDocument = New-Object System.Xml.XmlDocument
$XmlDocument.PreserveWhitespace = $true
$XmlDocument.Load($FilePath)

$Version = $XmlDocument.Project.PropertyGroup.Version
if ($Increment) {
	$Parts = $Version.Split(".")
	$Major = $Parts[0]
	$Middle = $Parts[1]
	$End = $Parts[2].SPlit("-")

	[int]$Minor = $End[0]

	$Minor = $Minor + 1
	$Updated = "${Major}.${Middle}.${Minor}"

	if ($Preview) {
		$Updated = "${Updated}-preview"
	}
	
	$XmlDocument.Project.PropertyGroup.Version = $Updated
	
	$XmlWriterSettings = New-Object System.Xml.XmlWriterSettings
	$XmlWriterSettings.OmitXmlDeclaration = $true

	$XmlWriter = [System.Xml.XmlTextWriter]::Create($FilePath, $XmlWriterSettings)

	$XmlDocument.Save($XmlWriter)
	$XmlWriter.Close()
	$FullName = ".\bin\Release\${Path}.${Updated}.nupkg"

} else {
	$FullName = ".\bin\Release\${Path}.${Version}.nupkg"
}

dotnet pack -c Release

dotnet nuget push $FullName -k $NugetKey -s https://api.nuget.org/v3/index.json

Pop-Location