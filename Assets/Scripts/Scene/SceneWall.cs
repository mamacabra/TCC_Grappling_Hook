using System;
using UnityEngine;

namespace SceneControll
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
