namespace SquadServer.Models.ModelsConfig;

public class MessageModelConfigure : IEntityTypeConfiguration<MessageModelEntity>
{
    public void Configure(EntityTypeBuilder<MessageModelEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(m => m.Sender).
                WithMany().
                HasForeignKey(m=>m.SenderId).
                OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(m=>m.Chat).
                WithMany(c=>c.Messages).
                HasForeignKey(m=>m.ChatId).
                OnDelete(DeleteBehavior.Cascade);
    }
}
