FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Cocktails.API.csproj", "./"]
RUN dotnet restore "Cocktails.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Cocktails.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cocktails.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cocktails.API.dll"]
