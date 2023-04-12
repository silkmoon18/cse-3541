using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damageForce;
    [SerializeField] private float destoryDelay = 10f;
    [SerializeField] private GameObject wood;

    private bool alive = true;

    private Vector3 damageDirection;

    private void Update()
    {
        if (health <= 0 && alive)
        {
            Die();
        }

    }
    public void GetDamaged(float damage, Vector3 damagePosition)
    {
        health -= damage;
        damageDirection = transform.position - damagePosition;
    }

    private void Die()
    {
        alive = false;

        // only for trees
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().AddForce(damageDirection * damageForce);
        StartCoroutine(DelayedDestroy(destoryDelay));
    }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(wood, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
