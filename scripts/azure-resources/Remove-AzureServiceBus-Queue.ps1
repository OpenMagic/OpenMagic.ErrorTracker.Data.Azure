Param (
    [string] [Parameter(Mandatory = $true)] [ValidateNotNullOrEmpty()]
    $QueueName,

    [string] [Parameter(Mandatory = $true)] [ValidateNotNullOrEmpty()]
    $ConnectionString
)

$ErrorActionPreference = "Stop"
$VerbosePreference = "SilentlyContinue"

$namespaceManager = [Microsoft.ServiceBus.NamespaceManager]::CreateFromConnectionString($ConnectionString);
$namespaceManager.DeleteQueue($QueueName)
