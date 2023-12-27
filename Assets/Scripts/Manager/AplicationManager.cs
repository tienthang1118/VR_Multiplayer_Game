using System.Collections;
using System.Collections.Generic;
using Project.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Managers {
    public class ApplicationManager : MonoBehaviour {

        public void Start() {
            SceneManagementManager.Instance.LoadLevel(SceneList.MAIN_MENU, (levelName) => {
            });
        }
    }
}
