using FluentMigrator;

namespace MoviesRate.Infrastructure.Migrations.Versions;

[Migration(DataBaseVersions.DELETE_ON_CASCADE_IN_REVIEWS, "Deleting review on cascade")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        Delete.ForeignKey("FK_Reviews_User_Identifier").OnTable("Reviews");
        Create.ForeignKey("FK_Reviews_User_Identifier")
            .FromTable("Reviews").ForeignColumn("UserIdentifier")
            .ToTable("Users").PrimaryColumn("Identifier")
            .OnDelete(System.Data.Rule.Cascade);
    }
}