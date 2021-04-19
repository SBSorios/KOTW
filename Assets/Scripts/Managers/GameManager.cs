using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Instance
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    [Header("UI Variables")]
    public Canvas parentCanvas;
    public Image windIconCooldown;
    public Camera mainCamera;
    public Vector3 mousePosition;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerController pc;
    [HideInInspector]
    public PlayerHealth ph;
    [HideInInspector]
    public WindController wc;

    [Header("Gameplay Variables")]
    public Transform startPOS;
    public Transform curCheckpoint;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            wc = player.GetComponent<WindController>();
            pc = player.GetComponent<PlayerController>();
            ph = player.GetComponent<PlayerHealth>();
            PlayerStart();
        }

        if (windIconCooldown != null)
        {
            windIconCooldown.fillAmount = wc.windCooldownTime;
        }
    }

    public void FixedUpdate()
    {
        CursorCooldown();
    }

    public void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void PlayerStart()
    {
        player.transform.position = startPOS.position;
    }

    public void CheckpointReset()
    {
        player.transform.position = curCheckpoint.position;
    }

    private void CursorCooldown()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        Vector3 mousePos = parentCanvas.transform.TransformPoint(movePos);

        windIconCooldown.transform.position = mousePos;

        transform.position = mousePos;
    }
}
