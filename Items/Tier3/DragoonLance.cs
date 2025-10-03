using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class DragoonLance : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;//check
            Item.knockBack = 3;
            Item.value = 30000;
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.shoot = 10;//spear proj
            Item.shootSpeed = 10f;//modify
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Dragoon Lance");
      // Tooltip.SetDefault("Right-click to enable Jump.");
    }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(Mod.Find<ModBuff>("Jump").Type, 600);
            }
            else if(player.FindBuffIndex(Mod.Find<ModBuff>("Jump").Type) != -1)
            {
                //initiate dive attack
                Item.shoot = 1;//change to downward falling lance
                player.jump = 0;//can't jump to stop
                player.wingTime = 0;//can't use wings to stop
                player.velocity.X = 0;
                player.velocity.Y -= 4;
            }
            return true;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            if (player.FindBuffIndex(Mod.Find<ModBuff>("Jump").Type) != -1)
            {
                damage += (int)(player.velocity.Y * Item.damage/4);
            }
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("SapphireTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("DragoonLance"), 1);
            recipe2.AddIngredient(mod.ItemType("SapphireTicket"), 2);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }*/
    }
}
