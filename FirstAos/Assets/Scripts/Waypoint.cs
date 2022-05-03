using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // �̵��� �������� ���� �迭 ����
    public static Transform[] points;

    void Awake()
    {
        // �迭�� ũ�� ����
        // child.Count: �� ��ũ��Ʈ�� �����ϰ� �ִ� ������Ʈ�� �ڽ� ����
        // �ڽ� ������Ʈ�� ã�� �����
        // transform.FindChild("�̸�"); transform.GetChild(��ȣ);
        points = new Transform[transform.childCount];

        // �迭�� �ε������� �ڽ��� Transform ����
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
