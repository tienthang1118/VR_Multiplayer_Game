using System;

[Serializable]
public class TagGamePlayerModel
{
    public bool isChaser;
    public TagGamePlayerModel(bool isChaser)
    {
        this.isChaser = isChaser;
    }
}
