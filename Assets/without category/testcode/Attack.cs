using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class Attack : MonoBehaviour
{
	// Token: 0x0600000A RID: 10 RVA: 0x00002274 File Offset: 0x00000474
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Damageable damageable = collision.GetComponent<Damageable>();
		bool flag = damageable != null;
		if (flag)
		{
			Vector2 deliveredKnockback = (base.transform.parent.localScale.x > 0f) ? this.knockback : new Vector2(-this.knockback.x, this.knockback.y);
			bool gotHit = damageable.Hit(this.attackDamage, deliveredKnockback);
			bool flag2 = gotHit;
			if (flag2)
			{
				Debug.Log(collision.name + " hit for " + this.attackDamage.ToString());
			}
		}
	}

	// Token: 0x04000014 RID: 20
	public int attackDamage = 10;

	// Token: 0x04000015 RID: 21
	public Vector2 knockback = Vector2.zero;
}
