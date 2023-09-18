FROM misterabvir/asp7 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM misterabvir/asp7 
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "TaskManager.dll"]
