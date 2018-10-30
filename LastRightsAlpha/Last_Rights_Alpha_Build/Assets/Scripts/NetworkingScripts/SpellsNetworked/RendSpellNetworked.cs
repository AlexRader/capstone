using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendSpellNetworked : Photon.MonoBehaviour
{
    private PhotonView photonView;

    public Rigidbody2D rb;
    GameObject myParent;

    public Vector2 vspeed;
    int dmg;
    public float objVelocity;
    bool called;

    private Vector2 targetVelocity;
    float myRotation;
    // Use this for initialization
    void Start()
    {
        called = false;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("destroyTime");
        rb.velocity = vspeed.normalized * objVelocity;

    }

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (photonView.isMine)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
        else
        {
            SmoothMove();
        }
    }
    private void SmoothMove()
    {
        rb.velocity = targetVelocity;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetVelocity);
        rb.rotation = myRotation;
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(rb.velocity);
            stream.SendNext(rb.rotation);
        }
        else
        {
            targetVelocity = (Vector2)stream.ReceiveNext();
            myRotation = (float)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.isMine)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject != myParent)
            {
                if (!called)
                {
                    called = true;
                    collision.gameObject.GetComponent<Damage>().SendMessage("takeDamage", dmg);
                    photonView.RPC("RPC_DestroySpell", PhotonTargets.All);
                }
            }
            else if (collision.gameObject.tag == "Wall")
                photonView.RPC("RPC_DestroySpell", PhotonTargets.All);
        }

        if (collision.gameObject.tag == "Shield")
            rb.velocity *= -1;
    }

    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(12.0f);
        photonView.RPC("RPC_DestroySpell", PhotonTargets.All);
        //Destroy(gameObject);
    }
    void SetVelocity(Vector2 vec)
    {
        vspeed = vec;
    }
    void SetDamage(int setDamage)
    {
        dmg = setDamage;
    }
    void parentObj(GameObject var)
    {
        myParent = var;
    }

    [PunRPC]
    private void RPC_DestroySpell()
    {
        Debug.Log("yooo");
        if (photonView.isMine)
            PhotonNetwork.Destroy(gameObject);
    }
}

