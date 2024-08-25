using UnityEngine;

namespace Scene
{
    public class ScenePowerUpItem : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.tag = Const.Tags.PowerUp;
        }
    }
}
