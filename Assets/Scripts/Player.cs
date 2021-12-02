using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    public float playerXConstraint = 10f;
    [SerializeField]
    public float playerYConstraint = 5.7f;
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private float tripleShotPowerDownTime = 5f;
    [SerializeField]
    private Vector3 _projectileSpawnPosition = new Vector3(0, 0.7f, 0);
    [SerializeField]
    private float _fireSpeed = 0.2f;
    private float _fireTime = -1f;
    [SerializeField]
    private int _lives = 3;
    
    //variable for is tripleshot active
    private bool tripleShotActive = false;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("_spawnManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _fireTime)
        {
            ShootLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= playerYConstraint)
        {
            transform.position = new Vector3(transform.position.x, playerYConstraint, 0);
        }
        else if (transform.position.y <= -1 * playerYConstraint)
        {
            transform.position = new Vector3(transform.position.x, -1 * playerYConstraint, 0);
        }

        if (transform.position.x >= playerXConstraint)
        {
            transform.position = new Vector3(-1 * playerXConstraint, transform.position.y, 0);
        }
        else if (transform.position.x <= -1 * playerXConstraint)
        {
            transform.position = new Vector3(playerXConstraint, transform.position.y, 0);
        }
    }

    void ShootLaser()
    {
        _fireTime = Time.time + _fireSpeed;
        if (tripleShotActive)
        {
            Instantiate(_tripleShot, transform.position + _projectileSpawnPosition, Quaternion.identity);
        }
        Instantiate(_projectile, transform.position + _projectileSpawnPosition, Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;
        
        if(_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot(bool a)
    {
        if (a)
        {
            tripleShotActive = true;
            StartCoroutine("TripleShotPowerDownRoutine");
        }
        else
        {
            tripleShotActive = false;
        }
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(tripleShotPowerDownTime);
        while(tripleShotActive)
        {
            tripleShotActive = false;
        }
    }

}