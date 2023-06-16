using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YDZoomControllerCamera : MonoBehaviour
{
    public float zoomSpeed = 10f;          // 줌 속도
    public float minFOV = 30f;             // 최소 FOV 값
    public float maxFOV = 60f;             // 최대 FOV 값

    private Camera _playerCamera;
    private bool _isZoomedIn = false;

    private void Start()
    {
        _playerCamera = GetComponent<Camera>();

    }

    private void Update()//수정할 내용 : 총을 쏘는 순간(마우스 왼쪽 버튼 누를때) 줌인이되고 줌 아웃이 되게끔
    {
        // 마우스 휠 입력을 감지하고 FOV 값을 변경합니다.
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            // FOV 값을 스크롤 입력에 따라 증가 또는 감소시킵니다.
            _playerCamera.fieldOfView += scrollInput * zoomSpeed;

            // FOV 값을 최소 및 최대 값으로 제한합니다.
            _playerCamera.fieldOfView = Mathf.Clamp(_playerCamera.fieldOfView, minFOV, maxFOV);
        }

        // 줌인/줌아웃 입력을 감지하고 FOV 값을 변경합니다.
        if (Input.GetButtonDown("Fire2"))   // 예를 들어, Fire2는 우클릭으로 설정합니다.
        {
            _isZoomedIn = !_isZoomedIn;   // 줌 상태를 토글합니다.

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
