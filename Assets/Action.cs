using UnityEngine;

public abstract class Action
{
    public GameObject Target { get; set; }

    public abstract void Redo(); //ctrl + y

    public abstract void Undo(); //ctrl + z
}

public class MoveAction : Action
{
    public Vector3 BeforePosition;
    public MoveAction(GameObject target, Vector3 beforePosition)
    {
        Target = target;
        BeforePosition = beforePosition;
    }
    
    public override void Redo()
    {
        (Target.transform.localPosition, BeforePosition) = (BeforePosition, Target.transform.localPosition);
    }

    public override void Undo()
    {
        (Target.transform.localPosition, BeforePosition) = (BeforePosition, Target.transform.localPosition);
    }
}

public class CreateAction : Action
{
    public CreateAction(GameObject gameObject)
    {
        Target = gameObject;
    }
    
    public override void Redo()
    {
        Target.SetActive(true);
    }

    public override void Undo()
    {
        Target.SetActive(false);
    }
}
