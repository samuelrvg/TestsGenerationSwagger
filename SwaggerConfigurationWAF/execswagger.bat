dotnet tool restore
dotnet tool run swagger tofile --output swagger_v3.json bin\Debug\net6.0\SwaggerConfigurationWAF.dll v1
dotnet tool run swagger tofile --serializeasv2 --output swagger_v2.json bin\Debug\net6.0\SwaggerConfigurationWAF.dll v1