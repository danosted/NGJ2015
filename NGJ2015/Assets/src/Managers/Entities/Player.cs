﻿using UnityEngine;
using System.Collections;
using Assets.src.Managers;

namespace Assets.src.Managers.Entities
{
	public class Player : CharacterBase
	{
		public Player(float health, float speed, float range, float damage) : base(health, speed, range, damage)
		{

		}

		public void OnEnable()
		{
			ManagerCollection.Instance.KeyInputManager.OnDownPressed += this.OnDownPressed;
            ManagerCollection.Instance.KeyInputManager.OnDownReleased += this.OnDownReleased;
            ManagerCollection.Instance.KeyInputManager.OnUpPressed += this.OnUpPressed;
            ManagerCollection.Instance.KeyInputManager.OnUpReleased += this.OnUpReleased;
            ManagerCollection.Instance.KeyInputManager.OnRightPressed += this.OnRightPressed;
            ManagerCollection.Instance.KeyInputManager.OnRightReleased += this.OnRightReleased;
            ManagerCollection.Instance.KeyInputManager.OnLeftPressed += this.OnLeftPressed;
            ManagerCollection.Instance.KeyInputManager.OnLeftReleased += this.OnLeftReleased;
            ManagerCollection.Instance.KeyInputManager.OnSpacePressed += this.OnSpacePressed;
            ManagerCollection.Instance.KeyInputManager.OnSpaceReleased += this.OnSpaceReleased;
		}

		public void OnDisable()
		{
            ManagerCollection.Instance.KeyInputManager.OnDownPressed -= this.OnDownPressed;
            ManagerCollection.Instance.KeyInputManager.OnDownReleased -= this.OnDownReleased;
            ManagerCollection.Instance.KeyInputManager.OnUpPressed -= this.OnUpPressed;
            ManagerCollection.Instance.KeyInputManager.OnUpReleased -= this.OnUpReleased;
            ManagerCollection.Instance.KeyInputManager.OnRightPressed -= this.OnRightPressed;
            ManagerCollection.Instance.KeyInputManager.OnRightReleased -= this.OnRightReleased;
            ManagerCollection.Instance.KeyInputManager.OnLeftPressed -= this.OnLeftPressed;
            ManagerCollection.Instance.KeyInputManager.OnLeftReleased -= this.OnLeftReleased;
            ManagerCollection.Instance.KeyInputManager.OnSpacePressed -= this.OnSpacePressed;
            ManagerCollection.Instance.KeyInputManager.OnSpaceReleased -= this.OnSpaceReleased;
		}

		private void OnDownPressed()
		{
			Move(new Vector3(0, 1)*Time.deltaTime);
		}
		private void OnDownReleased()
		{
			
		}
		private void OnUpPressed()
		{
			Move(new Vector3(0, -1)*Time.deltaTime);
		}
		private void OnUpReleased()
		{
			
		}
		private void OnRightPressed()
		{
			Move(new Vector3(1, 0)*Time.deltaTime);
		}
		private void OnRightReleased()
		{
			
		}
		private void OnLeftPressed()
		{
			Move(new Vector3(-1, 0)*Time.deltaTime);
		}
		private void OnLeftReleased()
		{

		}
		private void OnSpacePressed()
		{

		}
		private void OnSpaceReleased()
		{
			
		}

		public void TakeDamage(float damage)
		{
			_health -= damage;
		}
	}
}