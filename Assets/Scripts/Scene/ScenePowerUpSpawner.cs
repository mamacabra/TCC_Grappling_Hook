using UnityEngine;

namespace Scene
{
    public class ScenePowerUpSpawner : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.tag = Const.Tags.PowerUpSpawner;
        }
    }
}
