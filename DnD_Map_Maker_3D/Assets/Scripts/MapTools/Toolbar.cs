using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MapTools
{
    /// <summary>
    /// Controller for the toolbar that is used to select the tools
    /// </summary>
    public class Toolbar : MonoBehaviour
    {
        /// <summary>
        /// The mesh spawner. Some tools need it
        /// </summary>
        [FormerlySerializedAs("PlaneSpawner")] public PlaneSpawner planeSpawner;
        /// <summary>
        /// Currently selected tool
        /// </summary>
        public static IMapTool MapTool;
        /// <summary>
        /// All map tools that can be selected
        /// </summary>
        [FormerlySerializedAs("Buttons")] public List<Button> buttons;
        /// <summary>
        /// The object controller. Some tools need it
        /// </summary>
        [FormerlySerializedAs("Controller")] public ObjectController controller;
        /// <summary>
        /// The container for the models on the right side of the screen
        /// </summary>
        [FormerlySerializedAs("ModelContainer")] public GameObject modelContainer;
        /// <summary>
        /// The color picker used in the paint tool
        /// </summary>
        [FormerlySerializedAs("ColorPicker")] public GameObject colorPicker;
    
        /// <summary>
        /// Tool handler for the select tool
        /// </summary>
        /// <param name="playerCamera">Player camara the tool needs</param>
        public void OnSelect(Camera playerCamera)
        {
            ClearButtons();
            MapTool = new Select(playerCamera);
            buttons[0].GetComponent<Image>().color = Color.gray;
        }

        /// <summary>
        /// Tool handler for the Extrude tool
        /// </summary>
        public void OnExtrude()
        {
            ClearButtons();
            MapTool = new Extrude(planeSpawner);
            buttons[1].GetComponent<Image>().color = Color.gray;
        }

        /// <summary>
        /// Tool handler for the intrude tool
        /// </summary>
        public void OnIntrude()
        {
            ClearButtons();
            MapTool = new Intrude(planeSpawner);
            buttons[2].GetComponent<Image>().color = Color.gray;
        }

        /// <summary>
        /// Tool handler for the place tool
        /// </summary>
        public void OnPlace()
        {
            ClearButtons();
            MapTool = new Place(controller);
            buttons[3].GetComponent<Image>().color = Color.gray;
            modelContainer.SetActive(true);
        }

        /// <summary>
        /// Tool handler for the paint tool
        /// </summary>
        /// <param name="playerCamera">Player camara the tool needs</param>
        public void OnPaint(Camera playerCamera)
        {
            ClearButtons();
            MapTool = new Paint(playerCamera, colorPicker);
            buttons[4].GetComponent<Image>().color = Color.gray;
            colorPicker.SetActive(true);
        }
        /// <summary>
        /// Sets the toolbar and other things to before a tool was selected
        /// </summary>
        private void ClearButtons()
        {
            if (MapTool is Select select)
            {
                if (select.Selected != null)
                {
                    select.Selected.transform.GetChild(1).transform.gameObject.SetActive(false);       
                }
            }
            for (var index = 0; index < buttons.Count; index++)
            {
                var button = buttons[index];
                button.GetComponent<Image>().color = Color.white;
            }

            if (modelContainer != null)
            {
                modelContainer.SetActive(false);
            }
        
            colorPicker.SetActive(false);
        } 
    }
}
