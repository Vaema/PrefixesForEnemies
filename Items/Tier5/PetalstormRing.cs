using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier5
{
    public class PetalstormRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 65;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 80000;
            Item.rare = 8;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Petalstorm Ring");
      // Tooltip.SetDefault("Nature's wrath blows through your foes. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[12] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X - 1600, player.Center.Y, 0, 0, Mod.Find<ModProjectile>("Petalstorm").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[12]--;
            if (play.cooldowns[12] == -1)
            {
                play.cooldowns[12] = play.maxCooldowns[12];
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("RazorwindRing").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("RubyTicket").Type, 3);
            recipe.Register();
        }
    }
}
