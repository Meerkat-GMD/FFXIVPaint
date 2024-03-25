using UnityEngine;

public abstract class Action
{
    public GameObject Target { get; set; }

    public Action(GameObject target)
    {
        Target = target;
    }
    public abstract void Redo(); //ctrl + y

    public abstract void Undo(); //ctrl + z
}

public class MoveAction : Action
{
    public Vector3 BeforePosition;
    public MoveAction(GameObject target, Vector3 beforePosition) : base(target)
    {
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
    public CreateAction(GameObject target) : base(target)
    {
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

public class DrawAction : Action
{
    public DrawAction(GameObject target) : base(target)
    {
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

public class RotateAction : Action
{
    public float RotateAngle;
    public RotateAction(GameObject target, float rotateAngle) : base(target)
    {
        RotateAngle = rotateAngle;
    }
    
    public override void Redo()
    {
        var eulerAngles = new Vector3(0, 0,RotateAngle);
        float temp = Target.transform.eulerAngles.z;
        RotateAngle = temp;
        Target.transform.eulerAngles = eulerAngles;
    }

    public override void Undo()
    {
        var eulerAngles = new Vector3(0, 0,RotateAngle);
        float temp = Target.transform.eulerAngles.z;
        RotateAngle = temp;
        Target.transform.eulerAngles = eulerAngles;
    }
}