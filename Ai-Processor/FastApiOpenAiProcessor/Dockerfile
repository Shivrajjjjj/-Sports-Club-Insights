FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FastApiOpenAiProcessor/FastApiOpenAiProcessor.csproj", "FastApiOpenAiProcessor/"]
RUN dotnet restore "FastApiOpenAiProcessor/FastApiOpenAiProcessor.csproj"
COPY . .
WORKDIR "/src/FastApiOpenAiProcessor"
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "FastApiOpenAiProcessor.dll"]
