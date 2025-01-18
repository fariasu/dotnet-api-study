using System.Data;
using FluentMigrator;

namespace TaskManager.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_TASK_AND_USERS, "Create table to save TaskEntity and UserEntity information")]
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
            .WithColumn("TaskStatus").AsInt16().NotNullable().WithDefault(0)
            .WithColumn("CreatorId").AsInt64().NotNullable();

        Create.Table("Users")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Password").AsString(512).NotNullable()
            .WithColumn("UserIdentifier").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid);

        Create.ForeignKey("FK_Tasks_Users")
            .FromTable("Tasks").ForeignColumn("CreatorId")
            .ToTable("Users").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);
    }
}