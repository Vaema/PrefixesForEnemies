using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items
{
    public class RitualDagger : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 11;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 3;
            //item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Blood: Ritual Dagger");
      // Tooltip.SetDefault("Bleed yourself to fill your well");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Projectile p = null;
            int b = player.FindBuffIndex(Mod.Find<ModBuff>("BloodWell").Type);
            for (int i = 999; i >= 0; i--)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == Mod.Find<ModProjectile>("BloodWell").Type)
                {
                    p = Main.projectile[i];
                    break;
                }
            }
            if (p == null || b == -1 || player.FindBuffIndex(BuffID.Bleeding)!=-1)
            {
                return false;
            }
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(player.position, player.width, player.height, 5, 0f, 0f, 0, default(Color), 1.5f);
            }
            int dam = (int)(player.statLife * .25);
            player.Hurt(new Terraria.DataStructures.PlayerDeathReason(), dam, 0);
            player.AddBuff(BuffID.Bleeding, 1200);
            player.buffTime[b] += dam*60;
            return true;
        }
    }
}
