using System;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class BloodWell : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Well");
            // Description.SetDefault("");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BloodWell").Type] > 0)
            {
                modPlayer.bloodWell = true;
            }
            if (!modPlayer.bloodWell)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = Math.Max(2, player.buffTime[buffIndex]);
                player.buffTime[buffIndex] = Math.Min(10800, player.buffTime[buffIndex]);
            }
        }
    }
}