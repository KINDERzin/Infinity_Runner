using FFImageLoading.Maui;

namespace InfinityRunner;

public delegate void CallBack();
public class Player : Animacao
{
    public Player(CachedImage a): base (a)
    {
        //Animaçao do carro andando
        for(int i = 1; i <= 4; i++)
            animacao1.Add($"carro{i.ToString("D2")}.png");
        //Animação da explosão
        for(int i = 1; i < 6; i++)
            animacao2.Add($"morreu{i.ToString("D2")}.png");
        SetAnimacaoAtiva(1);
    }

    public void Die()
    {
        loop = false;
        SetAnimacaoAtiva(2);
    }

    public void Run()
    {
        loop = true;
        SetAnimacaoAtiva(1);
        Play();
    }
}