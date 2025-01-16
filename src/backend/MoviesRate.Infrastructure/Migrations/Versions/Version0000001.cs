using FluentMigrator;

namespace MoviesRate.Infrastructure.Migrations.Versions;

[Migration(DataBaseVersions.TABLE_USER, "Initial user table")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Email").AsString(160).NotNullable().Unique()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("Identifier").AsGuid().NotNullable().Unique();
    }
}