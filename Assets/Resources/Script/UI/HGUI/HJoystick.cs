using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HJoystick : ScrollRect, IPointerDownHandler, IPointerUpHandler
{
    private List<HJoystick_Event> m_events;

    private RectTransform m_background;


    protected override void Start()
    {
        base.Start();
        m_background = (RectTransform)transform;
        m_events = GetComponents<HJoystick_Event>().ToList<HJoystick_Event>();
    }

    private void Update()
    {
        content.anchoredPosition = Vector2.ClampMagnitude(content.anchoredPosition, m_background.rect.width / 2);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (var item in m_events)
        {
            item.OnPointerDown();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var item in m_events)
        {
            item.OnPointerUp();
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        foreach (var item in m_events)
        {
            item.OnDrag();
        }
    }
}

public class HJoystick_Event : MonoBehaviour
{
    public virtual void OnPointerDown() { }
    public virtual void OnDrag() { }
    public virtual void OnPointerUp() { }
}
