using FluentMigrator;

namespace MoviesRate.Infrastructure.Migrations.Versions;

[Migration(DataBaseVersions.TABLE_REVIEWS, "Table reviews to storage the movies reviews")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Reviews")
            .WithColumn("UserIdentifier").AsGuid().NotNullable().ForeignKey("FK_Reviews_User_Identifier", "Users", "Identifier")
            .WithColumn("MovieId").AsInt64().NotNullable()
            .WithColumn("Comments").AsString(500).NotNullable()
            .WithColumn("Rating").AsDecimal().NotNullable();
    }
}