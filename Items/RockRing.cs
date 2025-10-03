using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class RockRing : ModItem
    {
        int maxCharges = 3;
        int rechargeTime = 600;
        int charges = 3;
        int rechargeCount = 0;

        //conditional charge modifiers go here

        public override void SetDefaults()
        {

            Item.damage = 50;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = 3;
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
            //item.shoot = mod.ProjectileType("Rock");
            Item.shootSpeed = 8f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Rock Ring");
      // Tooltip.SetDefault("Shoots a big rock. Three charges.");
    }

        public override bool CanUseItem(Player player)
        {
            if (charges <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X, player.Center.Y - 600, 0, 0, Mod.Find<ModProjectile>("Rock").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            charges--;
            return false;
        }
        public override void UpdateInventory(Player player)
        {
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
    }
}
