node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
	    bat "nuget restore"
		bat "\"${tool 'MSBuild'}\" Asphalt-ModKit.sln /p:Configuration=Release /p:Platform=\"x64\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"

	stage 'Archive'
		bat "7z a Asphalt-ModKit.zip Mods/"
		archiveArtifacts 'Asphalt-ModKit.zip'
		cleanWs()
}