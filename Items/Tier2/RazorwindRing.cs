using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier2
{
    public class RazorwindRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 20;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 20000;
            Item.rare = 3;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Razorwind Ring");
      // Tooltip.SetDefault("Winds blow crystalline leaves through your foes. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[3] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X - 1600, player.Center.Y, 0, 0, Mod.Find<ModProjectile>("Razorwind").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[3]--;
            if (play.cooldowns[3] == -1)
            {
                play.cooldowns[3] = play.maxCooldowns[3];
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("TopazTicket").Type, 3);
            recipe.Register();
        }
    }
}
