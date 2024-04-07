using UnityEngine;

namespace Scene
{
    public class SceneObject : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.tag = Const.Tags.Object;
        }

        private void Start()
        {
            Destroy(this);
        }
    }
}
