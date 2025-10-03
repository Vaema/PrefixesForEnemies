using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier1
{
    public class IronGreatarrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 48;
            Item.maxStack = 999;

            Item.consumable = true;
            Item.knockBack = 3f;
            Item.value = 200;
            Item.rare = 2;
            Item.shoot = Mod.Find<ModProjectile>("IronGreatarrow").Type;
            Item.shootSpeed = -2f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Iron Greatarrow");
            // Tooltip.SetDefault("Too heavy for normal bows to use effectively");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(Mod.Find<ModItem>("AmethystTicket").Type);
            recipe.Register();
        }
    }
}
