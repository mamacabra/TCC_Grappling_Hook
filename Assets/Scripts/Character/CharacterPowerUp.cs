using System.Collections.Generic;
using Character.Utils;
using Const;
using PowerUp;
using Scene;
using UnityEngine;

namespace Character
{
    public class CharacterPowerUp : ACharacterMonoBehaviour
    {
        private const int MaxPowerUps = 3;
        private readonly List<PowerUpVariants> PowerUps = new();

        private void CatchPowerUp()
        {
            var newPowerUp = PowerUpManager.Catch(PowerUps);
            AddPowerUp(newPowerUp);
        }

        private void AddPowerUp(PowerUpVariants powerUp)
        {
            if (PowerUps.Count >= MaxPowerUps)
            {
                var randomPowerUp = PowerUps[Random.Range(0, PowerUps.Count)];
                PowerUps.Remove(randomPowerUp);
            }

            PowerUps.Add(powerUp);
            CharacterEntity.CharacterUI.UpdatePowerUpsUI(PowerUps);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PowerUp) == false) return;

            CatchPowerUp();
            Destroy(other.gameObject);
        }
    }
}
