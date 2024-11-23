using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverOver : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Color normalColor;

    [SerializeField] Color hoverColor;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TMP_Text>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<TMP_Text>().color = normalColor;
    }
    

    
}
