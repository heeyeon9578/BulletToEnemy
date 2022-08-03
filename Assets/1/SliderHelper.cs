using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderHelper : Slider
{
    [SerializeField]
    private UnityEvent<float> ev;
    Character char1;
    public float waitTime = 30.0f;
    public  float value1;

    void Start()
    {
        char1 = GameObject.FindWithTag("Character").GetComponent<Character>();

        //ev.AddListener(); 스크립트로 구현
        Debug.Log(char1);
        ev.AddListener(char1.Shoot);


    }

    public override void OnPointerUp(PointerEventData p)
    {

        ev.Invoke(this.value);
        // ev을 Invoke 해주세요.
        
        Debug.Log(p);
        
        value1 = value;
    }


}
