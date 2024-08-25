using System.Collections.Generic;
using Const;
using Enumerable = System.Linq.Enumerable;
using Random = UnityEngine.Random;

namespace PowerUp
{
    public static class PowerUpManager
    {
        private static readonly PowerUpVariants[] AvailablePowerUps =
        {
            PowerUpVariants.CharacterShieldPowerUp,
            PowerUpVariants.CharacterSpeedBoostPowerUp,
            PowerUpVariants.CharacterUntouchablePowerUp,
            PowerUpVariants.HookPathFirePowerUp,
            PowerUpVariants.HookPathIcePowerUp,
            PowerUpVariants.HookSplitPowerUp,
            PowerUpVariants.HookTriplePowerUp,
            PowerUpVariants.HookUntouchablePowerUp,
        };

        public static PowerUpVariants Catch(List<PowerUpVariants> characterPowerUps)
        {
            var filtered = Enumerable.ToList(AvailablePowerUps);
            filtered.RemoveAll(characterPowerUps.Contains);
            return filtered[Random.Range(0, filtered.Count)];
        }
    }
}
