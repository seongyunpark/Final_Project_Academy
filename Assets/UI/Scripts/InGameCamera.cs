using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    public float m_OrthoZoomSpeed = 0.5f;    // OrthoGraphic Mode
    public Camera camera;

    public float MoveSpeed;
    // Vector2 PrevPos = Vector2.zero;

    Vector2 prePos, nowPos;
    Vector3 movePos;

    float PrevDistance = 0.0f;

    Vector2 ClickPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameTime.Instance.IsGameMode == true)
        {
            if (Input.touchCount == 2)      // ����.�ƿ� ������ �հ��� 2����ŭ�� ��ġ�� ���
            {
                PinchZoom();
            }
        }


        // if (Input.touchCount == 1)
        // {
        //     ClickPoint = Input.GetTouch(0).position;
        // }

        if (Input.touchCount == 1)
        {
            ClickPoint = Input.GetTouch(0).position;

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                prePos = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                nowPos = touch.position - touch.deltaPosition;
                movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * MoveSpeed;
                camera.transform.Translate(movePos);
                prePos = touch.position - touch.deltaPosition;
            }
        }

        if (Input.touchCount == 0)
        {

        }
    }

    /* public void OnMouseDrag()
     {
         int touchcount = Input.touchCount;

         ////����
         if (touchcount == 1)
         {
             if (PrevPos == Vector2.zero)
             {
                 PrevPos = Input.GetTouch(0).position;

                 return;
             }

             Vector2 dir = (Input.GetTouch(0).position - PrevPos).normalized;
             Vector3 vec = new Vector3(dir.x, dir.y);

             camera.transform.position -= vec * MoveSpeed * Time.deltaTime;
             PrevPos = Input.GetTouch(0).position;
         }
     }

     public void ExitDrag()
     {
         PrevPos = Vector2.zero;
         PrevDistance = 0.0f;
     }
    */

    public void PinchZoom()
    {
        Touch touchZero = Input.GetTouch(0);        // ù��° �հ��� ��ǥ
        Touch touchOne = Input.GetTouch(1);         // �ι�° �հ��� ��ǥ

        // deltaPosition �� deltatime�� �����ϰ� delta ��ŭ �ð����� ������ �Ÿ��̴�
        // ���� position���� ���� delta ���� ���ָ� �����̱� ���� �հ��� ��ġ�� �ȴ�
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // ����� ���Ű��� ������ ũ�� ���ϱ�
        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        // �� ���� ���� = Ȯ�� / ��� �� �� ��ŭ ���� Ȯ�� / ��� ���� ����
        float deltaMagnitudeDiff = prevTouchDeltaMag - TouchDeltaMag;

        if (camera.orthographic)
        {
            camera.orthographicSize += deltaMagnitudeDiff * m_OrthoZoomSpeed;

            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 3f);
            camera.orthographicSize = Mathf.Min(camera.orthographicSize, 12f);
        }

    }

}
