ApiKey=$1
Source=$2

nuget push ./src/WriteOnce.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source