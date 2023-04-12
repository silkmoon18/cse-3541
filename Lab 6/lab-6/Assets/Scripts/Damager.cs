using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float damage;

    private List<GameObject> damageableList;

    private void Awake()
    {
        damageableList = new List<GameObject>();
    }

    public void Attack()
    {
        foreach (GameObject obj in damageableList)
        {
            obj.GetComponent<Damageable>().GetDamaged(damage, transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Damageable>() != null && !damageableList.Contains(other.gameObject))
            damageableList.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (damageableList.Contains(other.gameObject))
            damageableList.Remove(other.gameObject);
    }
}
