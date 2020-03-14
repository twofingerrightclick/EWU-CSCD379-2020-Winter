$WebServer = Start-Process -filepath dotnet -ArgumentList "run -p src\\SecretSanta.Web\\SecretSanta.Web.csproj --urls=http://localhost:44394/" -NoNewWindow -PassThru -WorkingDirectory "$PSScriptRoot"

$ApiServer = Start-Process -filepath dotnet -ArgumentList "run -p src\\SecretSanta.Api\\SecretSanta.Api.csproj --urls=http://localhost:44388/" -NoNewWindow -PassThru -WorkingDirectory "$PSScriptRoot"
echo "hosts running"
Start-Sleep -s 13

Start-Process -wait -filepath dotnet -ArgumentList 'test' -WorkingDirectory "$PSScriptRoot\test\SecretSanta.Web.Tests\" 

$ApiServer, $WebServer | Stop-Process


