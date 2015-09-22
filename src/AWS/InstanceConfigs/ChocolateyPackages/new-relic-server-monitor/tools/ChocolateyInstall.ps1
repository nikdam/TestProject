Update-SessionEnvironment

$arguments = @{}

  # Let's assume that the input string is something like this, and we will use a Regular Expression to parse the values
  # /L*v install.log /qn NR_LICENSE_KEY="your_license_key"

  # Now we can use the $env:chocolateyPackageParameters inside the Chocolatey package
  $packageParameters = $env:chocolateyPackageParameters

  # Default the values
  $licenseKey = "0"

  # Now parse the packageParameters using good old regular expression
  if ($packageParameters) {
      $match_pattern = "\/(?<option>([a-zA-Z_]+)):(?<value>([0-9a-fA-F]{40}[\r\n]*))"
      $option_name = 'option'
      $value_name = 'value'

      if ($packageParameters -match $match_pattern ){
          $results = $packageParameters | Select-String $match_pattern -AllMatches
          $results.matches | % {
            $arguments.Add(
                $_.Groups[$option_name].Value.Trim(),
                $_.Groups[$value_name].Value.Trim())
        }
      }
      else
      {
          Throw "License Key was found but was invalid (REGEX Failure)"
      }

      if ($arguments.ContainsKey("NR_LICENSE_KEY")) {
          Write-Host "License Key Argument Found"
          $licenseKey = $arguments["NR_LICENSE_KEY"]
      }

  } else {
      Write-Debug "No Package Parameters Passed in"
  }

  mkdir "C:\InstallLogs" -ErrorAction SilentlyContinue
  $silentArgs = "/L*v C:\InstallLogs\NRinstall.log /qn NR_LICENSE_KEY=" + $licenseKey

  Install-ChocolateyPackage 'NewRelicServerMonitor' 'msi' $silentArgs 'http://download.newrelic.com/windows_server_monitor/release/NewRelicServerMonitor_x86_3.3.3.0.msi' 'http://download.newrelic.com/windows_server_monitor/release/NewRelicServerMonitor_x64_3.3.3.0.msi'