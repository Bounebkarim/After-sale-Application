using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Client.Persistence.Configurations;
public class ClientConfiguration : IEntityTypeConfiguration<Domain.Client>
{
    public void Configure(EntityTypeBuilder<Domain.Client> builder)
    {
        builder.HasData(new Domain.Client(Guid.NewGuid(), "Emna", "Ben Zina", "11020305", "Rue de tunis km 0.5 cité jardin", "25756063") { CreatedBy = "karim", DateCreated = DateTime.UtcNow, ModifiedBy = "emna", DateModified = DateTime.UtcNow },
                        new Domain.Client(new Guid("B21D744A-C328-86D7-D45F-D3A483CF1BCA"), "Raven", "Rowland", "24122383", "P.O. Box 262, 1469 Hendrerit Road", "37724065") { CreatedBy = "karim", DateCreated = DateTime.UtcNow, ModifiedBy = "emna", DateModified = DateTime.UtcNow },
                        new Domain.Client(new Guid("9273F710-21D6-0CBA-42B8-7A22A3338CA9"), "Imelda", "Boone", "12248084", "157-1645 Dictum. St.", "31728272") { CreatedBy = "karim", DateCreated = DateTime.UtcNow, ModifiedBy = "emna", DateModified = DateTime.UtcNow },
                        new Domain.Client(new Guid("55122AF3-BB25-A27C-5751-BACEC0A31E67"), "Zane", "Brennan", "77643679", "Ap #566-3797 Non Rd.", "28362418") { CreatedBy = "karim", DateCreated = DateTime.UtcNow, ModifiedBy = "emna", DateModified = DateTime.UtcNow },
                        new Domain.Client(new Guid("B383EE1F-9073-E756-C759-894F9BEF1D39"), "Malcolm", "Sullivan", "87646134", "Ap #467-720 Cursus Road", "98258532") { CreatedBy = "karim", DateCreated = DateTime.UtcNow, ModifiedBy = "emna", DateModified = DateTime.UtcNow },
                        new Domain.Client(new Guid("377A02B7-4B2E-3B69-AC46-530E8054A026"), "Quinlan", "West", "32714860", "262-3388 A, Road", "14563616") { CreatedBy = "karim", DateCreated = DateTime.UtcNow, ModifiedBy = "emna", DateModified = DateTime.UtcNow },
                        new Domain.Client(new Guid("A21D744A-C328-86D7-D45F-D3A483CF1BCA"), "Lucas", "Crawford", "78901234", "123 Main Street", "45678901") { CreatedBy = "karim", DateCreated = DateTime.UtcNow, ModifiedBy = "emna", DateModified = DateTime.UtcNow }
    );
    }
}
