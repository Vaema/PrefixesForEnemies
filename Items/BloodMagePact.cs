using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items
{
    public class BloodMagePact : ModItem
    {
        public override void SetDefaults()
        {

            Item.accessory = true;
            Item.width = 10;
            Item.height = 10;
            Item.rare = 2;

            Item.value = 5000;
        }


    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Blood Mage's Pact");
      // Tooltip.SetDefault("Provides a blood well.");
    }

        public override void UpdateEquip(Player player)
        {
            if(player.FindBuffIndex(Mod.Find<ModBuff>("BloodWell").Type) == -1)
            {
                //int p = Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType("BloodWell"), 0, 0, player.whoAmI);
                Main.projectile[999] = new Projectile();
                Main.projectile[999].SetDefaults(Mod.Find<ModProjectile>("BloodWell").Type);
                Projectile proj = Main.projectile[999];
                proj.owner = player.whoAmI;
                proj.damage = 0;
                proj.knockBack = 0;
                proj.position = player.position;
                proj.identity = 999;
                proj.velocity = Vector2.Zero;
                proj.gfxOffY = 0f;
                proj.stepSpeed = 1f;
                proj.wet = false;

            }
            player.AddBuff(Mod.Find<ModBuff>("BloodWell").Type, 2);
        }/*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmethystTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
