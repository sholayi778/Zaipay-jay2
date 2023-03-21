#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

#ENV ASPNETCORE_ENVIRONMENT=Development #For swagger.

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Zaipay/Zaipay.csproj", "Zaipay/"]
COPY ["out/assembly/csharp-netcore/src/Org.OpenAPITools/Org.OpenAPITools.csproj", "out/assembly/csharp-netcore/src/Org.OpenAPITools/"]
RUN dotnet restore "Zaipay/Zaipay.csproj"
COPY . .
WORKDIR "/src/Zaipay"
RUN dotnet build "Zaipay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zaipay.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zaipay.dll"]