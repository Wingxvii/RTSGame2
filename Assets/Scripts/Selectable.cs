using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType {
    None,
    Wall,
    Barracks,
    Droid,
    Player,
    Turret,
}
/*
This is a simple template class for all objects selectable by the RTS player.
All RTS-Controled gameobjects will inherit this class
 
     
 */
public class Selectable : MonoBehaviour
{
    //the selection effect
    public Behaviour halo;
    //personal id
    public int id;
    //object type
    public EntityType entity;
    //destructability
    public bool indestructable = false;

    // Start is called before the first frame update
    void Start()
    {
        halo = (Behaviour)this.GetComponent("Halo");
        halo.enabled = true;
        //call base function
        BaseStart();
    }

    private void Update()
    {
        //call base function
        BaseUpdate();
    }

    private void FixedUpdate()
    {
        //call base function
        BaseFixedUpdate();
    }
    public void OnDestroy()
    {
        //call base function
        BaseOnDestory();
    }

    public void OnSelect()
    {
        Debug.Log("Selected");
        if (halo != null) { halo.enabled = true; }
    }
    public void OnDeselect()
    {
        Debug.Log("Deselected");
        halo.enabled = false;
    }

    public void ResetValues()
    {
        this.gameObject.transform.position = Vector3.zero;
        this.gameObject.transform.rotation = Quaternion.identity;

        BaseResetValues();
    }

    //base class overrides
    protected virtual void BaseStart() { }
    protected virtual void BaseUpdate() { }
    protected virtual void BaseFixedUpdate() { }
    protected virtual void BaseOnDestory() { }
    protected virtual void BaseResetValues() { }


}
