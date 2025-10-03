using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class FireCrackerRing : ModItem
    {
        int maxCharges = 2;
        int rechargeTime = 120;
        int charges = 2;
        int rechargeCount = 0;

        //conditional charge modifiers go here

        public override void SetDefaults()
        {

            Item.damage = 50;
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
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
            Item.shootSpeed = 5f;
            Item.shoot = Mod.Find<ModProjectile>("Firecracker").Type;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Firecracker Ring");
      // Tooltip.SetDefault("Shoots an unpredictable explosive blast. Two charges.");
    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            Main.projectile[p].ai[1] = (float)Main.rand.Next(-20, 21) / 200;
            Main.projectile[p].ai[2] = (float)Main.rand.Next(-20, 21) / 200;
            charges--;
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (charges <= 0)
            {
                return false;
            }
            else return true;
        }
        public override void UpdateInventory(Player player)
        {
            if (((MPlayer)player.GetModPlayer(Mod, "MPlayer")).chargeBangle)
            {
                maxCharges = 3;
            }
            if (charges < maxCharges)
            {
                rechargeCount++;
                if (((MPlayer)player.GetModPlayer(Mod, "MPlayer")).embellishedRegen && Main.rand.Next(0, 2) == 0)
                {
                    rechargeCount++;
                }
                if (rechargeCount >= rechargeTime)
                {
                    charges++;//consider combat text or other alert
                    rechargeCount = 0;
                }
            }
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("RubyTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("FireCrackerRing"), 1);
            recipe2.AddIngredient(mod.ItemType("RubyTicket"), 2);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }*/
    }
}
