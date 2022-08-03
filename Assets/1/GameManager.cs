using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;

    //����� ��������Ʈ �迭 �����
    public Sprite[] sprites;
    //��������Ʈ �ѹ� �޾ƿ���
    public int i;
    //�÷��̾�
    public GameObject adventurer;
    //��
    public GameObject Enemy;
    //��ŸƮ ĵ���� 
    public GameObject canvas;
    //����Ʈ�� bullet ���
    public List<GameObject> bullets= new List<GameObject>();
    //�� ü�¹�
    public Image img;
    //���� ��
    public Image img2;
    //�Ѿ� ������Ʈ
    public GameObject bull1;
    //�Ѿ��� �Ŀ���
    public static float power;
    //�� ��ũ��Ʈ
    Bullet bull;
    //�Ѿ� ���󰡴� ī�޶�
    public GameObject cam;
    //����ī�޶�
    GameObject maincamera;
    //��� üũ�ϱ�
    public Toggle toggle;
    public Toggle toggle1;
    public Toggle toggle2;
    // ����ٴ� Ÿ�� ������Ʈ�� Transform
    Transform target;
    // ī�޶� �ڽ��� Transform
    Transform tr;
    //ī�޶� ���� �Ѿ� �ε���
    int index = 0;
    //�����̴� ���� 
    public SliderHelper slider;
    //���� ��
    int ang;
    //�Ŀ� ��
    static float power2 = 0f;
    //������ ĵ����
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

        //Resources �����κ��� ���� ������Ʈ �ҷ����̱�             
        bull1 = Resources.Load("bullet") as GameObject;
        bull = bull1.GetComponent<Bullet>();
        Debug.Log(bull);
        canvas2 = GameObject.FindWithTag("canvas");
    }

    // Update is called once per frame
    void Update()
    {

        //ī�޶� true�϶�, target�� position�� ��� ���󰡱�
        if (cam.GetComponent<Camera>().enabled)
        {
            cam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 10);

        }


        if (Input.GetKeyDown(KeyCode.Space) && (bullets.Count < 1))
        {
            Debug.Log("Ű�� ���Ƚ��ϴ�");
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

    //������ ĳ���Ϳ� �°� ��������Ʈ ���� 
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

    //ĳ���͸� ���� ���� ���, ĵ������ ����
    public void selected()
    {
        if (toggle.isOn || toggle1.isOn || toggle2.isOn)
        {
            canvas.SetActive(false);
        }
    }

    //���� �׾��� ��� �� ��ε�
    public void Reload()
    {

        SceneManager.LoadScene(gameObject.scene.name);
    }

    //Bullet / Enemy �浹 �˻� ���� ����
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
    
    //ī�޶� ���� ��ũ��Ʈ
    void camcam()
    {

        target = bullets[0].transform;
        cam.GetComponent<Camera>().enabled = true;
        maincamera.SetActive(false);
        Vector3 vec = new Vector3(target.position.x, target.transform.position.y, 0);
        tr.position = vec;
    }

    //�Ѿ��� �Ŀ��� �޾ƿ� �Լ�
    public void bullpower(float per)
    {
        slider.value = per;
        power2 = per*40;
        Debug.Log("power2=============="+ power2);
    }

}
