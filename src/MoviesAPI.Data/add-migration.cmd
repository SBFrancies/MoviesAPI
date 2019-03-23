@SET name=%1
@IF ["%name%"] == [""] (
   echo ERROR: Must provide a migration name.
   EXIT /B 2
)

@dotnet ef migrations add %name% -c MoviesDbContext -o Migrations