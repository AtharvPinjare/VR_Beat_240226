using UnityEngine;

public class Collision_Manager_bullet : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
            Debug.Log("Bullet Destroyed on Wall Collision");
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Debug.Log("Enemy mar gya.");
        }
    }
}
