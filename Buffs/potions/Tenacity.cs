using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class Tenacity : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tenacity Tonic");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            modPlayer.tenacity = true;
        }
    }
}
