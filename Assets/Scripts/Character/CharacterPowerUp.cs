using Character.Utils;
using Scene;
using UnityEngine;

namespace Character
{
    public class CharacterPowerUp : ACharacterMonoBehaviour
    {
        [SerializeField] private string[] powerUps = new string[3];

        private void AddPowerUp(string powerUp)
        {
            for (var i = 0; i < powerUps.Length; i++)
            {
                if (powerUps[i] != "") continue;

                powerUps[i] = powerUp;
                break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Const.Tags.PowerUp)) return;

            var powerUp = other.GetComponent<ScenePowerUpItem>();
            if (powerUp == null) return;

            var powerUpName = powerUp.Get();
            AddPowerUp(powerUpName);
            Destroy(other.gameObject);
        }
    }
}
