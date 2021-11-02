
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN sed -i 's/deb.debian.org/mirrors.ustc.edu.cn/g' /etc/apt/sources.list \
    && apt-get update \
    && apt-get install -y ffmpeg \
    && apt-get clean && apt-get autoclean && apt-get autoremove \
    && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:4000
EXPOSE 80
EXPOSE 443
EXPOSE 4000


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BlogocomApiV2.csproj", "."]
RUN dotnet restore "./BlogocomApiV2.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BlogocomApiV2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogocomApiV2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogocomApiV2.dll"]