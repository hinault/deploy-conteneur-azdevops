#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/src/TestApp.MVC/TestApp.MVC.csproj", "src/TestApp.MVC/"]
COPY ["/src/TestApp.ApplicationCore/TestApp.ApplicationCore.csproj", "src/TestApp.ApplicationCore/"]
COPY ["/src/TestApp.Infrastructure/TestApp.Infrastructure.csproj", "src/TestApp.Infrastructure/"]
COPY ["/tests/TestApp.MVC.UnitTests/TestApp.MVC.UnitTests.csproj", "tests/TestApp.MVC.UnitTests/"]
RUN dotnet restore "src/TestApp.MVC/TestApp.MVC.csproj"
RUN dotnet restore "tests/TestApp.MVC.UnitTests/TestApp.MVC.UnitTests.csproj"
COPY . .
WORKDIR "/src/tests/TestApp.MVC.UnitTests"
RUN dotnet build "TestApp.MVC.UnitTests.csproj" -c Release -o /app/build
RUN dotnet test "TestApp.MVC.UnitTests.csproj" --logger "trx;LogFileName=TestApp.MVC.trx"
WORKDIR "/src/src/TestApp.MVC"
RUN dotnet build "TestApp.MVC.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "TestApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApp.MVC.dll"]