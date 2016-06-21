$ErrorActionPreference = "Stop"
$VerbosePreference = "SilentlyContinue"

Write-Host "Getting Azure Account..."
$azureAccount = Get-AzureAccount

If ($azureAccount -eq $null)
{
    $azureAccount = Add-AzureAccount
    If($azureAccount -eq $null)
    {
        Throw "Failed to add Azure Account."
    }
}

Return $azureAccount
