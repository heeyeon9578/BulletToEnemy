using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;

    //사용할 스프라이트 배열 만들기
    public Sprite[] sprites;
    //스프라이트 넘버 받아오기
    public int i;
    //플레이어
    public GameObject adventurer;
    //적
    public GameObject Enemy;
    //스타트 캔버스 
    public GameObject canvas;
    //리스트로 bullet 등록
    public List<GameObject> bullets= new List<GameObject>();
    //적 체력바
    public Image img;
    //원형 바
    public Image img2;
    //총알 오브젝트
    public GameObject bull1;
    //총알의 파워값
    public static float power;
    //총 스크립트
    Bullet bull;
    //총알 따라가는 카메라
    public GameObject cam;
    //메인카메라
    GameObject maincamera;
    //토글 체크하기
    public Toggle toggle;
    public Toggle toggle1;
    public Toggle toggle2;
    // 따라다닐 타겟 오브젝트의 Transform
    Transform target;
    // 카메라 자신의 Transform
    Transform tr;
    //카메라가 따라갈 총알 인덱스
    int index = 0;
    //슬라이더 헬퍼 
    public SliderHelper slider;
    //각도 값
    int ang;
    //파워 값
    static float power2 = 0f;
    //끝날때 캔버스
    public GameObject canvas2;
    private GameManager()
    {

    }

    public GameManager getGameManager()
    {
        if (gameManager == null)
        {
            gameManager = new GameManager();
        }
        return gameManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*slider = GameObject.FindWithTag("slider").GetComponent<SliderHelper>();*/
        img2.fillAmount = 0;
        
        tr = cam.GetComponent<Transform>();
        maincamera = GameObject.FindWithTag("MainCamera");

        //Resources 폴더로부터 게임 오브젝트 불러들이기             
        bull1 = Resources.Load("bullet") as GameObject;
        bull = bull1.GetComponent<Bullet>();
        Debug.Log(bull);
        canvas2 = GameObject.FindWithTag("canvas");
    }

    // Update is called once per frame
    void Update()
    {

        //카메라가 true일때, target의 position을 계속 따라가기
        if (cam.GetComponent<Camera>().enabled)
        {
            cam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 10);

        }


        if (Input.GetKeyDown(KeyCode.Space) && (bullets.Count < 1))
        {
            Debug.Log("키가 눌렸습니다");
            power = 0f;
            img2.fillAmount = power;
 
        }
        else if (Input.GetKeyUp(KeyCode.Space)&& !(img2.fillAmount == 0f)&& (bullets.Count < 1))
        {
            GameObject newBullet = Instantiate(bull1);
            bullets.Add(newBullet);
            newBullet.transform.position = new Vector3(7, Random.Range(-3.5f, -2.5f), 0);
            ang = (int)(power * 179);
            Debug.Log(ang);
            newBullet.GetComponent<Bullet>().Fly(power2, ang);
            camcam();
            slider.value = 0f;



        }
        else if (Input.GetKey(KeyCode.Space) && (bullets.Count < 1))
        {
            
            
            power += 0.001f;
            img2.fillAmount = power;
            if (power >= 1)
            {
                power = 0;
            }
        }

        collision();

    }

    //선택한 캐릭터에 맞게 스프라이트 설정 
    public void chooseCharacter(int i)
    {
        if (toggle.isOn)
        {
            i = 0;
        }
        else if (toggle1.isOn)
        {
            i = 1;
        }
        else if (toggle2.isOn)
        {
            i = 2;
        }

        adventurer.GetComponent<SpriteRenderer>().sprite = sprites[i];
    }

    //캐릭터를 선택 했을 경우, 캔버스를 오프
    public void selected()
    {
        if (toggle.isOn || toggle1.isOn || toggle2.isOn)
        {
            canvas.SetActive(false);
        }
    }

    //적이 죽었을 경우 씬 재로드
    public void Reload()
    {

        SceneManager.LoadScene(gameObject.scene.name);
    }

    //Bullet / Enemy 충돌 검사 로직 구현
     void collision()
    {
        for(int i=0; i<bullets.Count; i++)
        {
            if (bullets[i].transform.position.x < Enemy.transform.position.x && bullets[i].transform.position.y == Enemy.transform.position.y)
            {
                if (img.fillAmount <= 0)
                {
                    canvas2.transform.Find("Panel").gameObject.SetActive(true);

                }
                GameObject bullet_a = bullets[i];
                bullets.Remove(bullets[i]);
                img.fillAmount -= 0.4f;
                Destroy(bullet_a);
                maincamera.SetActive(true);
                cam.GetComponent<Camera>().enabled = false;
                return;
            }
        }
      

    }
    
    //카메라 관련 스크립트
    void camcam()
    {

        target = bullets[0].transform;
        cam.GetComponent<Camera>().enabled = true;
        maincamera.SetActive(false);
        Vector3 vec = new Vector3(target.position.x, target.transform.position.y, 0);
        tr.position = vec;
    }

    //총알의 파워를 받아올 함수
    public void bullpower(float per)
    {
        slider.value = per;
        power2 = per*40;
        Debug.Log("power2=============="+ power2);
    }

}
