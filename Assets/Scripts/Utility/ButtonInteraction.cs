using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject hoverIndicator;

    private void Start()
    {
        hoverIndicator.SetActive(false);
    }

    private void OnMouseOver()
    {
        hoverIndicator.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverIndicator.SetActive(false);
    }
}
