FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 59647
EXPOSE 44364

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["1.Services/Identity/Sector.Services.Identity/Sector.Services.Identity.csproj", "1.Services/Identity/Sector.Services.Identity/"]
COPY ["0.SharedKernel/SharedKernel.Common/SharedKernel.Common.csproj", "0.SharedKernel/SharedKernel.Common/"]
COPY ["1.Services/Identity/Sector.Services.Identity.Contract/Sector.Services.Identity.Contract.csproj", "1.Services/Identity/Sector.Services.Identity.Contract/"]
COPY ["0.SharedKernel/SharedKernel.Infrastructure/SharedKernel.Infrastructure.csproj", "0.SharedKernel/SharedKernel.Infrastructure/"]
COPY ["0.SharedKernel/SharedKernel.Implementation/SharedKernel.Implementation.csproj", "0.SharedKernel/SharedKernel.Implementation/"]
RUN dotnet restore "1.Services/Identity/Sector.Services.Identity/Sector.Services.Identity.csproj"
COPY . .
WORKDIR "/src/1.Services/Identity/Sector.Services.Identity"
RUN dotnet build "Sector.Services.Identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sector.Services.Identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NM.Sector.Services.Identity.dll"]