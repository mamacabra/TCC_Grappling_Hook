using UnityEngine;
using Random = UnityEngine.Random;

namespace Scene
{
    public class ScenePowerUpItem : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.tag = Const.Tags.PowerUp;
        }

        public string Get()
        {
            var names = new[] {"DoubleJump", "SpeedBoost", "Shield", "Health", "Damage", "Invisibility", "Teleport", "TimeSlow", "TimeSpeed"};
            return names[Random.Range(0, names.Length)];
        }
    }
}
