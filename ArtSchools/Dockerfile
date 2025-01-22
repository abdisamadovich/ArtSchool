#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#
#COPY ["ArtSchools/ArtSchools.csproj", "ArtSchools/"]
#RUN dotnet restore "ArtSchools/ArtSchools.csproj" --verbosity detailed --disable-parallel
#
#COPY . .
#WORKDIR "/src/ArtSchools"
#RUN dotnet build "ArtSchools.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "ArtSchools.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

COPY app/publish/ .

ENTRYPOINT ["dotnet", "ArtSchools.dll"]