using UnityEngine;
using UnityEngine.VFX;

public class BoltScripts : MonoBehaviour
{
    [SerializeField] Transform[] _pos;
    [SerializeField] Transform bossTransform; // 보스의 Transform

    private VisualEffect visualEffect;
    private float radius = 10f;
    private float height = 10f;
    private float minLength = 8f;
    private float maxLength = 1f;
    private float maxDistance = .5f; // 최대 거리 설정

    void Start()
    {
        visualEffect = GetComponentInChildren<VisualEffect>();
        visualEffect.SetVector4("Color", new Vector4(8, 0, 0, 0));
    }

    ///float timer = 0;

    // Update is called once per frame
    void Update()
    {
        ///timer += Time.deltaTime;
        ///if(timer > 1 )
        {
            ///visualEffect.playRate = 1;
            Vector3 startPos = GetRandomPositionOnCircle(radius, height);
            Vector3 endPos = bossTransform.position + GetRandomBossSize();

            // 시작 위치와 끝 위치 사이의 거리를 최대 거리(maxDistance)로 제한
            Vector3 direction = endPos - startPos;
            float distance = direction.magnitude;
            if (distance > maxDistance)
            {
                direction = direction.normalized * maxDistance;
                endPos = startPos + direction;
            }

            float length = Random.Range(minLength, maxLength);
            Vector3 cornerPos1 = startPos + direction.normalized * length;
            Vector3 cornerPos2 = endPos - direction.normalized * length;

            _pos[0].position = startPos;
            _pos[1].position = endPos;
            _pos[2].position = cornerPos1;
            _pos[3].position = cornerPos2;

            //visualEffect.SetVector3("StartPos", startPos);
            //visualEffect.SetVector3("EndPos", endPos);
            //visualEffect.SetVector3("CornerPos1", cornerPos1);
            //visualEffect.SetVector3("CornerPos2", cornerPos2);
            ///if(timer > 2)
            ///{
                ///timer = 0;
                ///visualEffect.playRate = 0;
            ///}
        }
    }

    private Vector3 GetRandomPositionOnCircle(float radius, float height)
    {
        float angle = Random.Range(0f, 360f);
        return GetPositionOnCircle(angle, radius, height);
    }

    private Vector3 GetPositionOnCircle(float angle, float radius, float height)
    {
        float radian = angle * Mathf.Deg2Rad;
        float x = Mathf.Sin(radian) * radius;
        float z = Mathf.Cos(radian) * radius;

        return new Vector3(x, height, z);
    }

    private Vector3 GetRandomBossSize()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        float z = Random.Range(-6f, 7f);

        return new Vector3(x, y, z);
    }
}