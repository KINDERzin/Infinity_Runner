namespace InfinityRunner;

public class Inimigo
{
    Image imageView;

    public Inimigo(Image a)
    {
        imageView = a;
    }

    public void MoveX(double s)
    {
        imageView.TranslationX -=s;
    }

    public double GetX()
    {
        return imageView.TranslationX;
    }

    public void Reset()
    {
        imageView.TranslationX = 500;
    }
}