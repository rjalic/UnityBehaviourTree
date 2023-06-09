using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 10f;
    private Vector3 target;
    private PlayerMovement player;

    public void SetTarget(Vector3 tar)
    {
        this.target = tar;
    }

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * speed * target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            return;
        }
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
