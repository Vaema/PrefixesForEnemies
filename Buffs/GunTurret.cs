using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class GunTurret : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gun Turret");
            // Description.SetDefault("");//add this
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("GunTurret").Type] > 0)
            {
                modPlayer.gunTurret = true;
            }
            if (!modPlayer.gunTurret)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}