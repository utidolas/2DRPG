using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    // props
    public Vector3 Direction {  get; set; }
    public float Damage { get; set; }

    private void Update()
    {
        transform.Translate(Direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IDamageable>()?.TakeDamage(Damage); // call 'TakeDamage' if IDamageable Interface not null
        Destroy(gameObject);
    }
}
