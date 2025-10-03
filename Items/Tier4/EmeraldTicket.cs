using Terraria.ModLoader;

namespace EnemyMods.Items.Tier4
{
    class EmeraldTicket : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;
            Item.value = 1000;
            Item.rare = 6;
            Item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Emerald Cosmic Ticket");
      // Tooltip.SetDefault("");
    }

    }
}
