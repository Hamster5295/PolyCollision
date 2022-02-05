using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animation))]
public class HButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public AnimationClip ani_BeginDrag, ani_EndDrag;

    private Animation m_ani;
    private List<HBtn_Event> events;


    private void Start()
    {
        m_ani = GetComponent<Animation>();
        events = GetComponents<HBtn_Event>().ToList<HBtn_Event>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_ani.Stop();
        m_ani.clip = ani_BeginDrag;
        m_ani.Play();

        foreach (var item in events)
        {
            item.OnPointerDown();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_ani.Stop();
        m_ani.clip = ani_EndDrag;
        m_ani.Play();

        foreach (var item in events)
        {
            item.OnPointerUp();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var item in events)
        {
            item.OnClick();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_ani.Stop();
        m_ani.clip = ani_BeginDrag;
        m_ani.Play();

        foreach (var item in events)
        {
            item.OnPointerDown();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_ani.Stop();
        m_ani.clip = ani_EndDrag;
        m_ani.Play();

        foreach (var item in events)
        {
            item.OnPointerUp();
        }
    }
}

public class HBtn_Event : MonoBehaviour
{
    public virtual void OnClick() { }

    public virtual void OnPointerDown() { }
    public virtual void OnPointerUp() { }
}
