using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBlue : MonoBehaviour
{
    // �̵��� �������� ���� �迭 ����
    public static Transform[] bluePoints;

    void Awake()
    {
        // �迭�� ũ�� ����
        // child.Count: �� ��ũ��Ʈ�� �����ϰ� �ִ� ������Ʈ�� �ڽ� ����
        // �ڽ� ������Ʈ�� ã�� �����
        // transform.FindChild("�̸�"); transform.GetChild(��ȣ);
        bluePoints = new Transform[transform.childCount];

        // �迭�� �ε������� �ڽ��� Transform ����
        for (int i = 0; i < bluePoints.Length; i++)
        {
            bluePoints[i] = transform.GetChild(i);
        }
    }
}
