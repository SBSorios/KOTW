using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject brokenPieces;
    public GameObject collectible;
    public GameObject collectibleLight;
    public bool spawnCollectible;

    private void Update()
    {
        if (spawnCollectible)
        {
            collectibleLight.SetActive(true);
        }
        else
        {
            collectibleLight.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            Break();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameManager.Instance.player.GetComponent<Collider2D>());
        }
    }

    public void Break()
    {
        Debug.Log("Break Vase");
        Destroy(this.gameObject);
        GameObject brokenVase = Instantiate(brokenPieces, transform.position, Quaternion.identity);

        foreach(Transform piece in brokenVase.transform)
        {
            piece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(2f, 7f));
        }

        if (spawnCollectible)
        {
            Instantiate(collectible, transform.position, Quaternion.identity);
            collectibleLight.SetActive(false);
        }
    }
}
