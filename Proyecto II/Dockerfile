# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiamos el archivo de proyecto y restauramos las dependencias
COPY ["Proyecto II.csproj", "./"]
RUN dotnet restore "./Proyecto II.csproj"

# Copiamos el resto del código fuente
COPY . .

# Publicamos la aplicación en modo Release y generamos la carpeta de salida
RUN dotnet publish "Proyecto II.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copiamos los archivos publicados desde la etapa de build
COPY --from=build /app/publish .

# Configuramos la variable de entorno para que la aplicación escuche en el puerto 80
ENV ASPNETCORE_URLS=http://+:80

# Exponemos el puerto 80
EXPOSE 80

# Configuramos el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Proyecto II.dll"]
