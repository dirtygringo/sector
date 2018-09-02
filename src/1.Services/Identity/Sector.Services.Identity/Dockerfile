FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 59647
EXPOSE 44364

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/1.Services/Identity/Sector.Services.Identity/Sector.Services.Identity.csproj", "src/1.Services/Identity/Sector.Services.Identity/"]
COPY ["src/0.SharedKernel/SharedKernel.Common/SharedKernel.Common.csproj", "src/0.SharedKernel/SharedKernel.Common/"]
COPY ["src/1.Services/Identity/Sector.Services.Identity.Contract/Sector.Services.Identity.Contract.csproj", "src/1.Services/Identity/Sector.Services.Identity.Contract/"]
COPY ["src/0.SharedKernel/SharedKernel.Infrastructure/SharedKernel.Infrastructure.csproj", "src/0.SharedKernel/SharedKernel.Infrastructure/"]
COPY ["src/0.SharedKernel/SharedKernel.Implementation/SharedKernel.Implementation.csproj", "src/0.SharedKernel/SharedKernel.Implementation/"]
RUN dotnet restore "src/1.Services/Identity/Sector.Services.Identity/Sector.Services.Identity.csproj"
COPY . .
WORKDIR "/src/src/1.Services/Identity/Sector.Services.Identity"
RUN dotnet build "Sector.Services.Identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sector.Services.Identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NM.Sector.Services.Identity.dll"]