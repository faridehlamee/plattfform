#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DashBoard/DashBoard.csproj", "DashBoard/"]
COPY ["WebFrameWork/WebFrameWork.csproj", "WebFrameWork/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Entites/Entites.csproj", "Entites/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "DashBoard/DashBoard.csproj"
COPY . .
WORKDIR "/src/DashBoard"
RUN dotnet build "DashBoard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DashBoard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DashBoard.dll"]
