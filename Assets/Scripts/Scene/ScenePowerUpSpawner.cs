using UnityEngine;

namespace SceneControll
{
    public class ScenePowerUpSpawner : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.PowerUpSpawn);
        }
        private void Awake()
        {
            gameObject.tag = Const.Tags.PowerUpSpawner;
        }
    }
}
