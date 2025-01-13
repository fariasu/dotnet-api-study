using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using TaskManager.Domain.Enums;

namespace TaskManager.Infrastructure.Migrations.Versions;

[FluentMigrator.Migration(DatabaseVersions.TABLE_TASK, "Create table to save TaskEntity information")]
public class Version0000001 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Tasks")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Title").AsString(255).NotNullable()
            .WithColumn("Description").AsString(2000)
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("EndDate").AsDateTime().NotNullable()
            .WithColumn("TaskPriority").AsInt16().NotNullable().WithDefault(0)
            .WithColumn("TaskStatus").AsInt16().NotNullable().WithDefault(0);
    }
}