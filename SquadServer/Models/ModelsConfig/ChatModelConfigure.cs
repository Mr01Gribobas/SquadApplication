namespace SquadServer.Models.ModelsConfig;

public class ChatModelConfigure : IEntityTypeConfiguration<ChatModelEntity>
{
    public void Configure(EntityTypeBuilder<ChatModelEntity> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(c => c.ChatName).
                                         HasMaxLength(100);
        builder.HasOne(c=>c.Team).
                WithMany().
                HasForeignKey(c=>c.TeamId).
                OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Users).
                WithMany(u => u.Chats).UsingEntity(j => j.ToTable("ChatUsers"));
        

    }
}
