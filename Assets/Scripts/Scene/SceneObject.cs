using UnityEngine;

namespace SceneControll
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
