
$version = Get-Content "Version" -Raw

echo $version

nuget pack Asphalt-ModKit\\Asphalt-ModKit.nuspec -Version $version