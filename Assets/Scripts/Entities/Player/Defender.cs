﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spirinse.Interfaces;
using System;

namespace Spirinse.Player
{
    public class Defender : MonoBehaviour, IDamagable
    {
        public Transform model;
        private Rigidbody2D rb;
        public Action<int> TakeDamageAction;
        public TrackingBall tracker;
        public Controls tempControls;

        public DefenderParticles defenderParticles;

        public int rotateSpeed = 50;
        public bool TakeDamage(int damage)
        {
            if (damage < 1) return false;
            TakeDamageAction?.Invoke(damage);
            return true;
        }

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            TakeDamageAction += defenderParticles.GetHit;
        }

        private void Update()
        {
            // TEMPORARY
            var dir = rb.velocity;

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            model.transform.localRotation = Quaternion.RotateTowards(model.transform.localRotation, q, rotateSpeed * Time.deltaTime);

            if(dir.x < 0)
            {
                model.localScale = new Vector3(model.localScale.x, -Mathf.Abs(model.localScale.y), model.localScale.z);
            }
            else
            {
                model.localScale = new Vector3(model.localScale.x, Mathf.Abs(model.localScale.y), model.localScale.z);
            }
        }
    }
}
