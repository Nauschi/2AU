using Assets.Source.GameSettings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Scripts.ItemScripts
{
    class TrapHider : MonoBehaviour
    {
        UsedItem usedItem = null;

        public void HideTrap(UsedItem item)
        {
            usedItem = item;
            StartCoroutine(MakeTheTrapGoWoosh());
        }

        IEnumerator MakeTheTrapGoWoosh()
        {
            yield return new WaitForSeconds(GameConstants.TRAP_HIDETIME);

            usedItem.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
