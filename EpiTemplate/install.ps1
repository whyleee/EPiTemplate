# env
$root_path = split-path (Get-Location).Path -parent
$web_path = (Get-Location).Path
$db_path = Join-Path $root_path "db"
$nuget_server = "http://159.224.42.176"
$db_server = ".\sqlexpress"

# params
$sitename = "EPiTemplate"
$hostname = "EPiTemplate.com"

# packages
$packages = @{
    "EPiServer.CMS.AppData.7.1.2" = $root_path;
    "EPiServer.CMS.Modulesbin.7.1.2" = $web_path;
    "EPiServer.CMS.Db.7.0.586.1" = $db_path;
}

# install 
$script = {
        Build-Solution
        Set-IisSqlAcessRights $root_path
        Install-Packages $packages
        Create-Website $sitename $hostname $web_path
        Launch-Website $sitename $hostname
}

try {
    . .\install_utils.ps1
    Start-Logging
    Import-Module WebAdministration
    & $script
    Write-InstallCompleted
} catch {
    Write-Host ($_ | Format-List | Out-String) -Foreground 'Red'
    echo "Installation failed."
} finally {
    Stop-Logging
}