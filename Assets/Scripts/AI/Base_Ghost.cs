﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Ghost : Unit, Idamagable, IRageble, IRescueAble
{
    public GameObject player, spawn, target;

    public Material myMat;

    public State myState;
    public int myHealth;

    public float RageTime { get; set; }
    public float IdleTime { get; set; }
    public float TaggedTime { get; set; }

    public bool KillGhost { get; set; }
    public bool KillPlayer { get; set; }
    public bool RescueGhost { get; set; }
    public bool GotRescuedGhost { get; set; }
    public bool InFleeZone { get; set; }

    protected Dictionary<string, State> myStateDictionary = new Dictionary<string, State>();

    public void Start()
    {
        SpawnAtRandom_Check();
        Unit_Check();
    }

    void Update()
    {
        if (myState != null) {
            myState.OnUpdate(this);
        }

        if (myHealth <= 0) {
            ChangeState("Tagged");
        }

        if (!target.activeInHierarchy) {
            NewRoute();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target) {
            NewRoute();
        }

        if (other.gameObject == spawn) {
            InFleeZone = true;
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Targetable") {
            collision.collider.GetComponent<Idamagable>().GiveDamage(1);
            collision.collider.GetComponent<Iconsumable>().Eat(true);
        }

        if (KillPlayer == true) {
            if (collision.collider.tag == "Player") {
                collision.collider.GetComponent<Idamagable>().GiveDamage(1);
            }
        }

        if (collision.collider.tag == "Ghost") {
            if (KillGhost == true) {
                collision.collider.GetComponent<Idamagable>().GiveDamage(1);
            }
            if(RescueGhost == true) {
                collision.collider.GetComponent<IRescueAble>().Rescue(true);
            }
        }
    }

    public void NewRoute()
    {
        NewPos.x = Random.Range(minX, maxX);
        NewPos.z = Random.Range(minZ, maxZ);
        NewPos.y = 0.5f;
        target.SetActive(true);
        target.transform.position = NewPos;
        PathRequestManager.RequestPath(transform.position, target.transform.position, OnPathFound);
    }

    public void ChangeState(string stateName)
    {
        if (myState != null) {
            myState.OnExit(this);
        }
        if (myStateDictionary.ContainsKey(stateName)) {
            myState = myStateDictionary[stateName];
            myState.OnEnter(this);
        }
    }

    public void GiveDamage(int damage)
    {
        myHealth -= damage;
    }

    public void Rage(bool startRaging)
    {
        if (startRaging == true) {
            ChangeState("Rage");
        }
    }

    public void Rescue(bool GetUp)
    {
        GotRescuedGhost = GetUp;
    }
}
