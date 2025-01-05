using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Itens
{

    public class ItemLayout : MonoBehaviour
    {
        private ItensSetup _curSetup;

        [Header("UI")]
        public Image uiIcon;
        public TextMeshProUGUI uiValue;

         public void Load(ItensSetup setup)
        {
            _curSetup = setup;
            UpdateUi();
        }

        private void UpdateUi()
        {
            uiIcon.sprite = _curSetup.icon;
        }

        private void Update()
        {
            uiValue.text = _curSetup.soInt.value.ToString();
        }
    }
}
