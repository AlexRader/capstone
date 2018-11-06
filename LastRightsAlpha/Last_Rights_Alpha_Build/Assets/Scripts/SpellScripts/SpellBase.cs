using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    enum StatisEffects {Stun};

    public GameObject aimReticle;

    public float resetTimerMax, timerMax, radius;
    [SerializeField]
    protected int damage;

    [SerializeField]
    float timer;

    [SerializeField]
    protected bool casting;

    [SerializeField]
    bool startResetTimer; 

    [SerializeField]
    float resetTimer;

    [SerializeField]
    Transform caster;

    [SerializeField]
    GameObject uiElement;

    public void Init()
    {
        timer = timerMax;
        resetTimer = resetTimerMax;
        caster = transform.parent;
        startResetTimer = false;
        casting = false; 
    }
    // overrideable aim call for all spells
    public virtual void Aim(float x, float y) { }

    //overrideable timer for spell cast durration
    public virtual void Timer(bool effected) { }

    //break the spell cast
    public virtual void Cancel() { }

    //cast spell
    public virtual void castSpell() { }

    //setters and getters for the vars
    public void setTimer(float dt) { timer += dt; }

    public void setCaster(Transform obj) { caster = obj; }

    public float getTimer() { return timer; }

    public Transform getCaster() { return caster; }

    public void setResetTimer() { startResetTimer = !startResetTimer; }
    public bool getResetTimer() { return startResetTimer; }

    public void setCountDown(float dt) { resetTimer += dt; }
    public float getCountDown() { return resetTimer; }
    public virtual void setStartCast() { casting = !casting; }
    public virtual bool getStartCast() { return casting; }
    public void setuiElement(GameObject obj) { uiElement = obj; }
    public GameObject getuiElement() { return uiElement; }
    public void sendUIMessage(float timer) { Debug.Log(timer);  uiElement.SendMessage("resetTimer", timer); }

}
