ApiKey=$1
Source=$2

nuget push ./src/WriteOnceContainer.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source
nuget push ./src/WriteOnceContainer.FluentAssertions.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source