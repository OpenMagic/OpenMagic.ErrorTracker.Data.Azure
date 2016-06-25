Param (
    [string] [ValidateNotNullOrEmpty()]
    $StorageAccountName = "errortracker",

    [string] [ValidateNotNullOrEmpty()]
    $Location = "West US",

    [string] [ValidateNotNullOrEmpty()]
    $StorageType = "Standard_GRS",

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

Write-Host "Searching for storage account '$StorageAccountName'..."
$storageAccount = Get-AzureStorageAccount -StorageAccountName $StorageAccountName -ErrorAction SilentlyContinue

If ($storageAccount -ne $null)
{
    Write-Host "Found storage account '$StorageAccountName'..."
}
Else
{
    Write-Host "Creating storage account '$StorageAccountName'..."
    $storageAccount = New-AzureStorageAccount -StorageAccountName $StorageAccountName -Location $Location -Type $StorageType
    Write-Host "Successfully created storage account '$StorageAccountName'..."
}

$storageAccount

$storageKey = Get-AzureStorageKey -StorageAccountName "errortracker"

$storageKey

Write-Host
Write-Host "Copy & paste the following into LastPass & AppSettings."
Write-Host
Write-Host "<appSettings>"
Write-Host "  <add key=""Azure_Storage_ConnectionString"" value=""DefaultEndpointsProtocol=https;AccountName=$($storageKey.StorageAccountName);AccountKey=$($storageKey.Primary)"" />"
Write-Host "  <add key=""Azure_Storage_TableNamePrefix"" value=""[empty | test]"" />"
Write-Host "</appSettings>"
Write-Host
