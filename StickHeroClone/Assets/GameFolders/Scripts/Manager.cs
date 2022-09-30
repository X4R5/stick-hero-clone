using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] GameObject _player, _panel;
        public GameObject _currentBlock, _nextBlock, _stick;
        [SerializeField] float _growSpeed, _rotationSpeed, _moveSpeed;
        bool _readyToRotate, _needUpdate;
        public bool _isGrowing;
        CreateBlock _cb;
        Vector3 _targetPos;
        Stick _stickSc;
        [SerializeField] Transform _cam;
        GameObject _previousBlock;
        private void Awake() {
            _currentBlock = GameObject.Find("Block");
        }
        private void Start() {
            _player = GameObject.Find("Player");
            _panel.SetActive(false);
            _targetPos = _player.transform.position;
            _cb = GetComponent<CreateBlock>();
            _stick = _currentBlock.transform.GetChild(1).gameObject;
            _stickSc = _stick.GetComponent<Stick>();
            //_stick.SetActive(false);
        }

        private void Update() {
            _player.transform.position = Vector3.Lerp(_player.transform.position, _targetPos, _moveSpeed * Time.deltaTime);
            // if(Input.GetMouseButton(0) && (!_stick.activeInHierarchy || _isGrowing)){
            //     _isGrowing = true;
            //     var stickSprite = _stick.transform.GetChild(0);
            //     _stick.SetActive(true);
            //     stickSprite.transform.localScale += new Vector3(0, 0.02f, 0);
            //     stickSprite.transform.localPosition += new Vector3(0, 0.01f, 0);
            //     _readyToRotate = true;
            // }else if(_stick.activeInHierarchy && _readyToRotate){
            //     _isGrowing = false;
            //     var newRotation = Quaternion.AngleAxis(90, Vector3.down);
            //     _stick.transform.rotation = Quaternion.Slerp(_stick.transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
            //     _readyToRotate = false;            
            // }
            if(Input.GetMouseButton(0)){
                _isGrowing = true;
                _stick.SetActive(true);
            }else if(_isGrowing){
                _isGrowing = false;
                _readyToRotate = true;
            }
            if(_needUpdate){
                //Debug.Log("need update worked");
                if(_stickSc._isOk){
                    UpdateCurrentBlock();
                    _cb.Create();
                    _needUpdate = false;
                }else{
                    var rb = _stick.GetComponent<Rigidbody2D>();
                    var body = _stick.transform.GetChild(0);
                    body.GetComponent<SpriteRenderer>().color = Color.red;
                    rb.gravityScale = 1;
                    _panel.SetActive(true);
                }
            }
            // if(_stick.transform.localRotation.z != 0 && !_readyToRotate){
            //     _needUpdate = true;
            // }
            if(_targetPos.x - _player.transform.position.x <= 0.015f){
                _cam.transform.position = Vector3.Lerp(_cam.transform.position, new Vector3(_player.transform.position.x + 7.5f, 0, -10f), _moveSpeed / 100);
                Destroy(_previousBlock, 1f);
            }
        }
        private void FixedUpdate() {
            if(_isGrowing){
                var stickSprite = _stick.transform.GetChild(0);
                stickSprite.transform.localScale += new Vector3(0, (_growSpeed / 10) * 2 * Time.deltaTime, 0);
                stickSprite.transform.localPosition += new Vector3(0, _growSpeed / 10 * Time.deltaTime, 0);
            }
            if(_readyToRotate && _stick.activeInHierarchy){
                var newRotation = Quaternion.AngleAxis(90.02f, Vector3.back);
                //var newRotation = _stick.transform.GetChild(1).transform.rotation;
                _stick.transform.rotation = Quaternion.Slerp(_stick.transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
                if( _stick.transform.rotation == newRotation ){
                    _readyToRotate = false;
                    _needUpdate = true;
                }
            }
        }
        void UpdateCurrentBlock(){
            _previousBlock = _currentBlock;
            _targetPos = new Vector3(_nextBlock.transform.localPosition.x, _player.transform.position.y, _player.transform.position.z);
            _currentBlock = _nextBlock;
            _stick = _currentBlock.transform.GetChild(1).gameObject;
            _stickSc = _stick.GetComponent<Stick>();
            _stick.SetActive(false);
        }

        public void RestartGame(){
            SceneManager.LoadScene("Game");
        }
    }
}
