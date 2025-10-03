using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class VenomfangGreatarrow : ModItem
    {
        public override void SetDefaults()
            {

                Item.damage = 20;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 18;
                Item.height = 48;
                Item.maxStack = 999;

                Item.consumable = true;
                Item.knockBack = 3f;
                Item.value = 1000;
                Item.rare = 6;
                Item.shoot = Mod.Find<ModProjectile>("VenomfangGreatarrow").Type;
                Item.shootSpeed = -2f;
                Item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Venomfang Greatarrow");
      // Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(Mod.Find<ModItem>("EmeraldTicket").Type);
            recipe.Register();
        }
    }
}
