using Terraria.ModLoader;

namespace EnemyMods.Items.Tier5
{
    class RubyTicket : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;
            Item.value = 1000;
            Item.rare = 8;
            Item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Ruby Cosmic Ticket");
      // Tooltip.SetDefault("");
    }

    }
}
