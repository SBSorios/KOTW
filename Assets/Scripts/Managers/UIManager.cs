using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Instance
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    #endregion

    [Header("Main")]
    public Canvas mainCanvas;

    [Header("In Game References")]
    public GameObject inGameObjects;
    public Image windIconCooldown;

    public void FixedUpdate()
    {
        if (SaveManager.Instance.inGame)
        {
            CursorCooldown();
        }
    }

    public void LoadedNewScene()
    {
        mainCanvas = FindObjectOfType<Canvas>();
        mainCanvas.worldCamera = Camera.main;
    }

    public void LoadedInMenus()
    {
        inGameObjects.SetActive(false);
    }

    public void LoadedInGame()
    {
        inGameObjects.SetActive(true);

        if (windIconCooldown != null)
        {
            windIconCooldown.fillAmount = GameManager.Instance.wc.windCooldownTime;
        }
    }

    private void CursorCooldown()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCanvas.transform as RectTransform,
            Input.mousePosition, mainCanvas.worldCamera,
            out movePos);

        Vector3 mousePos = mainCanvas.transform.TransformPoint(movePos);

        windIconCooldown.transform.position = mousePos;

        transform.position = mousePos;
    }
}
