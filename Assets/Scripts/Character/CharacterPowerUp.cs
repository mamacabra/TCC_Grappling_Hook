using System.Collections.Generic;
using Character.Utils;
using Const;
using PowerUp;
using PowerUp.Variants;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class CharacterPowerUp : ACharacterMonoBehaviour
    {
        private const int MaxPowerUps = 3;
        public List<PowerUpVariants> PowerUps { get; private set; } = new();
        public List<APowerUp> PowerUpInstances { get; private set; } = new();

        public void Start()
        {
            var backup = PowerUpManager.GetPowerUpBackup(CharacterEntity.Character.Id);
            if (backup.PowerUps?.Count > 0)
            {
                PowerUps = backup.PowerUps;
            }

            CharacterEntity.Character.ToggleShield(false);
            CharacterEntity.CharacterUI.UpdatePowerUpsUI(PowerUps);
            for (var index = 0; index < PowerUps.Count; index++)
            {
                var powerUp = PowerUps[index];
                switch (powerUp)
                {
                    case PowerUpVariants.CharacterShieldPowerUp:
                        CharacterEntity.Character.ToggleShield(true);
                        PowerUpInstances.Add(new CharacterShieldPowerUp(CharacterEntity));
                        break;
                    case PowerUpVariants.CharacterSpeedBoostPowerUp:
                        CharacterEntity.Character.ToggleSpeedBoost(true);
                        PowerUpInstances.Add(new CharacterSpeedBoostPowerUp(CharacterEntity));
                        break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PowerUp) == false) return;

            CatchPowerUp();
            Destroy(other.gameObject);
        }

        private void AddPowerUp(PowerUpVariants powerUp)
        {
            if (PowerUps.Count >= MaxPowerUps)
            {
                var randomPowerUp = PowerUps[Random.Range(0, PowerUps.Count)];
                PowerUps.Remove(randomPowerUp);
            }

            PowerUps.Add(powerUp);
        }

        private void AddPowerUpInstance(PowerUpVariants powerUpVariant)
        {
            APowerUp instance = powerUpVariant switch
            {
                PowerUpVariants.CharacterShieldPowerUp => new CharacterShieldPowerUp(CharacterEntity),
                PowerUpVariants.CharacterSpeedBoostPowerUp => new CharacterSpeedBoostPowerUp(CharacterEntity),
                _ => null
            };

            PowerUpInstances.Add(instance);
        }

        private void CatchPowerUp()
        {
            var newPowerUp = PowerUpManager.Catch(PowerUps);

            if (newPowerUp == null) return;

            AddPowerUp((PowerUpVariants)newPowerUp);
            AddPowerUpInstance((PowerUpVariants)newPowerUp);
            CharacterEntity.CharacterUI.UpdatePowerUpsUI(PowerUps);

            foreach (var powerUpInstance in PowerUpInstances)
                powerUpInstance.OnCatch();
        }

        public void DropPowerUp(PowerUpVariants powerUpVariant)
        {
            if (PowerUps.Count > 0)
            {
                for (var i = 0; i < PowerUps.Count; i++)
                {
                    if (PowerUps[i] == powerUpVariant)
                    {
                        PowerUps.Remove(PowerUps[i]);
                        PowerUpInstances[i].OnDrop();
                        PowerUpInstances.Remove(PowerUpInstances[i]);
                    }
                }
            }

            CharacterEntity.CharacterUI.UpdatePowerUpsUI(PowerUps);
        }
    }
}
