using Character.Utils;
using PowerUp;
using UnityEngine;

namespace Character
{
    public class CharacterPowerUp : ACharacterMonoBehaviour
    {
        [SerializeField] private string[] powerUps = new string[3];

        public void AddPowerUp(string powerUp)
        {
            for (var i = 0; i < powerUps.Length; i++)
            {
                if (powerUps[i] != "") continue;

                Debug.Log($"Character {gameObject.name} picked up {powerUp}");
                powerUps[i] = powerUp;
                break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Const.Tags.PowerUp)) return;

            var powerUp = other.GetComponent<PowerUpItem>();
            if (powerUp == null) return;

            var powerUpName = powerUp.Get();
            AddPowerUp(powerUpName);
            // Debug.Log($"Character {gameObject.name} picked up {powerUpName}");
            Destroy(other.gameObject);
        }
    }
}
