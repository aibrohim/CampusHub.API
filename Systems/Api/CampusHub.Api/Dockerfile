#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/CampusHub.Api/CampusHub.Api.csproj", "Systems/Api/CampusHub.Api/"]
COPY ["Shared/CampusHub.Library/CampusHub.Common.csproj", "Shared/CampusHub.Library/"]
COPY ["Services/CampusHub.Services.Settings/CampusHub.Services.Settings.csproj", "Services/CampusHub.Services.Settings/"]
COPY ["Services/CampusHub.Services.Logger/CampusHub.Services.Logger.csproj", "Services/CampusHub.Services.Logger/"]
COPY ["Data/CampusHub.Context/CampusHub.Context.csproj", "Data/CampusHub.Context/"]
COPY ["Data/CampusHub.Context.Entities/CampusHub.Context.Entities.csproj", "Data/CampusHub.Context.Entities/"]
COPY ["Data/CampusHub.Context.Migrations/CampusHub.Context.Migrations.PgSql/CampusHub.Context.Migrations.PgSql.csproj", "Data/CampusHub.Context.Migrations/CampusHub.Context.Migrations.PgSql/"]
COPY ["Data/CampusHub.Context.Seeder/CampusHub.Context.Seeder.csproj", "Data/CampusHub.Context.Seeder/"]
COPY ["Systems/Api/CampusHub.Api.Settings/CampusHub.Api.Settings.csproj", "Systems/Api/CampusHub.Api.Settings/"]
COPY ["Services/CampusHub.Services.Buildings/CampusHub.Services.Buildings.csproj", "Services/CampusHub.Services.Buildings/"]
COPY ["Services/CampusHub.Services.Rooms/CampusHub.Services.Rooms.csproj", "Services/CampusHub.Services.Rooms/"]
COPY ["Services/CampusHub.Services.Courses/CampusHub.Services.Courses.csproj", "Services/CampusHub.Services.Courses/"]
COPY ["Services/CampusHub.Services.Modules/CampusHub.Services.Modules.csproj", "Services/CampusHub.Services.Modules/"]
COPY ["Services/CampusHub.Services.Assessments/CampusHub.Services.Assessments.csproj", "Services/CampusHub.Services.Assessments/"]
COPY ["Services/CampusHub.Services.Groups/CampusHub.Services.Groups.csproj", "Services/CampusHub.Services.Groups/"]
COPY ["Services/CampusHub.Services.Guests/CampusHub.Services.Guests.csproj", "Services/CampusHub.Services.Guests/"]
COPY ["Services/CampusHub.Services.GuestLectures/CampusHub.Services.GuestLectures.csproj", "Services/CampusHub.Services.GuestLectures/"]
COPY ["Services/CampusHub.Services.Clubs/CampusHub.Services.Clubs.csproj", "Services/CampusHub.Services.Clubs/"]
COPY ["Services/CampusHub.Services.ClubMeetings/CampusHub.Services.ClubMeetings.csproj", "Services/CampusHub.Services.ClubMeetings/"]
RUN dotnet restore "Systems/Api/CampusHub.Api/CampusHub.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/CampusHub.Api"
RUN dotnet build "CampusHub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CampusHub.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CampusHub.Api.dll"]
