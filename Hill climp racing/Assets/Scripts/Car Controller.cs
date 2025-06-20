using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{

    WheelJoint2D[] _wheelJoints;
    JointMotor2D _frontWheel;
    JointMotor2D _backWheel;
    



    [SerializeField] private float _maxSpeed = - 1000f;
    [SerializeField] private float _brakeForce =  3000f;
    [SerializeField] public Text _textMoney;
    [SerializeField] public int _money;
    [SerializeField] Image _petrolBar;
    [SerializeField] private int _petrolAmount = 100; 
    [SerializeField] private GameObject _lousePanel;
    [SerializeField] private float petrolDecreaseRate = 1f; 
    [SerializeField] private GameObject _mainCanvas;
    private float nextTime = 0f; 
    private float delayTime = 1f; 

    private float _gravityScale = 9.8f;
    private float _maxBackSpeed = 1500f;
    private float _acceliration = 250f;
    private float deacceliration = - 100f;
    private float _angleCar = 0;

    public ClickScripts[] ControlCar;


    private void Start()
    {   
        _petrolAmount = 40;
        _textMoney.text = $"{_money}";
        _wheelJoints = GetComponents<WheelJoint2D>();
        _frontWheel = _wheelJoints[1].motor;
        _backWheel = _wheelJoints[0].motor;
    }

    private void Update()
    {

        _petrolBar.fillAmount = _petrolAmount / 40f;

        

        if (Time.time >= nextTime && _petrolAmount >= 0)
        {
            nextTime += delayTime; 
            _petrolAmount--; 
        }

        _textMoney.text = $"{_money}";

        if ( _petrolAmount <= 0)
        {
            _lousePanel.SetActive(true);
            Debug.Log("Закончился Бензин");
            _mainCanvas.SetActive(false);
        }

    
    }


    private void FixedUpdate()
    {   


        _frontWheel.motorSpeed = _backWheel.motorSpeed;

        _angleCar = transform.localEulerAngles.z;

       
        if (_angleCar >= 180)
        {
            _angleCar = _angleCar - 360;
        }



        if (ControlCar[0].clickedIs == true)
        {
            _backWheel.motorSpeed = Mathf.Clamp(_backWheel.motorSpeed - (_acceliration - _gravityScale * Mathf.PI * (_angleCar / 180) * 80) * Time.fixedDeltaTime , _maxSpeed , _maxBackSpeed);
        }

        else if (ControlCar[0].clickedIs == false && _backWheel.motorSpeed < 0 || (ControlCar[0].clickedIs == false && _backWheel.motorSpeed == 0 && _angleCar < 0))
        {
            _backWheel.motorSpeed = Mathf.Clamp(_backWheel.motorSpeed - (deacceliration - _gravityScale * Mathf.PI * (_angleCar / 180) * 80) * Time.fixedDeltaTime , _maxSpeed , 0);
        }

        else if ((ControlCar[0].clickedIs == false && _backWheel.motorSpeed > 0) || (ControlCar[0].clickedIs == false && _backWheel.motorSpeed == 0 && _angleCar > 0))
        {
            _backWheel.motorSpeed = Mathf.Clamp(_backWheel.motorSpeed - (-deacceliration - _gravityScale * Mathf.PI * (_angleCar / 180) * 80) * Time.fixedDeltaTime , 0 , _maxBackSpeed);
        }

        
        if (ControlCar[1].clickedIs == true)
        {
            _backWheel.motorSpeed = Mathf.Clamp(_backWheel.motorSpeed - (_acceliration - _gravityScale * Mathf.PI * (_angleCar / 180) * 80) * Time.fixedDeltaTime , _brakeForce , 0);
        }

        else if ((ControlCar[1].clickedIs == false && _backWheel.motorSpeed > 0) || (ControlCar[1].clickedIs == false && _backWheel.motorSpeed == 0 && _angleCar > 0))
        {
            _backWheel.motorSpeed = Mathf.Clamp(_backWheel.motorSpeed - (-deacceliration - _gravityScale * Mathf.PI * (_angleCar / 180) * 80) * Time.fixedDeltaTime , 0 , _brakeForce);
        }
      


        // if (ControlCar[1].clickedIs == true && _backWheel.motorSpeed > 0)
        // {
        //     _backWheel.motorSpeed = Mathf.Clamp(_backWheel.motorSpeed - _brakeForce * Time.fixedDeltaTime , 0 , _maxBackSpeed);
        // }
        
        // else if (ControlCar[1].clickedIs == true && _backWheel.motorSpeed < 0)
        // {
        //     _backWheel.motorSpeed = Mathf.Clamp(_backWheel.motorSpeed + _brakeForce * Time.fixedDeltaTime , _maxSpeed , 0);
        // }



        _wheelJoints[1].motor = _backWheel;
        _wheelJoints[0].motor = _frontWheel;

    }

        private void OnTriggerEnter2D(Collider2D coll) 
        {
            if (coll.CompareTag("Money"))
            {
                Money moneyScript = coll.GetComponent<Money>();
                if (moneyScript != null)
                {
                    _money += moneyScript._moneyType;
                    Destroy(coll.gameObject);
                    Debug.Log($"+ {moneyScript._moneyType} монет");
                }
            }

            else if (coll.CompareTag("petrol"))
            {
                Destroy(coll.gameObject);
                Debug.Log("Пополнение бензином");
                _petrolAmount = 40;
            }
    }
    


    
}   