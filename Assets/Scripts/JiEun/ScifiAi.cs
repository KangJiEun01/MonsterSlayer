using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScifiAi : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    [SerializeField] Transform _hero;
    [SerializeField] float _speed;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody _ScifiRig;
    Transform _scifi;
    int _nowpoint = 0;

    float WalkTime_Sumcooltime = 3; //x초 지나면 다음 포인트로 랜덤으로 바꿀 수 있으면 바꿔보기
    float WalkTime_current;//시간이 x초 남았나
    float WalkTime_start; //point++ 까지 남은 초
                          //방향과 같이 캐릭터 로테이션
    float StandTime_Sumcooltime = 3;
    float StandTime_current;
    float StandTime_start;
    void Start()
    {
        _scifi = GetComponent<Transform>();
        _animator.Play("Walk");
    }
    void Update()
    {
        Walk();
    }
    void Walk()//좌우로 움직임 2~5회 ***이동 마다(랜덤) 4초간 (y 로테이션) 대기 ***
    {

       // int ranNum = Random.Range(0, _points.Length);
        Vector3 NextPosition = _points[_nowpoint].position;
        Vector3 dirVector = (NextPosition - _scifi.position).normalized;
        Vector3 LastVector = dirVector * _speed;
        _scifi.position = _scifi.position + LastVector * Time.deltaTime;
        WalkTime_current = Time.time - WalkTime_start;
        if (WalkTime_current > WalkTime_Sumcooltime)
        {
            stand();
            int ranNum = Random.Range(0, _points.Length);
            _nowpoint= ranNum;
            //if (_nowpoint >= _points.Length)
            //{
            //    _nowpoint = 0;//현재 배열값 제외하고 랜덤으로 교체***
            //}
        }
    }
    void Reset_WalkCoolTime() //걸어가는쿨타임 초기화
    {
        _speed = 1;
        _animator.Play("Walk");
        WalkTime_current = WalkTime_Sumcooltime;
        WalkTime_start = Time.time;
        ScifiRotation();
    }
    void ScifiRotation()
    {
        Quaternion Qut = _points[_nowpoint].rotation;
        //Quaternion Qut2 = _hero.rotation;
        //Quaternion Qut3 = _hero.rotation.y;
        //_scifi.rotation = (Quaternion.Lerp(_scifi.rotation,Quaternion.Euler(0,90,0) , 0.4f)); //Euler 값을 포인터 방향으로 바라보게 수정
        _scifi.rotation = (Quaternion.Lerp(_scifi.rotation, Qut, 1f)); //Euler 값을 포인터 방향으로 바라보게 수정
    }
    void stand()
    {
        int ranTime = Random.Range(2, 6);
        Debug.Log("서있는애니메이션");
        _animator.Play("Idle");
        _speed = 0;
        Invoke("Reset_WalkCoolTime", ranTime);
        //_ScifiRig.velocity = Vector3.zero;

    }
    //void ScifiRotation02()
    //{
    //    float _yRo = _points[_nowpoint].transform.rotation.y;
    //    Vector3 _scifiRo = new Vector3(0, _yRo, 0);
    //}
}
