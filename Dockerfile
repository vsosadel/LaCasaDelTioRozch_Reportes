FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env

WORKDIR /lacasadeltiorozchapireportes

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
WORKDIR /lacasadeltiorozchapireportes/Api
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
# Change timezone to local time
ENV TZ=America/Mexico_City
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

COPY --from=build-env /lacasadeltiorozchapireportes/Api/out .
EXPOSE 80/tcp
EXPOSE 443/tcp
ENTRYPOINT ["dotnet", "Api.dll"]
