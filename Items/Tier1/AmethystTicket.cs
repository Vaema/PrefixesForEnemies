using Terraria.ModLoader;

namespace EnemyMods.Items.Tier1
{
    class AmethystTicket : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;
            Item.value = 1000;
            Item.rare = 2;
            Item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Amethyst Cosmic Ticket");
      // Tooltip.SetDefault("");
    }

    }
}
