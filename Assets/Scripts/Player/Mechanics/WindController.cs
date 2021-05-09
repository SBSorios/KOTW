using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    [Header("Mechanic Variables")]
    public GameObject windBrush;
    public bool allowWind = true;
    public bool canCast = true;
    public float windCooldownTime = 1f;
    private float timer = 0f;

    private void Update()
    {
        WindMechanic();
        WindCooldown();
    }

    private void WindMechanic()
    {
        if (canCast)
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
    }

    public void WindEnabled()
    {
        windBrush.GetComponent<SpriteRenderer>().enabled = true;
        windBrush.GetComponent<CircleCollider2D>().enabled = true;
        windBrush.GetComponent<TrailRenderer>().enabled = true;

        UIManager.Instance.windIconCooldown.enabled = false;
    }

    public void WindDisabled()
    {
        windBrush.GetComponent<SpriteRenderer>().enabled = false;
        windBrush.GetComponent<CircleCollider2D>().enabled = false;
        windBrush.GetComponent<TrailRenderer>().enabled = false;
    }

    public void WindCooldown()
    {
        if (!allowWind)
        {
            timer += 1 * Time.deltaTime;

            UIManager.Instance.windIconCooldown.enabled = true;
            UIManager.Instance.windIconCooldown.fillAmount = timer;

            if (timer >= windCooldownTime)
            {
                timer = 0;
                allowWind = true;
            }
        }
    }
}
