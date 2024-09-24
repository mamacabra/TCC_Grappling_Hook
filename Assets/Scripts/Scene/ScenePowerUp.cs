using UnityEngine;

namespace Scene
{
    public class ScenePowerUp : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.tag = Const.Tags.PowerUp;
        }
        public void OnTriggerEnter(Collider other)
        {
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.PowerUpPickUp);
        }
    }
}
