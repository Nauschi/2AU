using System;

public class AnimatorConstant
{
    public string Name { get; }
    public Type Type { get; }

    public static AnimatorConstant Horizontal = new AnimatorConstant("Horizontal", typeof(float));
    public static AnimatorConstant Vertical = new AnimatorConstant("Vertical", typeof(float));
    public static AnimatorConstant Speed = new AnimatorConstant("Speed", typeof(float));
    public static AnimatorConstant LastInput = new AnimatorConstant("LastInput", typeof(int));

    public AnimatorConstant(string name, Type type) 
    {
        Name = name;
        Type = type;
    } 
}
