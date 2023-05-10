using AgendaTenis.Core.Identity.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaTenis.Core.Identity.AcessoDados.Mapeamentos
{
    public class UsuarioMapping : IEntityTypeConfiguration<UsuarioEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Email)
                 .IsRequired()
                 .HasColumnName("Email")
                 .HasColumnType("nvarchar(100)");

            builder.Property(c => c.Senha)
                 .IsRequired()
                 .HasColumnName("Senha")
                 .HasColumnType("nvarchar(100)");
        }
    }
}
