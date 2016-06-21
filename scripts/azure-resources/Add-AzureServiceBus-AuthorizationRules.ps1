Param (
    [string] [ValidateNotNullOrEmpty()]
    $NamespaceName = "errortracker",
    
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

$sendRuleName = "SendRule"
$sendRuleKey = [Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule]::GenerateRandomKey()

$listRuleName = "ListenRule"
$listenRuleKey = [Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule]::GenerateRandomKey()

$manageRuleName = "ManageRule"
$manageRuleKey = [Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule]::GenerateRandomKey()

Write-Host "Adding authorization rules..."
New-AzureSBAuthorizationRule -Name "Root$sendRuleName" -Namespace $NamespaceName -Permission $("Send") -PrimaryKey $sendRuleKey | Out-Null
New-AzureSBAuthorizationRule -Name "Root$listRuleName" -Namespace $NamespaceName -Permission $("Listen") -PrimaryKey $listenRuleKey | Out-Null
New-AzureSBAuthorizationRule -Name "Root$manageRuleName" -Namespace $NamespaceName -Permission $("Manage", "Listen","Send") -PrimaryKey $manageRuleKey | Out-Null
Write-Host "Successfully added authorization rules."

Write-Host
Write-Host "Copy & paste the following into LastPass & AppSettings."
Write-Host
Write-Host "<appSettings>"
Write-Host "  <add key=""AzureServiceBus_NamespaceAddress"" value=""errortracker"" />"
Write-Host "  <add key=""AzureServiceBus_SendRuleName"" value=""$sendRuleName"" />"
Write-Host "  <add key=""AzureServiceBus_SendRuleKey"" value=""$sendRuleKey"" />"
Write-Host "  <add key=""AzureServiceBus_ListenRuleName"" value=""$listenRuleName"" />"
Write-Host "  <add key=""AzureServiceBus_ListenRuleKey"" value=""$listenRuleKey"" />"
Write-Host "  <add key=""AzureServiceBus_ManageRuleName"" value=""$manageRuleName"" />"
Write-Host "  <add key=""AzureServiceBus_ManageRuleKey"" value=""$manageRuleKey"" />"
Write-Host "  <add key=""EventsQueue_Name"" value=""[production | tests]/events"" />"
Write-Host "</appSettings>"
Write-Host