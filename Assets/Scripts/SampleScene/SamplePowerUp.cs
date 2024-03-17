using UnityEngine;
using UnityEngine.SceneManagement;

namespace SampleScene
{
    public class SamplePowerUp : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
