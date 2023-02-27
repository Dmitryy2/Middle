using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Damageable : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000C RID: 12 RVA: 0x0000232C File Offset: 0x0000052C
	// (set) Token: 0x0600000D RID: 13 RVA: 0x00002344 File Offset: 0x00000544
	public int MaxHealth
	{
		get
		{
			return this._maxHealth;
		}
		set
		{
			this._maxHealth = value;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000E RID: 14 RVA: 0x00002350 File Offset: 0x00000550
	// (set) Token: 0x0600000F RID: 15 RVA: 0x00002368 File Offset: 0x00000568
	public int Health
	{
		get
		{
			return this._health;
		}
		set
		{
			this._health = value;
			UnityEvent<int, int> unityEvent = this.healthChanged;
			if (unityEvent != null)
			{
				unityEvent.Invoke(this._health, this.MaxHealth);
			}
			bool flag = this._health <= 0;
			if (flag)
			{
				this.IsAlive = false;
			}
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000010 RID: 16 RVA: 0x000023B8 File Offset: 0x000005B8
	// (set) Token: 0x06000011 RID: 17 RVA: 0x000023D0 File Offset: 0x000005D0
	public bool IsAlive
	{
		get
		{
			return this._isAlive;
		}
		set
		{
			this._isAlive = value;
			this.animator.SetBool(AnimationStrings.isAlive, value);
			Debug.Log("IsAlive set " + value.ToString());
			bool flag = !value;
			if (flag)
			{
				this.damageableDeath.Invoke();
			}
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002424 File Offset: 0x00000624
	// (set) Token: 0x06000013 RID: 19 RVA: 0x00002446 File Offset: 0x00000646
	public bool LockVelocity
	{
		get
		{
			return this.animator.GetBool(AnimationStrings.lockVelocity);
		}
		set
		{
			this.animator.SetBool(AnimationStrings.lockVelocity, value);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000245B File Offset: 0x0000065B
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000246C File Offset: 0x0000066C
	private void Update()
	{
		bool flag = this.isInvincible;
		if (flag)
		{
			bool flag2 = this.timeSinceHit > this.invincibilityTime;
			if (flag2)
			{
				this.isInvincible = false;
				this.timeSinceHit = 0f;
			}
			this.timeSinceHit += Time.deltaTime;
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000024C0 File Offset: 0x000006C0
	public bool Hit(int damage, Vector2 knockback)
	{
		bool flag = this.IsAlive && !this.isInvincible;
		bool result;
		if (flag)
		{
			this.Health -= damage;
			this.isInvincible = true;
			this.animator.SetTrigger(AnimationStrings.hitTrigger);
			this.LockVelocity = true;
			UnityEvent<int, Vector2> unityEvent = this.damageableHit;
			if (unityEvent != null)
			{
				unityEvent.Invoke(damage, knockback);
			}
			CharacterEvents.characterDamaged(base.gameObject, damage);
			result = true;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002548 File Offset: 0x00000748
	public bool Heal(int healthRestore)
	{
		bool flag = this.IsAlive && this.Health < this.MaxHealth;
		bool result;
		if (flag)
		{
			int maxHeal = Mathf.Max(this.MaxHealth - this.Health, 0);
			int actualHeal = Mathf.Min(maxHeal, healthRestore);
			this.Health += actualHeal;
			CharacterEvents.characterHealed(base.gameObject, actualHeal);
			result = true;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x04000016 RID: 22
	public UnityEvent<int, Vector2> damageableHit;

	// Token: 0x04000017 RID: 23
	public UnityEvent damageableDeath;

	// Token: 0x04000018 RID: 24
	public UnityEvent<int, int> healthChanged;

	// Token: 0x04000019 RID: 25
	private Animator animator;

	// Token: 0x0400001A RID: 26
	[SerializeField]
	private int _maxHealth = 100;

	// Token: 0x0400001B RID: 27
	[SerializeField]
	private int _health = 100;

	// Token: 0x0400001C RID: 28
	[SerializeField]
	private bool _isAlive = true;

	// Token: 0x0400001D RID: 29
	[SerializeField]
	private bool isInvincible = false;

	// Token: 0x0400001E RID: 30
	private float timeSinceHit = 0f;

	// Token: 0x0400001F RID: 31
	public float invincibilityTime = 0.25f;
}
