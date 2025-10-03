using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier1
{
    public class IceShardRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 12;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Ice Shard Ring");
      // Tooltip.SetDefault("Shoots a lot of icicles. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[0] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int p = Projectile.NewProjectile(player.Center.X, player.Center.Y - 50, 0, 0, Mod.Find<ModProjectile>("IceShardSpawner").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[0]--;
            if(play.cooldowns[0] == -1)
            {
                play.cooldowns[0] = play.maxCooldowns[0];
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("AmethystTicket").Type, 3);
            recipe.Register();
        }
    }
}
