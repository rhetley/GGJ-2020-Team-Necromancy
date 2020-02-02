using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{

    public static GameObject CreateProjectile2D<Collider>(GameObject caster, Vector2 StartPos, Vector2 LaunchVector) where Collider : Collider2D
    {
        return CreateProjectile2D<Collider>(caster, StartPos, LaunchVector, null);
    }
    public static GameObject CreateProjectile2D<Collider>(GameObject caster, Vector2 StartPos, Vector2 LaunchVector, UnityEvent hitEvent) where Collider : Collider2D
    {
        return CreateProjectile2D<Collider>(caster, StartPos, LaunchVector, hitEvent, Resources.Load<Sprite>("sad"), 0.1f * Vector3.one, 4.73f * Vector2.one);
    }
    public static GameObject CreateProjectile2D<Collider>(GameObject caster, Vector2 StartPos, Vector2 LaunchVector, UnityEvent hitEvent, Sprite sprite, Vector3 ProjectileScale, Vector2 Collider2DScale) where Collider : Collider2D
    {
        GameObject Projectile = new GameObject();
        Rigidbody2D rigid = Projectile.AddComponent<Rigidbody2D>();
        Collider2D collider2D = Projectile.AddComponent<Collider>();
        Projectile proj = Projectile.AddComponent<Projectile>();
        SpriteRenderer spr = Projectile.AddComponent<SpriteRenderer>();
        if (collider2D is BoxCollider2D)
        {
            BoxCollider2D b = (BoxCollider2D)collider2D;
            b.size = Collider2DScale;
        }
        Projectile.transform.position = StartPos;
        spr.sortingOrder = 3;
        Projectile.layer = 12;
        spr.sprite = sprite;
        proj.LaunchVelocity = LaunchVector;
        proj.hitEvent = hitEvent;
        proj.Caster = caster;
        Projectile.transform.localScale = ProjectileScale;
        return Projectile;
    }

    Rigidbody2D rigid;
    [SerializeField]
    Vector2 LaunchVelocity;
    [SerializeField]
    UnityEvent hitEvent;
    [SerializeField]
    public GameObject Caster;
    void Start()
    {
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        rigid.velocity = LaunchVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Hook h = Caster.GetComponent<Hook>();
        if (h != null)
        {
            h.InitialHookTeather = new Vector2(this.transform.position.x, this.transform.position.y);
        }
        hitEvent?.Invoke();

    }
}
