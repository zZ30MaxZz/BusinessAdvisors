FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./SoftTeK.BusinessAdvisors.Api/SoftTeK.BusinessAdvisors.Api.csproj" --disable-parallel
RUN dotnet publish "./SoftTeK.BusinessAdvisors.Api/SoftTeK.BusinessAdvisors.Api.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
ENTRYPOINT ["dotnet", "SoftTeK.BusinessAdvisors.Api.dll"]