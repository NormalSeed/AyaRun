using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DesignPattern
{
    /// <summary>
    /// 값이 변경되었을 때 씬에서 관찰 가능한 행동
    /// </summary>
    /// <param name="ObservableProperty"></param>
    public class ObservableProperty<T>
    {
        [SerializeField] private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;
                _value = value;
                Notify();
            }
        }

        private UnityEvent<T> _onValueChanged = new();

        public ObservableProperty(T value = default)
        {
            _value = value;
        }

        public void Subscribe(UnityAction<T> action)
        {
            _onValueChanged.AddListener(action);
        }

        public void Unsubscribe(UnityAction<T> action)
        {
            _onValueChanged?.RemoveListener(action);
        }

        public void UnsubscribeAll(UnityAction<T> action)
        {
            _onValueChanged?.RemoveAllListeners();
        }

        public void Notify()
        {
            _onValueChanged?.Invoke(Value);
        }
    }
}
