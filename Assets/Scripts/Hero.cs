using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	public int maxHP = 30;

	private int HP = 30;

	public int GetCurrentHealth () {
		return this.HP;
	}

	public void ReduceHealth (int damageTaken) {
		this.HP -= damageTaken;
	}

	public void RestoreHealth (int healingAmount) {
		this.HP += healingAmount;
		if (this.HP > maxHP) {
			this.HP = maxHP;
		}
	}
}
