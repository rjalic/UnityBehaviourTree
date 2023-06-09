using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 10f;
    private Vector3 _target;
    private GameObject _shooter;

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    public void SetPlayer(GameObject shooter)
    {
        _shooter = shooter;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * speed * _target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _shooter)
        {
            return;
        }
        var enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
