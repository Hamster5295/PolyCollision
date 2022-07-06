using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    public RectTransform stickBackground, stickHandle;
    public float maxDistance, maxMagnitude;

    private Transform playerTransform;
    private Rigidbody2D playerRb;
    private Animation ani_background, ani_handle;
    private Image img_background, img_handle;
    private GameUnit playerUnit;

    private Vector2 currentPos;
    private bool underControl = false;
    private float factor = 1;

    private void Start()
    {
        playerTransform = player.transform;
        playerRb = player.GetComponent<Rigidbody2D>();

        ani_background = stickBackground.GetComponent<Animation>();
        ani_handle = stickHandle.GetComponent<Animation>();

        img_background = stickBackground.GetComponent<Image>();
        img_handle = stickHandle.GetComponent<Image>();

        playerUnit = player.GetComponent<GameUnit>();

        factor = 1920f / Screen.width;
    }

    Vector2 value;
    private void Update()
    {
        // var p = (Vector2)stickHandle.position;
        // p.x /= factor; p.y /= factor;

        value = Vector2.ClampMagnitude((Vector2)stickHandle.position - currentPos, maxMagnitude);
        if (underControl)
        {
            playerRb.AddForce(value * Time.deltaTime * playerUnit.speed);
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, value)), 0.05f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Application.platform != RuntimePlatform.Android) return;
        currentPos = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentPos = eventData.position;

        // var p = currentPos;
        // p.x *= factorX; p.y *= factorY;
        // Debug.Log(currentPos);
        // Debug.Log(p);
        stickBackground.position = currentPos;
        stickHandle.position = currentPos;
        ColorUtil.SetColorAlpha(img_background, 1);
        ColorUtil.SetColorAlpha(img_handle, 1);

        ani_background.Stop();
        ani_handle.Stop();
        underControl = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ani_background.Play("Image_FadeOut");
        ani_handle.Play("Image_FadeOut");
        underControl = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // var p = currentPos;
        // p.x *= factorX; p.y *= factorY;
        stickHandle.position = Vector2.ClampMagnitude(eventData.position - currentPos, maxDistance / factor) + currentPos;
    }
}
