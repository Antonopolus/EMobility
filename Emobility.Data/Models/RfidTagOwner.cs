using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EMobility.Data
{
    public class RfidTagOwner
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string  RfidTagId { get; set; }

        public DateTime From { get; set; }
        public DateTime? Until { get; set; }
    }

    public class RfidTagOwnerEntityConfiguration : IEntityTypeConfiguration<RfidTagOwner>
    {

        void IEntityTypeConfiguration<RfidTagOwner>.Configure(EntityTypeBuilder<RfidTagOwner> builder)
        {
            builder.HasKey(cs => cs.Id);
            //builder.HasIndex(owner => owner.RfidTagId).IsUnique();

            builder.HasCheckConstraint("UntilAfterFrom", 
                $@"[{nameof(RfidTagOwner.Until)}] IS NULL OR [{nameof(RfidTagOwner.Until)}] > [{nameof(RfidTagOwner.From)}]");

            builder.HasData(new RfidTagOwner() { Id = 1, Owner = "Christian", RfidTagId = "0x...", From = DateTime.UtcNow});
            builder.HasData(new RfidTagOwner() { Id = 2, Owner = "Thomas", RfidTagId = "0x...", From = DateTime.UtcNow });
            builder.HasData(new RfidTagOwner() { Id = 3, Owner = "Florian", RfidTagId = "0x...", From = DateTime.UtcNow });
        }
    }
}
