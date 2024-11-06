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
	int velocidade4 = 0;
	int larguraJanela = 0;
	int alturaJanela = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	async Task Desenha()
	{
		while(!estaMorto)
		{
			GerenciaCenarios();
			await Task.Delay(tempoEntreFrames);
		}
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
		velocidade3 = (int)(w * 0.007);
		velocidade4 = (int)(w * .009);
		velocidade = (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var a in layerUm.Children)
			(a as Image).WidthRequest = w;

		foreach (var a in layerDois.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in layerTres.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in layerQuatro.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in layerSemaforo.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in layerAsfalto.Children)
			(a as Image).WidthRequest = w;

		layerUm.WidthRequest = w * 1.5;
		layerDois.WidthRequest = w * 1.5;
		layerTres.WidthRequest = w * 1.5;
		layerQuatro.WidthRequest = w * 1.5;
		layerSemaforo.WidthRequest = w * 1.5;
		layerAsfalto.WidthRequest = w * 1.5;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(layerUm);
		GerenciaCenario(layerDois);
		GerenciaCenario(layerTres);
		GerenciaCenario(layerQuatro);
		GerenciaCenario(layerSemaforo);
		GerenciaCenario(layerAsfalto);		
	}

	void MoveCenario()
	{
		layerUm.TranslationX -= velocidade1;
		layerDois.TranslationX -= velocidade2;
		layerTres.TranslationX -= velocidade3;
		layerQuatro.TranslationX -= velocidade4;
		layerSemaforo.TranslationX -= velocidade3;
		layerAsfalto.TranslationX -= velocidade;
	}

	void GerenciaCenario(HorizontalStackLayout hsl)
	{
		var view = (hsl.Children.First() as Image);

		if(view.WidthRequest + hsl.TranslationX < 0)
		{
			hsl.Children.Remove(view);
			hsl.Children.Add(view);
			hsl.TranslationX = view.TranslationX;
		}
	}
}

