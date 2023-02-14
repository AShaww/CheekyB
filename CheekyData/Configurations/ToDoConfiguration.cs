using CheekyData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheekyData.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.HasKey(g => g.ToDoId);
        builder.HasOne(a => a.User).WithOne(a => a.ToDos).HasForeignKey<ToDo>(a => a.UserId);
        builder.Property(p => p.ToDoId).ValueGeneratedOnAdd();
        builder.Property(p => p.ToDoTitle).HasMaxLength(30);
        builder.Property(p => p.ToDoMessage).HasMaxLength(250);
        builder.Property(p => p.ToDoDateModified).ValueGeneratedOnUpdate();
        builder.ToTable("ToDo").HasData(ToDoSeed());
    }

    private static IEnumerable<ToDo> ToDoSeed()
    {
        return new List<ToDo>
        {
            new()
            {
                ToDoId = Guid.Parse("fde34b5e-5b1a-46d6-9fe7-b6896cf86517"),
                UserId = Guid.Parse("830e9471-9d6e-4557-8bf5-ec89d375d933"),
                ToDoTitle = "Something about something 1",
                ToDoMessage = "Well there was one day the ended and the new day started"
            },
            new()
            {
                ToDoId = Guid.Parse("59887cb4-62be-4d64-a7cf-70a608c84d7b"),
                UserId = Guid.Parse("830e9471-9d6e-4557-8bf5-ec89d375d933"),
                ToDoTitle = "Something about something 2",
                ToDoMessage = "Well there was one day the ended and the new day started"
            },
            new()
            {
                ToDoId = Guid.Parse("f783e4e6-f492-4ecf-8362-fd4834ab37d7"),
                UserId = Guid.Parse("830e9471-9d6e-4557-8bf5-ec89d375d933"),
                ToDoTitle = "Something about something 3",
                ToDoMessage = "Well there was one day the ended and the new day started"
            },
            new()
            {
                ToDoId = Guid.Parse("5fce3a3a-a421-4830-a49b-f8813d6d4fb9"),
                UserId = Guid.Parse("830e9471-9d6e-4557-8bf5-ec89d375d933"),
                ToDoTitle = "Something about something",
                ToDoMessage = "Well there was one day the ended and the new day started"
            }
        };
    }
}