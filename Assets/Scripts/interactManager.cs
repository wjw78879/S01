using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class interactManager : MonoBehaviour
{
    public EventSystem eventSystem;

    gameManager manager;

    public InteractLayer[] interactLayers;

    public string defaultPauseLayer;

    private Stack<GameObject> layerStack;

    private int pauseLayerNo;

    private void Start()
    {
        manager = FindObjectOfType<gameManager>();
        layerStack = new Stack<GameObject>();
        eventSystem.enabled = false;
    }

    public void PushNewLayer(string newLayerName)
    {
        foreach (InteractLayer layer in interactLayers)
        {
            if (layer.layerName == newLayerName)
            {
                if (layerStack.Count == 0)
                {
                    layerStack.Push(null);
                }
                else
                {
                    layerStack.Push(eventSystem.currentSelectedGameObject);
                }
                eventSystem.enabled = false;
                eventSystem.firstSelectedGameObject = layer.defaultSelect;
                eventSystem.enabled = true;
            }
        }
    }

    public void ReturnLayer()
    {
        if (layerStack.Count == 0)
        {
            Debug.LogError("Return layer when count == 0");
        }
        else if (layerStack.Count == 1)
        {
            layerStack.Pop();
            eventSystem.enabled = false;
        }
        else
        {
            eventSystem.enabled = false;
            eventSystem.firstSelectedGameObject = layerStack.Pop();
            eventSystem.enabled = true;
        }
    }

    public void Clear()
    {
        layerStack.Clear();
        eventSystem.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Cancel") && manager.canEsc)
        {
            if (manager.pause)
            {
                if (pauseLayerNo == layerStack.Count - 1)
                {
                    manager.Resume();
                }
                ReturnLayer();
            }
            else
            {
                pauseLayerNo = layerStack.Count;
                PushNewLayer(defaultPauseLayer);
                manager.Pause();
            }
            
        }
    }
}

[System.Serializable]
public class InteractLayer
{
    public string layerName;
    public GameObject defaultSelect;
}