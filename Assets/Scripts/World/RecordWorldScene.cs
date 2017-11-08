using UnityEngine;

namespace Assets.Scripts.World
{
    public class RecordWorldScene : MonoBehaviour {

        // Use this for initialization
        void Start () {
            WorldStateManager.Instance.RecordCurrentWorldScene();
            
        }
	
    }
}
