#1
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["SquadServer/SquadServer.csproj", "SquadServer/"]
RUN dotnet restore "SquadServer/SquadServer.csproj"

COPY . .
WORKDIR "/src/SquadServer"
RUN dotnet build "SquadServer.csproj" -c Release -o /app/build


#2
FROM build AS publish
RUN dotnet publish "SquadServer.csproj" -c Release -o /app/publish /p:UseAppHost=false


#3
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app
COPY --from=publish /app/publish .

#open ports
EXPOSE 80
EXPOSE 443

# entry 
ENTRYPOINT ["dotnet", "SquadServer.dll"]