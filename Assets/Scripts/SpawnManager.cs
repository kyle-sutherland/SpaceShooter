using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float enemySpawnTimer = 5f;
    [SerializeField]
    private float powerupTimerLow = 6f;
    [SerializeField]
    private float powerupTimerHi = 10f;
    [SerializeField]
    private GameObject _spawnEnemy;
    [SerializeField]
    private GameObject _spawnPowerup;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupContainer;

    private bool stopSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EnemySpawnRoutine");
        StartCoroutine("PowerupSpawnRoutine");
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (stopSpawn == false)
        {
            GameObject newSpawn = Instantiate(_spawnEnemy, SetSpawnPosition(), Quaternion.identity);
            if (newSpawn.tag == "Enemy")
            {
                newSpawn.transform.parent = _enemyContainer.transform;
                
            }
            yield return new WaitForSeconds(enemySpawnTimer);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (!stopSpawn)
        {
            GameObject newSpawn = Instantiate(_spawnPowerup, SetSpawnPosition(), Quaternion.identity);
            if (newSpawn.tag == "Powerup")
            {
                newSpawn.transform.parent = _powerupContainer.transform;
            }
            yield return new WaitForSeconds(Random.Range(powerupTimerLow, powerupTimerHi));
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawn = true;
    }

    public Vector3 SetSpawnPosition()
    {
        return new Vector3(Random.Range(-9.5f, 9.5f), 8, 0);
    }
}
