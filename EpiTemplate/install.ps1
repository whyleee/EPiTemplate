# params
$proj_name = "EPiTemplate"
$hostname = "EPiTemplate.com"
$nuget_server = "http://159.224.42.176"

# env
$root_path = split-path (Get-Location).Path -parent
$web_path = (Get-Location).Path
$progress = 0

# packages
$packages = @{
    "EPiServer.CMS.AppData.7.1.2" = $root_path;
    "EPiServer.CMS.Modulesbin.7.1.2" = $web_path;
}

# install
$script = {
    try
    {
        Import-Module WebAdministration
        
        Build-Solution
        Install-Packages $packages
        Create-Website $proj_name $hostname $web_path
        Launch-Website $proj_name $hostname
    }
    catch
    {
        Write-Host ($_ | Format-List | Out-String) -Foreground 'Red'
        echo "Installation failed."
    }
}


################################################################
# Functions
################################################################

Function Write-InstallProgress($done, $op)
{
    $status = "$([math]::round($done))% complete"
    Write-Progress -Activity "Installing $proj_name..." -Status $status -PercentComplete $done -CurrentOperation $op
    $progressMsg = "[{0,-4}] $op" -f "$done%"
    echo $progressMsg
}

Function Write-InstallCompleted()
{
    $completeMsg = "Installation finished."
    Write-Progress -Activity "Installing $proj_name..." -Completed -Status "100% complete" -CurrentOperation $completeMsg
    echo "[100%] $completeMsg"
}

Function Register-CustomCultures()
{
    .tools\cigen add -n en-DK -b en -r da-DK --english-name "English (Denmark)" --native-name "English (Danmark)"
}

Function Build-Solution()
{
    Write-InstallProgress $progress "Building solution..."

    & .\build.cmd

    $script:progress += 30
}

Function Install-Package($package, $to)
{
    $packageZip = "$package.zip"

    # download
    $url = $nuget_server + "/content/" + $packageZip
    $zip_path = Join-Path $root_path $packageZip
    $wc = New-Object System.Net.WebClient
    $wc.DownloadFile($url, $zip_path)

    # unzip
    $shell_app = new-object -com shell.application
    $zip_file = $shell_app.namespace($zip_path)
    $destination = $shell_app.namespace($to)
    $destination.Copyhere($zip_file.items(), 0x10)

    # cleanup
    Remove-Item $zip_path
}

Function Install-Packages($packages)
{
    $i = 0
    $packages.GetEnumerator() | % {
        $script:progress = $progress + (60 / $packages.Count) * $i
        Write-InstallProgress $progress "Installing $($_.key)..."
        Install-Package $_.key $_.value
        $i++
    }
    $script:progress = 60
}

Function Create-Website($name, $hostname, $web_path)
{
    Write-InstallProgress $progress "Installing $name website..."

    # iis
    $appPool = New-WebAppPool $name
    $appPool.processModel.identityType = "ApplicationPoolIdentity"
    Set-ItemProperty "IIS:\AppPools\$name" managedRuntimeVersion v4.0
    New-WebSite -Name $name -Port 80 -HostHeader "local.$hostname" -ApplicationPool $name -PhysicalPath "$web_path"
    #New-WebBinding -Name $name -IPAddress "*" -Port 80 -HostHeader "my.$hostname"

    # hosts
    $hosts_path = "$env:windir\System32\drivers\etc\hosts"
    $hosts_header = @"



######################################################################
# $name
######################################################################

"@
    $hosts_line = "127.0.0.1`tlocal.$hostname"
    $hosts_header + $hosts_line | Out-File -encoding ASCII -append $hosts_path

    $script:progress += 5
}

Function Launch-Website($name, $hostname)
{
    Write-InstallProgress $progress "Launching $name website..."

    # launch
    Start-WebSite $name
    start "http://local.$hostname"

    $script:progress += 5
}

# invoke script
try {
    & $script
    Write-InstallCompleted
} catch {
    Write-Error $_
    echo "Installation failed."
}