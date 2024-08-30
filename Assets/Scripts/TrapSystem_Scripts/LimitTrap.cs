using UnityEngine;

namespace TrapSystem_Scripts
{
    public class LimitTrap : MonoBehaviour
{
    public Transform fallingObjectPrefab;  // Prefab of the object that will fall
    public float minX = -25f;              // Minimum X boundary
    public float maxX = 25f;               // Maximum X boundary
    public float minZ = -25f;              // Minimum Z boundary
    public float maxZ = 25f;               // Maximum Z boundary
    public float fallHeight = 50f;         // Height from which the object will fall
    public float timeBeforeDeath = 3f;     // Time before the object falls

    private Transform player;
    private bool isOutsideArena = false;
    private float outsideTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Find player by tag
    }

    void Update()
    {
        // Check if player is outside the square arena
        if (player.position.x < minX || player.position.x > maxX || 
            player.position.z < minZ || player.position.z > maxZ)
        {
            if (!isOutsideArena)
            {
                isOutsideArena = true;
                outsideTime = Time.time;
            }
            else
            {
                // Check if time limit is reached
                if (Time.time - outsideTime >= timeBeforeDeath)
                {
                    SpawnFallingObject();
                    isOutsideArena = false;  // Reset
                }
            }
        }
        else
        {
            // Reset if player is back inside arena
            isOutsideArena = false;
        }
    }

    void SpawnFallingObject()
    {
        Vector3 spawnPosition = new Vector3(player.position.x, fallHeight, player.position.z);
        Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);
    }
}

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 20f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y <= 0) // Assuming ground level is at y = 0
        {
            Destroy(gameObject);  // Destroy the object if it reaches the ground
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Implement player death logic here
            Destroy(collision.gameObject);  // Example: destroy player on contact
        }
    }
}
}

