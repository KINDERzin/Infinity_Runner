namespace InfinityRunner;

public partial class MainPage : ContentPage
{
	bool estaMorto = false;
	bool estaPulando = false;

	const int tempoEntreFrames = 25;

	int velocidade = 0;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int larguraJanela = 0;
	int alturaJanela = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
	}

	void CalculaVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade = (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var a in layerFundo.Children)
			(a as Image).WidthRequest = w;
		foreach (var b in layerAsfalto.Children)
			(b as Image).WidthRequest = w;

		layerFundo.WidthRequest = w * 1.5;
		layerAsfalto.WidthRequest = w * 1.5;
	}
}

