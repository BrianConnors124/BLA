using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LongRangeAttackBase
{
    protected float damage;
    protected float speed;
    protected Vector2 targetLocation;

    protected Vector2 origin;
    protected Entity player;

    protected List<GameObject> objList;
    
    
    
    protected LongRangeAttackBase()
    {
        
    }

    public virtual void SendObject(Vector2 origin, Entity player, GameObject projectile)
    {
        this.origin = origin;
        this.player = player;
        damage = projectile.GetComponent<Projectile>().damage;
        speed = projectile.GetComponent<Projectile>().speed;
        targetLocation = player.Location;
        objList.Add(projectile);
    }






















}
