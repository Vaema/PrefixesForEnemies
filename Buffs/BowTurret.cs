using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class BowTurret : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bow Turret");
            // Description.SetDefault("");//add this
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BowTurret").Type] > 0)
            {
                modPlayer.bowTurret = true;
            }
            if (!modPlayer.bowTurret)
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