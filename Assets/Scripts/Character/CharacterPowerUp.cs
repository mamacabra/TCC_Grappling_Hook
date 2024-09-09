using System.Collections.Generic;
using Character.Utils;
using Const;
using PowerUp;
using UnityEngine;

namespace Character
{
    public class CharacterPowerUp : ACharacterMonoBehaviour
    {
        private const int MaxPowerUps = 3;
        private readonly List<PowerUpVariants> powerUps = new();

        private void CatchPowerUp()
        {
            var newPowerUp = PowerUpManager.Catch(powerUps);
            if (newPowerUp != null) AddPowerUp((PowerUpVariants) newPowerUp);
        }

        private void AddPowerUp(PowerUpVariants powerUp)
        {
            if (powerUps.Count >= MaxPowerUps)
            {
                var randomPowerUp = powerUps[Random.Range(0, powerUps.Count)];
                powerUps.Remove(randomPowerUp);
            }

            powerUps.Add(powerUp);
            CharacterEntity.CharacterUI.UpdatePowerUpsUI(powerUps);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PowerUp) == false) return;

            CatchPowerUp();
            Destroy(other.gameObject);
        }
    }
}
