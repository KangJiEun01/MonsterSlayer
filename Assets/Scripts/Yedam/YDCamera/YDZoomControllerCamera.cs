using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YDZoomControllerCamera : MonoBehaviour
{
    public float zoomSpeed = 10f;          // �� �ӵ�
    public float minFOV = 30f;             // �ּ� FOV ��
    public float maxFOV = 60f;             // �ִ� FOV ��

    private Camera _playerCamera;
    private bool _isZoomedIn = false;

    private void Start()
    {
        _playerCamera = GetComponent<Camera>();

    }

    private void Update()//������ ���� : ���� ��� ����(���콺 ���� ��ư ������) �����̵ǰ� �� �ƿ��� �ǰԲ�
    {
        // ���콺 �� �Է��� �����ϰ� FOV ���� �����մϴ�.
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            // FOV ���� ��ũ�� �Է¿� ���� ���� �Ǵ� ���ҽ�ŵ�ϴ�.
            _playerCamera.fieldOfView += scrollInput * zoomSpeed;

            // FOV ���� �ּ� �� �ִ� ������ �����մϴ�.
            _playerCamera.fieldOfView = Mathf.Clamp(_playerCamera.fieldOfView, minFOV, maxFOV);
        }

        // ����/�ܾƿ� �Է��� �����ϰ� FOV ���� �����մϴ�.
        if (Input.GetButtonDown("Fire2"))   // ���� ���, Fire2�� ��Ŭ������ �����մϴ�.
        {
            _isZoomedIn = !_isZoomedIn;   // �� ���¸� ����մϴ�.

            if (_isZoomedIn)
            {
                _playerCamera.fieldOfView = minFOV;
            }
            else
            {
                _playerCamera.fieldOfView = maxFOV;
            }
        }
    }
}
