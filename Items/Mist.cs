using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class Mist : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 15;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 3;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Blood: Mist");
      // Tooltip.SetDefault("Your Well emits a pulse of fine blood mist");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Projectile p = null;
            int b = player.FindBuffIndex(Mod.Find<ModBuff>("BloodWell").Type);
            for (int j = 0; j < 1000; j++)
            {
                if (Main.projectile[j].owner == player.whoAmI && Main.projectile[j].type == Mod.Find<ModProjectile>("BloodWell").Type)
                {
                    p = Main.projectile[j];
                    break;
                }
            }
            if (p == null || b == -1)
            {
                return false;
            }
            if (player.buffTime[b] <= 121)
            {
                return false;
            }
            player.buffTime[b] -= 120;
            float spread = 360f * 0.0174f;
            float baseSpeed = 2.5f;
            double startAngle = spread / 2;
            double deltaAngle = spread / 30f;
            double offsetAngle;
            int i;
            for (i = 0; i < 30; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(p.Center.X, p.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("BloodMist").Type, (int)(Item.damage * player.GetDamage(DamageClass.Magic)), Item.knockBack, player.whoAmI);
            }
            return true;
        }
    }
}
