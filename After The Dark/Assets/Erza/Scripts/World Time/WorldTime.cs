using UnityEngine;
using System.Collections;
using System;

namespace WorldTime
{
    
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        
        [SerializeField]
        private float _daylength;

        private TimeSpan _currentTime;
        private float _minuteLenth => _daylength /WorldTimeConstant.minutesInDay;

        private void Start()
        {
            StartCoroutine(AddMinute());
        }
        private IEnumerator AddMinute()
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, _currentTime);
            yield return new WaitForSeconds(_minuteLenth);
            StartCoroutine(AddMinute());
        }
    }
}
