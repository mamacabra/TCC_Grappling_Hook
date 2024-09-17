using System.Collections.Generic;
using Character.Utils;
using Const;
using PowerUp;
using PowerUp.Variants;
using UnityEngine;

namespace Character
{
    public class CharacterPowerUp : ACharacterMonoBehaviour
    {
        private const int MaxPowerUps = 3;
        private readonly List<PowerUpVariants> powerUps = new();
        private readonly List<APowerUp> powerUpInstances = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PowerUp) == false) return;

            CatchPowerUp();
            Destroy(other.gameObject);
        }

        private void AddPowerUp(PowerUpVariants powerUp)
        {
            if (powerUps.Count >= MaxPowerUps)
            {
                var randomPowerUp = powerUps[Random.Range(0, powerUps.Count)];
                powerUps.Remove(randomPowerUp);
            }

            powerUps.Add(powerUp);
        }

        public void DropPowerUp(PowerUpVariants powerUpVariant)
        {
            if (powerUpVariant is PowerUpVariants.CharacterShieldPowerUp)
            {
                powerUps.Remove(powerUpVariant);
                foreach (var powerUp in powerUpInstances)
                {
                    if (powerUp is CharacterShieldPowerUp)
                    {
                        powerUp.OnDrop();
                        powerUpInstances.Remove(powerUp);
                    }
                }
            }
        }

        private void AddPowerUpInstance(PowerUpVariants powerUpVariant)
        {
            APowerUp instance = null;
            if (powerUpVariant is PowerUpVariants.CharacterShieldPowerUp)
                instance = new CharacterShieldPowerUp(CharacterEntity);

            powerUpInstances.Add(instance);
        }

        private void CatchPowerUp()
        {
            var newPowerUp = PowerUpManager.Catch(powerUps);

            if (newPowerUp == null) return;

            AddPowerUp((PowerUpVariants)newPowerUp);
            AddPowerUpInstance((PowerUpVariants)newPowerUp);
            CharacterEntity.CharacterUI.UpdatePowerUpsUI(powerUps);

            foreach (var powerUpInstance in powerUpInstances)
                powerUpInstance.OnCatch();
        }
    }
}
