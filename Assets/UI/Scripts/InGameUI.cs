using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    PopUpUI m_popUpInstant;     // Title -> InGame ������ �̵� �� ���� ���� ����� �˾�â

    public TextMeshProUGUI m_nowAcademyName;
    public TextMeshProUGUI m_nowDirectorName;

    public TextMeshProUGUI m_nowMoney;
    public TextMeshProUGUI m_touchcount;

   // public TextMeshProUGUI m_CamerapointX;
   // public TextMeshProUGUI m_CamerapointY;
   // public TextMeshProUGUI m_CamerapointZ;
   //
   // public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        m_popUpInstant.DelayTurnOnUI();

        m_nowAcademyName.text = PlayerInfo.Instance.m_AcademyName;
        m_nowDirectorName.text = PlayerInfo.Instance.m_DirectorName;
        m_nowMoney.text = PlayerInfo.Instance.m_MyMoney.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        m_nowMoney.text = PlayerInfo.Instance.m_MyMoney.ToString();

        if(Input.touchCount == 0)
        {
            m_touchcount.text = "0";
        }
        if (Input.touchCount == 1)
        {
            m_touchcount.text = "1";


            // m_touchpointX.text = ;
        }
        if (Input.touchCount == 2)
        {
            m_touchcount.text = "2";

        }


        //m_CamerapointX.text = camera.transform.position.x.ToString();
        //m_CamerapointY.text = camera.transform.position.y.ToString();
        //m_CamerapointZ.text = camera.transform.position.z.ToString();


    }
}
