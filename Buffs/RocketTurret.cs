using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class RocketTurret : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rocket Turret");
            // Description.SetDefault("");//add this
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("RocketTurret").Type] > 0)
            {
                modPlayer.rocketTurret = true;
            }
            if (!modPlayer.rocketTurret)
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