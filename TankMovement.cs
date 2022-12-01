using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed;
    public float maxForwardSpeed;
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float turnSpeed;
    public float projectileForce;
    public GameObject projectile;
    public GameObject projectileSpawnPoint;
    public Rigidbody rb;
    public Transform tankTransform;

    public float fireRate;
    float fireRateReset;
    public float firingRate;
    public float firingRateReset;
    public bool firing;

    public bool die = false;

    public int ammo;

    private void Start()
    {
        fireRateReset = fireRate;
        firingRateReset = firingRate;
    }

    void Update()
    {
        if(fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
        else if(fireRate <= 0)
        {
            fireRate = 0;
        }

        if(firingRate > 0)
        {
            firingRate -= Time.deltaTime;
        }
        else if(firingRate <= 0)
        {
            firing = false;
            firingRate = 0;
        }

        if(die)
        {
            KillTank();
        }

        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed),
            Mathf.Clamp(rb.velocity.y, -maxVerticalSpeed, maxVerticalSpeed), Mathf.Clamp(rb.velocity.z, -maxForwardSpeed, maxForwardSpeed));

        float Vertical = Input.GetAxis("Vertical");
        bool isDriving = Input.GetButton("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        rb.AddRelativeForce(0, 0, (speed * Time.deltaTime) * Vertical);
        tankTransform.Rotate(0, (turnSpeed * Time.deltaTime) * Horizontal, 0);

        //Tank Animation Handler basically just controls the Animator Component of the tank.
        TankAnimationHandler.DriveAnimation(isDriving);
        TankAnimationHandler.GunAnimation(firing);

        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire") && fireRate == 0 && ammo > 0 && die == false)
        {
            if (firingRate <= 0)
            {
                firing = true;
                firingRate = firingRateReset;
            }

            GameObject tempBullet = Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
            tempBullet.transform.localRotation = this.transform.rotation;
            Rigidbody tempRb = tempBullet.GetComponent<Rigidbody>();

            tempRb.AddForce(projectileSpawnPoint.transform.forward * projectileForce);
            fireRate = fireRateReset;
            ammo--;

            Destroy(tempBullet, 15f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("Bullet Collided");
            die = true;
        }
        else
        {
            return;
        }
    }

    public void KillTank()
    {
        //This doesn't do anything yet, but it will have like explosions and things.
    }
}
