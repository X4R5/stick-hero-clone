using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CreateBlock : MonoBehaviour
    {
        [SerializeField] GameObject _blockPrefab;
        [Range(0f,20f)] [SerializeField] float _minX, _maxX;
        Manager _manager;
        private void Start() {
            _manager = GetComponent<Manager>();
            Create();
        }

        public void Create(){
            var newPosition = new Vector3(Random.Range(_manager._currentBlock.transform.position.x + _minX, _manager._currentBlock.transform.position.x + _maxX), -3.77f, 0);
            var newBlock = Instantiate(_blockPrefab, newPosition, Quaternion.identity);
            _manager._nextBlock = newBlock;
        }
    }
}
