using UnityEngine;

public class AnimationButtons : MonoBehaviour
{
    
    private ClickScripts _clickScripts;
    private Animator _animation;
    private bool _isBtnGas = false;
    private bool _isBtnBreak = false;



    private void Start()
    {
        _animation = GetComponent<Animator>();
        _clickScripts = GetComponent<ClickScripts>();
    }

    private void Update()
    {   
       _isBtnGas = _clickScripts.clickedIs;
       _isBtnBreak= _clickScripts.clickedIs;


        if (_isBtnGas == true)   
        {
            _animation.SetTrigger("gas");
        }
        else if (_isBtnGas == false)
        {
            _animation.SetTrigger("no_gas");
        }

        if (_isBtnBreak == true)   
        {
            _animation.SetTrigger("break");
        }
        else if (_isBtnBreak == false)
        {
            _animation.SetTrigger("no_break");
        }
    }
}
