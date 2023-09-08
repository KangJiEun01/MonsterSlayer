using UnityEngine;

public class Recoil : GenericSingleton<Recoil>
{
   
    Vector3 currentRotation;
    Vector3 targetRotation;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;


    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
    public void RecoilFire(float recoil)
    {
        targetRotation += new Vector3(-recoil, Random.Range(-recoil, recoil), Random.Range(-recoilZ, recoilZ));
    }
}
