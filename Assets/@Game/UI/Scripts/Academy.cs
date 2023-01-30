using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// 
/// </summary>
public class Academy : MonoBehaviour
{
    public TMP_InputField m_Academy;
    public TMP_InputField m_Director;

    private string m_AcademyName;
    private string m_DirectorName;

    public TextMeshProUGUI m_PopupNotice;                       // �ȳ����� ����� ����

    #region AcademyData
    public string MyAcademy
    {
        get { return m_AcademyName; }
        private set { m_AcademyName = value; }
    }
    public string MyDirector
    {
        get { return m_DirectorName; }
        private set { m_DirectorName = value; }
    }
    #endregion

    // ��ī�����̸� & ���� �̸� ����
    public void SetAcademyData()
    {
        if (m_Academy.text != "" && m_Director.text != "")
        {

            MyAcademy = m_Academy.text;
            MyDirector = m_Director.text;

            Debug.Log("AcademyName : " + MyAcademy);
            Debug.Log("DirectorName : " + MyDirector);

            PlayerInfo.Instance.m_AcademyName = MyAcademy;
            PlayerInfo.Instance.m_DirectorName = MyDirector;

            string strFile = Application.dataPath + "\\Json\\PlayerInfo_NewtonJson.json";
            FileInfo fileInfo = new FileInfo(strFile);

            Debug.Log(strFile);

            // ���� �ִ��� Ȯ�� ������ true ������ false
            if (fileInfo.Exists == true)
            {
                Debug.Log(" ���� �����ϱ� ����� ");
                // ��ųʸ��� ������ NewtonJson �������
                // "\\Json\\NewtonJson.json" -> ���� �̸��� ������ ������ �´� �̸�����
                //File.WriteAllText(Application.dataPath + "\\Json\\PlayerInfo_NewtonJson.json", JsonConvert.SerializeObject(PlayerInfo.Instance));
                Debug.Log(PlayerInfo.Instance);

            }
            else
            {
                Debug.Log(" ���� �����ϱ� ���� ");
                //File.WriteAllText(Application.dataPath + "\\Json\\PlayerInfo_NewtonJson.json", JsonConvert.SerializeObject(PlayerInfo.Instance));
                Debug.Log(PlayerInfo.Instance);

            }

            if (GPGSBinder.Instance.UserId != null)
            {
                GPGSBinder.Instance.UnlockAchievement(GPGSIds.achievement_Welcome);     //���� ���� �޼�
                // �����̸� ��� �ұ�?
            }

            MoveSceneManager.m_Instance.MoveToInGameScene();
        }
        else
        {
            m_PopupNotice.text = "�� �� �Է����ּ���";

            // '��й�ȣ�� ���� �ʽ��ϴ�' �˾�â ����
            m_PopupNotice.gameObject.SetActive(true);

            m_PopupNotice.gameObject.GetComponent<PopOffUI>().DelayTurnOffUI();    // �˾�â ���� 
        }
    }
}
