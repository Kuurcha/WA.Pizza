#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Wa.Pizza.Infrasctructure/Wa.Pizza.Infrasctructure.csproj", "Wa.Pizza.Infrasctructure/"]
COPY ["Wa.Pizza.Core/Wa.Pizza.Core.csproj", "Wa.Pizza.Core/"]
RUN dotnet restore "Wa.Pizza.Infrasctructure/Wa.Pizza.Infrasctructure.csproj"
COPY . .
WORKDIR "/src/Wa.Pizza.Infrasctructure"
RUN dotnet build "Wa.Pizza.Infrasctructure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wa.Pizza.Infrasctructure.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wa.Pizza.Infrasctructure.dll"]