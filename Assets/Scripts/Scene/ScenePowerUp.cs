using UnityEngine;

namespace Scene
{
    public class ScenePowerUp : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.tag = Const.Tags.PowerUp;
        }
    }
}
