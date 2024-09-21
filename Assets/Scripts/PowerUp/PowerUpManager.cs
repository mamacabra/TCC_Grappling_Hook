using System.Collections.Generic;
using System.Linq;
using Const;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;
using Random = UnityEngine.Random;

namespace PowerUp
{
    public class PowerUpManager : MonoBehaviour
    {
        public static PowerUpManager Instance;

        [Header("Power Up")]
        [SerializeField] private GameObject powerUpItem;

        [Header("Spawn")]
        [SerializeField] private bool isSpawnEnable;
        [SerializeField] private List<GameObject> spawnPoints;

        private float spawnCountDown;
        private const float SpawnCountDownDefault = 5f;

        private static readonly PowerUpVariants[] AvailablePowerUps =
        {
            // PowerUpVariants.CharacterShieldPowerUp,
            PowerUpVariants.CharacterSpeedBoostPowerUp,
            // PowerUpVariants.CharacterUntouchablePowerUp,
            // PowerUpVariants.HookPathFirePowerUp,
            // PowerUpVariants.HookPathIcePowerUp,
            // PowerUpVariants.HookSplitPowerUp,
            // PowerUpVariants.HookTriplePowerUp,
            // PowerUpVariants.HookUntouchablePowerUp,
        };

        public void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        public void FixedUpdate()
        {
            if (isSpawnEnable == false) return;

            spawnCountDown -= Time.fixedDeltaTime;
            if (spawnCountDown <= 0)
            {
                SpawnPowerUp();
                ResetSpawnCountDown();
            }
        }

        private void SpawnPowerUp()
        {
            if (spawnPoints.Count <= 0) return;

            var index = Random.Range(0, spawnPoints.Count);
            var spawnPoint = spawnPoints[index];
            if (spawnPoint)
            {
                Instantiate(powerUpItem, spawnPoint.transform.position, Quaternion.identity);
                spawnPoints.Remove(spawnPoint);
                Destroy(spawnPoint);
            }
        }

        public void StartSpawn()
        {
            if (isSpawnEnable) return;

            isSpawnEnable = true;
            FindAndDestroyPowerUps();
            FindPowerUpsSpawnPoints();
            ResetSpawnCountDown();
        }

        public void StopSpawn()
        {
            isSpawnEnable = false;
        }

        private void ResetSpawnCountDown()
        {
            spawnCountDown = SpawnCountDownDefault;
            if (spawnPoints.Count == 0) StopSpawn();
        }

        private static void FindAndDestroyPowerUps()
        {
            var powerUps = GameObject.FindGameObjectsWithTag(Tags.PowerUp);
            foreach (var powerUp in powerUps) Destroy(powerUp);
        }

        private void FindPowerUpsSpawnPoints()
        {
            var gameObjects = GameObject.FindGameObjectsWithTag(Tags.PowerUpSpawner);
            spawnPoints = gameObjects.ToList();
        }

        public static PowerUpVariants? Catch(List<PowerUpVariants> characterPowerUps)
        {
            var filtered = Enumerable.ToList(AvailablePowerUps);
            filtered.RemoveAll(characterPowerUps.Contains);

            if (filtered.Count == 0) return null;
            return filtered[Random.Range(0, filtered.Count)];
        }
    }
}
