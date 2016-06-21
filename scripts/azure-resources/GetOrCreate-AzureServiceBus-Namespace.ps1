Param (
    [string] [ValidateNotNullOrEmpty()]
    $Name = "errortracker",

    [string] [ValidateNotNullOrEmpty()]
    $Location = "West US",

    [Microsoft.Azure.ServiceManagemenet.Common.Models.PSAzureAccount]
    $azureAccount
)

$ErrorActionPreference = "Stop"
$VerbosePreference = "SilentlyContinue"

If ($azureAccount -eq $null)
{
    $azureAccount = .\GetOrAdd-AzureAccount.ps1
}
Write-Host "Using Azure Account '$($azureAccount.Id)'."

Write-Host "Searching for service bus '$Name'..."
$serviceBus = Get-AzureSBNamespace -Name $Name

If ($serviceBus -ne $null)
{
    $serviceBus
    Write-Host
    Write-Host "Found service bus '$Name'..."
    exit 0
}

Write-Host "Creating service bus '$Name'..."
New-AzureSBNamespace -Name $Name -Location $Location -CreateACSNamespace $false -NamespaceType Messaging
Write-Host
Write-Host "Successfully created service bus '$Name'..."
