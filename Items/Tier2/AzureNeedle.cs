using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier2
{
    public class AzureNeedle : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 25;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 28;
            Item.height = 28;

            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = 3;
            Item.knockBack = .5f;
            Item.value = 70;
            Item.rare = 3;
            Item.UseSound = SoundID.Item7;
            Item.consumable = true;
            Item.shoot = Mod.Find<ModProjectile>("AzureNeedle").Type;
            Item.shootSpeed = 12f;
            Item.maxStack = 999;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Azure Needle");
      // Tooltip.SetDefault("Right-click to prepare multiple needles");
    }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            GItem info = Item.GetGlobalItem<GItem>();
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            Item.useTime = 13;
            Item.useAnimation = 13;
            if (info.numNeedles <= 1 && player.altFunctionUse != 2)
            {
                return true;
            }
            else
            {
                play.typeNeedles = type;
                play.needleTime = info.numNeedles * 3;
                play.needleDamage = damage;
                player.itemAnimation = info.numNeedles * 3;
                for (int i = 0; i < info.numNeedles - 1; i++)
                {
                    if ((player.ThrownCost33 && Main.rand.Next(100) < 33) || (player.ThrownCost50 && Main.rand.Next(2) == 0))
                    {

                    }
                    else
                    {
                        Item.stack--;
                    }
                }
                info.numNeedles = 0;
                info.timeToNeedle = 0;
                return false;
            }
            //if channeled, shoot X needles
        }
        public override bool ConsumeItem(Player player)
        {
            GItem info = Item.GetGlobalItem<GItem>();
            if (player.altFunctionUse == 2 && info.numNeedles == 0)
            {
                return false;
            }
            return base.ConsumeItem(player);
        }
        public override bool CanUseItem(Player player)
        {
            GItem info = Item.GetGlobalItem<GItem>();
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (player.altFunctionUse == 2)
            {
                Item.UseSound = SoundID.Item32;
                if (info.timeToNeedle == 0)
                {
                    info.timeToNeedle = Item.useTime;
                }
                if (info.numNeedles < Math.Min(6, Item.stack))
                {
                    info.timeToNeedle--;
                }
                if (info.timeToNeedle == 1 && info.numNeedles < Math.Min(6, Item.stack))
                {
                    info.numNeedles++;
                    if (info.numNeedles != Math.Min(6, Item.stack))
                    {
                        SoundEngine.PlaySound(SoundID.Item17.WithVolumeScale(.3f), player.position);
                    }
                }
                if (info.numNeedles == Math.Min(6, Item.stack) && info.timeToNeedle == 1)
                {
                    //play sound
                    SoundEngine.PlaySound(SoundID.Unlock.WithVolumeScale(.3f), player.position);
                    info.timeToNeedle = Item.useTime;
                    Item.useTime = 18;
                    Item.useAnimation = 18;
                }
                return false;
            }
            Item.consumable = true;
            Item.UseSound = SoundID.Item7;
            return base.CanUseItem(player);
        }
        public override void UpdateInventory(Player player)
        {
            if (player.inventory[player.selectedItem] != Item)
            {
                GItem info = Item.GetGlobalItem<GItem>();
                info.numNeedles = 0;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200);
            recipe.AddIngredient(Mod.Find<ModItem>("TopazTicket").Type, 1);
            recipe.Register();
        }
    }
}
