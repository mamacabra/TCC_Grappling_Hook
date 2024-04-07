using System;
using UnityEngine;

namespace Scene
{
    public class SceneWall : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.tag = Const.Tags.Wall;
        }

        private void Start()
        {
            Destroy(this);
        }
    }
}
