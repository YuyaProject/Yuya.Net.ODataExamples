FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ODataExample/ODataExample.csproj", "ODataExample/"]
RUN dotnet restore "ODataExample/ODataExample.csproj"
COPY . .
WORKDIR "/src/ODataExample"
RUN dotnet build "ODataExample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ODataExample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ODataExample.dll"]