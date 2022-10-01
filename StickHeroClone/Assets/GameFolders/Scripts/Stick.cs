using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Stick : MonoBehaviour
    {
        Manager _manager;
        public bool _isOk;
        [SerializeField] Transform _rayPos;
        [SerializeField] LayerMask _layerMask;
        [SerializeField] float _maxDistance = 10f;
        private void Start() {
            _manager = GameObject.Find("Manager").GetComponent<Manager>();
        }
        private void Update() {
            if(this.gameObject == _manager._stick) CheckIsOk();
        }
        void CheckIsOk(){
            RaycastHit2D hit = Physics2D.Raycast(_rayPos.position, _rayPos.forward, _maxDistance, _layerMask);
            Debug.DrawRay(_rayPos.position, _rayPos.forward * _maxDistance, Color.red);
            
            if(!_manager.IsGrowing){
                if(hit.collider != null && hit.collider.gameObject == _manager._nextBlock){
                    _isOk = true;
                }else{
                    _isOk = false;
                }
            }
            
        }
    }
}
