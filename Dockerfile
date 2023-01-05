FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS build-env
WORKDIR /src
COPY src/*.csproj .
RUN dotnet restore
COPY src .
RUN dotnet publish --os linux --arch x64 -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT [ "dotnet","TPCSharp.exe" ]


