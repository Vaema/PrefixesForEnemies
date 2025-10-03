using Terraria.ModLoader;

namespace EnemyMods.Items.Tier2
{
    class TopazTicket : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;
            Item.value = 1000;
            Item.rare = 3;
            Item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Topaz Cosmic Ticket");
      // Tooltip.SetDefault("");
    }

    }
}
