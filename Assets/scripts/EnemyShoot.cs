using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public SimpleShoot shooter;
    public float shootInterval = 2f;
    private float shootTimer = 0f;

    void Start()
    {
        if (shooter != null)
        {
            shooter.maxAmmo = 100;
        }
        else
        {
            Debug.LogWarning("Shooter is not assigned on enemy.");
        }
    }

    void Update()
    {
        if (Camera.main != null)
        {
            //This means enemy camera will follow the player position and will face the player.
            transform.forward = Vector3.ProjectOnPlane(Camera.main.transform.position - transform.position, Vector3.up).normalized;
        }

        
    }

    void Dead(Vector3 hitPoint)
    {
        //Basically disabling the animations since the enemy is dead.
        var animator = GetComponent<Animator>();
        if (animator != null) animator.enabled = false;

        SetupRagdoll(false);

        //This is for hitpoints, defines where we hit and how will it impact the enemy
        foreach (var item in Physics.OverlapSphere(hitPoint, 0.5f))
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(1000f, hitPoint, 0.5f);
            }
        }

        this.enabled = false;
    }

    void Shoot()
    {
        if (shooter != null) shooter.Shoot();
    }

    
    void SetupRagdoll(bool isAnimated)
    {
        //This gets all the components defined in the rig(head,hand etc...)
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies)
        {
            body.isKinematic = isAnimated;
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var col in colliders)
        {
            //compares the colliders with the animation
            //disables 
            col.enabled = !isAnimated;
        }

        Collider mainCol = GetComponent<Collider>();
        if (mainCol != null)
            //gets the ref of main collider and enables animation
            mainCol.enabled = isAnimated;

        Rigidbody mainRB = GetComponent<Rigidbody>();
        if (mainRB != null)
            mainRB.isKinematic = !isAnimated;
    }
}