using System;

[Serializable]
public class TagGameRoleModel
{
    public float timeToChase;
    public TagGameRoleModel(float time)
    {
        timeToChase = time;
    }
    public TagGameRoleModel() { }
}
