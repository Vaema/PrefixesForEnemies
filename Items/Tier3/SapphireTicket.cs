using Terraria.ModLoader;

namespace EnemyMods.Items.Tier3
{
    class SapphireTicket : ModItem
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
      // DisplayName.SetDefault("Sapphire Cosmic Ticket");
      // Tooltip.SetDefault("");
    }

    }
}
