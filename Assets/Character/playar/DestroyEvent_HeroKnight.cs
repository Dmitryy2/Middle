using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEvent_HeroKnight : MonoBehaviour
{
    // Уничтожьте частицы, когда анимация завершит воспроизведение. 
    // destroyEvent() вызывается как событие в анимации.
    public void destroyEvent()
    {
        Destroy(gameObject);
    }
}
