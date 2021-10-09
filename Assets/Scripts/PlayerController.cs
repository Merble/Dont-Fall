using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private GameObject focalPoint;
    [SerializeField] private GameObject powerUpIndicator;

    [SerializeField] private int powerUpDuration = 7;

    private float forwardInput;
    [SerializeField] private float speed = 5;
    [SerializeField] private float powerUpForce = 10;

    [SerializeField] private bool hasPowerUp = false;

    void Start()
    {
        
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);

            hasPowerUp = true;
            powerUpIndicator.SetActive(true);

            StartCoroutine(PowerUpCoundtdown());
            StopCoroutine(PowerUpCoundtdown());
        }
    }

    IEnumerator PowerUpCoundtdown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 forceAway = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(forceAway * powerUpForce, ForceMode.Impulse);
        }
    }
}
