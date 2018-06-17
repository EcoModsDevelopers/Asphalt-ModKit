node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
	    bat "nuget restore"
		bat "\"${tool 'MSBuild'}\" Asphalt-ModKit.sln /p:Configuration=Release /p:Platform=x64 /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"

	stage 'Archive'
	    bat "for /R packages %%a in (0Harmony.dll) do xcopy /Y \"%%a\" Mods || call (exit /b 0)"
	    bat "for /R packages %%a in (\"net45\SharpYaml.dll\") do xcopy /Y \"%%a\" Mods || call (exit /b 0)"
		bat "7z a Asphalt-ModKit-Snapshot-${BUILD_NUMBER}.zip Mods/"
		archiveArtifacts 'Asphalt-ModKit-Snapshot-${BUILD_NUMBER}.zip'
		cleanWs()
}