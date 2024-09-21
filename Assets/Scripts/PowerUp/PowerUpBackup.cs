using System.Collections.Generic;
using Const;

namespace PowerUp
{
    public struct PowerUpBackup
    {
        public int CharacterId;
        public List<PowerUpVariants> PowerUps;
        public List<APowerUp> PowerUpInstances;
    }
}
