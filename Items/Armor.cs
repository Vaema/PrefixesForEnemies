using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items
{
    public class Armor : ModItem
    {
        public override void SetDefaults()
        {

            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
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
      // DisplayName.SetDefault("Blood: Armor");
      // Tooltip.SetDefault("Protect yourself with from your Well");
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
            if (p == null || b == -1)
            {
                return false;
            }
            if (player.buffTime[b] <= 241)
            {
                return false;
            }
            player.buffTime[b] /= 2;
            player.AddBuff(Mod.Find<ModBuff>("BloodArmor").Type, 1200);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.bloodArmor = player.buffTime[b] / 100;
            return true;
        }
    }
}
