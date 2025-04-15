# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia el archivo del proyecto desde la carpeta "Proyecto II" al contenedor
COPY ["Proyecto II/Proyecto II.csproj", "Proyecto II/"]

# Copia todos los demás archivos (incluyendo la solución, otros proyectos, etc.)
COPY . .

# Establece el directorio de trabajo dentro del proyecto
WORKDIR "/src/Proyecto II"

# Restaura las dependencias del proyecto
RUN dotnet restore "Proyecto II.csproj"

# Publica la aplicación en modo Release y genera la carpeta de salida
RUN dotnet publish "Proyecto II.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copia la publicación desde la etapa anterior
COPY --from=build /app/publish .

# Configura la URL para que la aplicación escuche en el puerto 80
ENV ASPNETCORE_URLS=http://+:80

# Expone el puerto 80
EXPOSE 80

# Establece el comando de entrada para ejecutar la aplicación.
# Nota: Asegúrate de que el nombre del ensamblado publicado sea "Proyecto II.dll"
ENTRYPOINT ["dotnet", "Proyecto II.dll"]
