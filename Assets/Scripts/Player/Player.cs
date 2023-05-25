using System;
using DefaultNamespace.Gui;
using DefaultNamespace.Items;
using DefaultNamespace.SaveSystem;
using DefaultNamespace.State;
using DefaultNamespace.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour, ISavableGetter
    {
        // Object`s for general purposes
        [SerializeField] private Transform _objectTransform;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;
        
        // Player`s handlers and settings
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private Vector3 _healthBarOffset;
        [SerializeField] private HealthPoints _healthPoints;
        private PlayerStateController _controller;
        private PlayerSavable _savable;

        // Probably can be Equipment(?) -> Stash, Weapon
        public Stash stash;
        
        public Weapon weapon;
        public Transform weaponPivot;
        
        // Porperties
        public PlayerStateController GetController => _controller;
        public Rigidbody2D GetRigidbody2D => _rigidbody2D;
        public Animator GetAnimator => _animator;
        
        public PlayerStats GetStats => _stats;
        public ISavable GetSavable => _savable;
        
        public Transform GetObject => _objectTransform;
        public HealthPoints GetHealthPoints => _healthPoints;

        public event Action OnUpdate;

        private void Awake()
        {
            _savable = new PlayerSavable(this);
        }

        private void Start()
        {
            
            _controller ??= new PlayerStateController(this);
            OnUpdate += _controller.Update;

            stash.OnChange += GuiHandler.Instance.inventory.UpdateCells;
            _healthPoints.OnZero += GameOver;
            weapon.Equip(this);

            Application.targetFrameRate = 60;
        }

        private void GameOver()
        {
            SaveSystem.SaveHandler.DropSave();
            GuiHandler.Instance.healthBars.RemoveHealthBar(gameObject);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Update()
        {
            OnUpdate?.Invoke();
            GuiHandler.Instance.healthBars.UpdateHealthBar(gameObject, _healthBarOffset, _healthPoints.GetHealth / _healthPoints.GetMaxHealthPoints);
        }

        public void TakeDamage(float amount)
        {
            _healthPoints.Increase(-amount);
        }
    }
}