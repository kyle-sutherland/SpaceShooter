using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("_spawnManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -9.1f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("powerup hit Player");

            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.ActivateTripleShot(true);
            }

            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Debug.Log("powerup hit Laser");

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }

   
}
