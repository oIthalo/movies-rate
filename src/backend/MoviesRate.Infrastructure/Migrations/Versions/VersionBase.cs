using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MoviesRate.Infrastructure.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
    {
        return Create.Table(table)
           .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
           .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
           .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefaultValue("CURRENT_TIMESTAMP");
    }
}