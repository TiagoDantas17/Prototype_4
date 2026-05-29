using System.Collections;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    public float speed = 10.0f;
    public bool hasPowerup = false;
    public GameObject powerupIndicator;

    public float normalStrength = 10.0f;
    public float powerupStrength = 25.0f;
    private float powerupDuration = 5.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        if (powerupIndicator != null)
        {
            powerupIndicator.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (focalPoint != null)
        {
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed, ForceMode.Force);
        }
    }

    void Update()
    {
        if (powerupIndicator != null)
        {
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;

            if (powerupIndicator != null)
            {
                powerupIndicator.SetActive(true);
            }

            Destroy(other.gameObject);
            StartCoroutine(PowerupCooldown());
        }
    }

    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerupDuration);

        hasPowerup = false;

        if (powerupIndicator != null)
        {
            powerupIndicator.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            if (hasPowerup)
            {
                enemyRb.AddForce(awayFromPlayer.normalized * powerupStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRb.AddForce(awayFromPlayer.normalized * normalStrength, ForceMode.Impulse);
            }
        }
    }
}