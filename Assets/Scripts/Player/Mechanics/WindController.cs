using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    [Header("Mechanic Variables")]
    public GameObject windCursor;
    public bool allowWind = true;
    public float windCooldownTime = 1f;
    private float timer = 0f;
    private Vector3 mousePosition;

    private void Update()
    {
        WindMechanic();
        WindCooldown();
    }

    private void WindMechanic()
    {
        if (Input.GetMouseButton(0) && allowWind)
        {
            WindEnabled();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            allowWind = false;
        }
        else
        {
            WindDisabled();
        }
    }

    public void WindEnabled()
    {
        windCursor.GetComponent<SpriteRenderer>().enabled = true;
        windCursor.GetComponent<CircleCollider2D>().enabled = true;
        windCursor.GetComponent<TrailRenderer>().enabled = true;

        GameManager.Instance.windIconCooldown.enabled = false;
    }

    public void WindDisabled()
    {
        windCursor.GetComponent<SpriteRenderer>().enabled = false;
        windCursor.GetComponent<CircleCollider2D>().enabled = false;
        windCursor.GetComponent<TrailRenderer>().enabled = false;
    }

    public void WindCooldown()
    {
        if (!allowWind)
        {
            timer += 1 * Time.deltaTime;

            GameManager.Instance.windIconCooldown.enabled = true;
            GameManager.Instance.windIconCooldown.fillAmount = timer;

            if (timer >= windCooldownTime)
            {
                timer = 0;
                allowWind = true;
            }
        }
    }
}
