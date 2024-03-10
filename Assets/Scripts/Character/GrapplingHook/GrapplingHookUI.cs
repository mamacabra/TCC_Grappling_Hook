using UnityEngine;
using UnityEngine.UI;

namespace Character.GrapplingHook
{
    public class GrapplingHookUI : MonoBehaviour
    {
        public Text forceText;

        public void UpdateForceUI(int force)
        {
            if (forceText) forceText.text = force.ToString();
        }
    }
}
