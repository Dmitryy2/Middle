using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000007 RID: 7
public class CharacterEvents
{
	// Token: 0x04000023 RID: 35
	public static UnityAction<GameObject, int> characterDamaged;

	// Token: 0x04000024 RID: 36
	public static UnityAction<GameObject, int> characterHealed;
}