using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject brokenPieces;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            Break();
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
    }
}
