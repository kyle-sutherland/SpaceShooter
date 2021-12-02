using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
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

            switch (powerupID)
            {
                case 0:
                    player.ActivateTripleShot(true);
                    Debug.Log("player collected TripleShot");
                    break;
                case 1:
                    player.ActivateSpeedBoost(true);
                    Debug.Log("player collected SpeedBoost");
                    break;
                case 2:
                    //player.ActivateShield(true);
                    Debug.Log("player Collected Shield");
                    break;
                default:
                    Debug.Log("player collected DefaultPowerup");
                    break;
            }

            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Debug.Log("powerup hit Laser");

            //Destroy(other.gameObject);
            //Destroy(this.gameObject);
        }
    }
}
