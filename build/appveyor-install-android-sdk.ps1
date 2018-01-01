$AndroidToolPath = "${env:ProgramFiles(x86)}\Android\android-sdk\tools\android.bat" 
$AndroidSdkManagerToolPath = "${env:ProgramFiles(x86)}\Android\android-sdk\tools\bin\sdkmanager.bat" 

Function Get-AndroidSDKs() { 
	$output = & $AndroidToolPath list sdk --all 
	$sdks = $output |% { 
		if ($_ -match '(?<index>\d+)- (?<sdk>.+), revision (?<revision>[\d\.]+)') { 
			$sdk = New-Object PSObject 
			Add-Member -InputObject $sdk -MemberType NoteProperty -Name Index -Value $Matches.index 
			Add-Member -InputObject $sdk -MemberType NoteProperty -Name Name -Value $Matches.sdk 
			Add-Member -InputObject $sdk -MemberType NoteProperty -Name Revision -Value $Matches.revision 
			$sdk 
		} 
	} 
	$sdks 
}

Function Install-AndroidSDK() { 
	[CmdletBinding()] 
	Param( 
		[Parameter(Mandatory=$true, Position=0)] 
		[PSObject[]]$sdks 
	)

	$sdkIndexes = $sdks |% { $_.Index } 
	$sdkIndexArgument = [string]::Join(',', $sdkIndexes) 
	Echo 'y' | & $AndroidToolPath update sdk -u -a -t $sdkIndexArgument 
}

$sdks = Get-AndroidSDKs |? { $_.name -like 'sdk platform*API 10*' -or $_.name -like 'google apis*api 10' } 
Write-Host "Going to install SDKs: " $sdks
Install-AndroidSDK -sdks $sdks
Write-Host "Installed SDKs: " $sdks

Write-Host "Going to accept licenses"
for ($i=0; $i -lt 30; $i++) { $response += "y`n"}; $response | cmd /c '$AndroidSdkManagerToolPath 2>&1' --update
Write-Host "Accepted licenses"
