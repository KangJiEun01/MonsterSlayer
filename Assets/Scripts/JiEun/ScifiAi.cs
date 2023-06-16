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

    float WalkTime_Sumcooltime = 3; //x�� ������ ���� ����Ʈ�� �������� �ٲ� �� ������ �ٲ㺸��
    float WalkTime_current;//�ð��� x�� ���ҳ�
    float WalkTime_start; //point++ ���� ���� ��
                          //����� ���� ĳ���� �����̼�
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
    void Walk()//�¿�� ������ 2~5ȸ ***�̵� ����(����) 4�ʰ� (y �����̼�) ��� ***
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
            //    _nowpoint = 0;//���� �迭�� �����ϰ� �������� ��ü***
            //}
        }
    }
    void Reset_WalkCoolTime() //�ɾ����Ÿ�� �ʱ�ȭ
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
        //_scifi.rotation = (Quaternion.Lerp(_scifi.rotation,Quaternion.Euler(0,90,0) , 0.4f)); //Euler ���� ������ �������� �ٶ󺸰� ����
        _scifi.rotation = (Quaternion.Lerp(_scifi.rotation, Qut, 1f)); //Euler ���� ������ �������� �ٶ󺸰� ����
    }
    void stand()
    {
        int ranTime = Random.Range(2, 6);
        Debug.Log("���ִ¾ִϸ��̼�");
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
