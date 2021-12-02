using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    [SerializeField]
    private float _speed = 6.5f;

    private Vector3 spawnPosition;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("_spawnManager is NULL");
        }

        spawnPosition = _spawnManager.SetSpawnPosition();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -8f)
        {
            Spawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("enemy hit Player");
            
            Player player = other.transform.GetComponent<Player>();
            
            if(player != null)
            {
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }
            
        if(other.tag == "Laser")
        {
            Debug.Log("enemy hit Laser");
            
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }       
    }

    public void Spawn()
    {
        transform.position = spawnPosition;
        spawnPosition = _spawnManager.SetSpawnPosition();
    }
}
