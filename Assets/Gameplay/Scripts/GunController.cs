using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFinished { get; private set; } = false;

    [SerializeField] private Transform _barrel;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Animator _anim;

    public void StartShooting()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        isFinished = false;
        _anim.SetTrigger("StartShooting");
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 30; i++)
        {
            var scatter = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 40f;
            var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2) + scatter);

            var bullet = Instantiate(_bulletPref, _barrel.transform.position, Quaternion.identity);            
            bullet.GetComponent<Rigidbody>().AddForce(ray.direction * 1000f);
            yield return new WaitForSeconds(0.1f);
        }

        _anim.SetTrigger("StopShooting");
        yield return new WaitForSeconds(0.7f);
        isFinished = true;
    }
}
