using Terraria.ModLoader;

namespace EnemyMods.Items.Tier6
{
    class AmberTicket : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;
            Item.value = 1000;
            Item.rare = 10;
            Item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Amber Cosmic Ticket");
      // Tooltip.SetDefault("");
    }

    }
}
