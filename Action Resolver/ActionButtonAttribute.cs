using System;

public class ActionButtonAttribute : Attribute
{
    public string action;
    
    public ActionButtonAttribute (string action)
    {
        this.action = action;
    }
}


