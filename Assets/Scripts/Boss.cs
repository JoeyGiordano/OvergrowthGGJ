using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    #region Singleton
    private static Boss _Boss;

    public static Boss Instance { get { return _Boss; } }


    private void Awake()
    {
        if (_Boss != null && _Boss != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _Boss= this;
        }
    }
    #endregion

    private int randomAnimation;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("halfHp", false);
        animator.SetInteger("random", 0);
    }

    // Update is called once per frame
    void Update()
    {
        randomAnimation = Random.Range(0, 2);
        animator.SetInteger("random", randomAnimation);
        
    }

    public void setIsHalfHp(bool hp){
        print("setting boss to half hp animation");
        animator.SetBool("halfHp", hp);
    }
}
