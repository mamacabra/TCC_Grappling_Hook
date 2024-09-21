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
            if (powerUpVariant is not PowerUpVariants.CharacterShieldPowerUp) return;

            if (powerUps.Count > 0)
            {
                for (var i = 0; i < powerUps.Count; i++)
                {
                    if (powerUps[i] == powerUpVariant)
                    {
                        powerUps.Remove(powerUps[i]);
                    }
                }
            }

            if (powerUpInstances.Count > 0)
            {
                for (var i = 0; i < powerUpInstances.Count; i++)
                {
                    if (powerUpInstances[i] is CharacterShieldPowerUp)
                    {
                        powerUpInstances[i].OnDrop();
                        powerUpInstances.Remove(powerUpInstances[i]);
                    }
                }
            }

            CharacterEntity.CharacterUI.UpdatePowerUpsUI(powerUps);
        }

        private void AddPowerUpInstance(PowerUpVariants powerUpVariant)
        {
            APowerUp instance = powerUpVariant switch
            {
                PowerUpVariants.CharacterShieldPowerUp => new CharacterShieldPowerUp(CharacterEntity),
                PowerUpVariants.CharacterSpeedBoostPowerUp => new CharacterSpeedBoostPowerUp(CharacterEntity),
                _ => null
            };

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
