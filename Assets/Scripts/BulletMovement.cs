using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 25f;
    private GameObject _shooter;
    private Vector3 _target;

    private void Update()
    {
        transform.position += Time.deltaTime * speed * _target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _shooter) return;
        var enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy != null) enemy.TakeDamage(damage);

        Destroy(gameObject);
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    public void SetPlayer(GameObject shooter)
    {
        _shooter = shooter;
    }
}