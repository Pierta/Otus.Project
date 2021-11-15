#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#docker build -t pierta/otus.project.crudapi -f Dockerfile ..

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Otus.Project.CrudApi/Otus.Project.CrudApi.csproj", "Otus.Project.CrudApi/"]
RUN dotnet restore "Otus.Project.CrudApi/Otus.Project.CrudApi.csproj"
COPY . .
WORKDIR "/src/Otus.Project.CrudApi"
RUN dotnet build "Otus.Project.CrudApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Otus.Project.CrudApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otus.Project.CrudApi.dll"]