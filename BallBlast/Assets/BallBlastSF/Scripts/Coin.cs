using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float fallspeed;
    [SerializeField] private float minY;

    private void Update()
    {
        if (transform.position.y > minY)
        {
            transform.position += Vector3.down * fallspeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cart cart = collision.GetComponent<Cart>();

        if (cart != null)
        {
            cart.CollectCoin();
            Destroy(gameObject);
        }

    }

}
