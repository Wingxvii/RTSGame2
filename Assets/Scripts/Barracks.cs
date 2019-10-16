using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barracks : Selectable
{
    public Slider buildProcess;
    public Queue<float> buildTimes;
    public float currentBuildTime;

    //inherited function realizations
    protected override void BaseStart() {
        entity = EntityType.Barracks;
        buildProcess = GetComponentInChildren<Slider>();
        buildProcess.gameObject.SetActive(false);
    }

    protected override void BaseUpdate()
    {
        if (currentBuildTime < 0 && buildTimes.Count > 0) {
            DroidManager.Instance.QueueFinished(this, DroidType.Base);
            currentBuildTime += buildTimes.Dequeue();
        }
        if (currentBuildTime > 0)
        {
            currentBuildTime -= Time.deltaTime;
        }
    }

    public void OnRequestBuild() {
        if (ResourceManager.Instance.credits > 100) {
            ResourceManager.Instance.credits -= 100;

            buildTimes.Enqueue(DroidManager.Instance.RequestQueue(DroidType.Base));
        }
    }

    //child-sepific functions
}
