$assemblyInfoPath = "Asphalt-ModKit\Properties\AssemblyInfo.cs"

$regex = '^\[assembly: AssemblyVersion\("(.*?)"\)\]'
$assemblyInfo = Get-Content $assemblyInfoPath -Raw

$version = [Regex]::Match(
        $assemblyInfo, 
        $regex,
        [System.Text.RegularExpressions.RegexOptions]::Multiline
    ).Groups[1].Value
	
$version | Out-File -FilePath Version -NoNewline