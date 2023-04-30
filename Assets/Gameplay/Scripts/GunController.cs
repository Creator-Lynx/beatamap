using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFinished { get; private set; } = false;

    [SerializeField] private Transform Barrel;
    [SerializeField] private GameObject BulletPref;

    public void StartShooting()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        isFinished = false;

        for (int i = 0; i < 30; i++)
        {
            var scatter = new Vector2(Random.value, Random.value) * 60f;
            var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2) + scatter);

            var bullet = Instantiate(BulletPref, Barrel.transform.position, Quaternion.identity);            
            bullet.GetComponent<Rigidbody>().AddForce(ray.direction * 1000f);
            yield return new WaitForSeconds(0.1f);
        }

        isFinished = true;
    }
}
