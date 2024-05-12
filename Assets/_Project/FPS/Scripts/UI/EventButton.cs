using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.FPS.UI
{
    public class EventButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool buttonPressed = false;
        public bool Pressed => buttonPressed;

public void OnPointerDown(PointerEventData eventData)
{
    buttonPressed = true;
}

public void OnPointerUp(PointerEventData eventData)
{
    buttonPressed = false;
}
}
}
