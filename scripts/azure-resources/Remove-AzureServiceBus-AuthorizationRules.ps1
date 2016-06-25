Param (
    [string] [ValidateNotNullOrEmpty()]
    $NamespaceName = "errortracker",
    
    # [Microsoft.Azure.ServiceManagemenet.Common.Models.PSAzureAccount]
    $azureAccount
)

$ErrorActionPreference = "Stop"
$VerbosePreference = "SilentlyContinue"

If ($azureAccount -eq $null)
{
    $azureAccount = .\GetOrAdd-AzureAccount.ps1
}
Write-Host "Using Azure Account '$($azureAccount.Id)'."

Write-Host "Getting authorization rules..."
$authorizationRules = Get-AzureSBAuthorizationRule -Namespace $NamespaceName | Where-Object { $_.Name -ne "RootManageSharedAccessKey" }
Write-Host "Successfully got $($authorizationRules.Count) authorization rules."

Write-Host "Removing $($authorizationRules.Count) authorization rules..."
foreach ($authorizationRule in $authorizationRules)
{

    Try
    {
        Write-Verbose "Removing authorization rule '$($authorizationRule.Name)'..."
        Remove-AzureSBAuthorizationRule -Name  $authorizationRule.Name -Namespace $NamespaceName
        Write-Verbose "Successfully removed authorization rule '$($authorizationRule.Name)'."
    }
    Catch
    {
        Write-Error "Could not delete authorization rule '$($authorizationRule.Name)'."
        Throw
    }
}
Write-Host "Successfully removed $($authorizationRules.Count) authorization rules..."
