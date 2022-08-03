using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject bull;
    // 심화 과제용
    private Rigidbody2D rgbd;
    //총알 따라가는 카메라
    GameObject cam;
    //메인카메라
    GameObject maincamera;
    // hp바
    Image hp;
    //원래 포지션
    Vector3 prePos;
    //클리어 캔버스
    GameObject Canvas2;
    private void Awake()
    {
        prePos = transform.position;
        hp = GameObject.FindWithTag("hp").GetComponent<Image>();
        maincamera = GameObject.FindWithTag("MainCamera");
        cam = GameObject.FindWithTag("camera");
        rgbd = this.GetComponent<Rigidbody2D>();
        Canvas2 = GameObject.FindWithTag("canvas");
    }

    //총알이 방향에 따라 이미지의 로테이션이 변하게 하는 코드
    private void FixedUpdate()
    {
        if (transform.position == prePos)
            return;
        Vector2 v2 = transform.position - prePos;
        float dir = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(180, 180, dir);
        prePos = transform.position;
    }

    //총알 날라가는 파워와 방향 정해주는 함수
    public void Fly(float power, int angle)
    {
        
        float FlyAngle =  180- angle; // 구현 방향에 따라 180 - angle or angle 
        var rot = new Vector2(Mathf.Cos((FlyAngle) * Mathf.Deg2Rad), Mathf.Sin((FlyAngle) * Mathf.Deg2Rad));
        
        rgbd.velocity = rot * power;
        Debug.Log(power);
        Debug.Log(rot);
        Debug.Log(rot * power);
        Debug.Log("rgbd.velocity: " + rgbd.velocity);
        

    }
    // 총알과 충돌했을 경우 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("destroy"))
        {
            Debug.Log("check");
            maincamera.SetActive(true);
            cam.GetComponent<Camera>().enabled=false;
            GameObject go = GameObject.FindWithTag("GameManager");
            go.GetComponent<GameManager>().bullets.Remove(bull);
            Destroy(bull);

        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            hp.fillAmount -= 0.4f;
            if (hp.fillAmount == 0)
            {
                Canvas2.transform.Find("Panel").gameObject.SetActive(true);
            }
            maincamera.SetActive(true);
            cam.GetComponent<Camera>().enabled = false;
            GameObject go = GameObject.FindWithTag("GameManager");
            go.GetComponent<GameManager>().bullets.Remove(bull);
            Destroy(bull);
        }
    }

    private void OnDestroy()
    {
        
        
    }



}

