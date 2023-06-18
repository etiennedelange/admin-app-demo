[CmdletBinding()]
param (
    $serviceNameParam,
    $exePathParam
)

$serviceName = $serviceNameParam;
$exePath = $exePathParam;

$secondsToWaitForStop = 3
$secondsToWaitForUninstall = 3

"serviceNameParam: $($serviceNameParam)"
"exePathParam: $($exePathParam)"

$existingService = Get-WmiObject -Class Win32_Service -Filter "Name='$serviceName'"

if ($existingService) {
    "'$serviceName' exists already. Stopping."
    Stop-Service $serviceName
    "Waiting $secondsToWaitForStop seconds to allow existing service to stop."
    Start-Sleep -s $secondsToWaitForStop
    
    $existingService.Delete()
    "Waiting $secondsToWaitForUninstall seconds to allow service to be uninstalled."
    Start-Sleep -s $secondsToWaitForUninstall
}
"Installing the service."
New-Service -BinaryPathName $exePath -Name $serviceName -DisplayName $serviceName -StartupType Automatic 
"Installed the service."

"Starting the service."
Start-Service $serviceName
	
"Completed."