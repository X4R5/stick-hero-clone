using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] GameObject _panel;
        GameObject _currentBlock, _player, _previousBlock;
        public GameObject _nextBlock, _stick;
        [SerializeField] float _growSpeed, _rotationSpeed, _moveSpeed;
        bool _readyToRotate, _needUpdate, _isGrowing;
        public bool IsGrowing => _isGrowing;
        CreateBlock _cb;
        Vector3 _targetPos;
        Stick _stickSc;
        [SerializeField] Transform _cam;
        public GameObject CurrentBlock => _currentBlock;

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
        }

        private void Update() {

            Player.SetPosition(_player, _targetPos, _moveSpeed);

            if(Input.GetMouseButton(0)){
                _isGrowing = true;
                _stick.SetActive(true);
            }else if(_isGrowing){
                _isGrowing = false;
                _readyToRotate = true;
            }
            if(_needUpdate){
                
                if(_stickSc._isOk){
                    UpdateCurrentBlock();
                    _cb.Create();
                    _needUpdate = false;
                }else{
                    StickMovements.DestroyStick(_stick);
                    _panel.SetActive(true);
                }
            }

            if(_targetPos.x - _player.transform.position.x <= 0.015f){
                _cam.transform.position = Vector3.Lerp(_cam.transform.position, new Vector3(_player.transform.position.x + 7.5f, 0, -10f), _moveSpeed / 100);
                Destroy(_previousBlock, 1f);
            }
        }
        private void FixedUpdate() {
            if(_isGrowing){

                var stickSprite = _stick.transform.GetChild(0);
                StickMovements.Grow(stickSprite, _growSpeed);

            }
            if(_readyToRotate && _stick.activeInHierarchy){

                var newRotation = Quaternion.AngleAxis(90.02f, Vector3.back);
                StickMovements.Rotate(_stick, newRotation, _rotationSpeed);

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
