using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum CanvasType
{
    MainMenu,
    GameUI

}
public class CanvasManager : SingletonDontDestroy<CanvasManager>
{
    List<CanvasController> canvasControllers;
    CanvasController lastActiveCanvas;

    public override void Awake()
    {
        base.Awake();

        canvasControllers = GetComponentsInChildren<CanvasController>().ToList();

        canvasControllers.ForEach(x => x.gameObject.SetActive(false));

        SwitchCanvas(CanvasType.MainMenu);
    }

    public void SwitchCanvas(CanvasType _type)
    {
        if(lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = canvasControllers.Find(x => x.canvasType == CanvasType.MainMenu);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else
        {
            Debug.LogWarning("The main menu was not found in the list");
        }
    }
}
