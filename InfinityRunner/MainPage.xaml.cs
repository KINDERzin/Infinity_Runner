namespace InfinityRunner;

public partial class MainPage : ContentPage
{
	Player player;
	Inimigos inimigos;

	bool estaMorto = false;
	bool estaNoChao = true;
	bool estaPulando = false;
	bool estaNoAr = false;

	const int tempoEntreFrames = 25;
	const int forcaGravidade = 6;
	const int forcaPulo = 8;
	const int maxTempoPulando = 6;
	const int MaxTempoNoAr = 4;

	int velocidade = 0;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velocidade4 = 0;
	int larguraJanela = 0;
	int alturaJanela = 0;
	int tempoPulando = 0;
	int tempoNoAr = 0;

	public MainPage()
	{
		InitializeComponent();
		player = new Player(ImgCarro);
		player.Run();
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
			if(inimigos != null)
				inimigos.Desenha(velocidade);	
			if(!estaPulando && !estaNoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
			else 
				AplicaPulo();
	
			await Task.Delay(tempoEntreFrames);
		}
	}

	void AplicaGravidade()
	{
		if(player.GetY() < 0)
			player.MoveY(forcaGravidade);
		else if(player.GetY() >= 0)
		{
			player.SetY(0);
			estaNoChao = true;
		}	
	}

	void AplicaPulo()
	{
		estaNoChao = false;
		if(estaPulando && tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			estaNoAr = true;
			tempoNoAr = 0;
		}
		else if(estaNoAr && tempoNoAr >= maxTempoPulando)
		{
			estaPulando = false;
			estaNoAr = false;
			tempoPulando = 0;
			tempoNoAr = 0;
		}
		else if(estaPulando && tempoPulando < maxTempoPulando)
		{
			player.MoveY(-forcaPulo);
			tempoPulando++;
		}
		else if(estaNoAr)
			tempoNoAr++;
	}

    void OnGridClicked(object sender, TappedEventArgs e)
    {
        if(estaNoChao)
            estaPulando = true;
    }

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);

		inimigos = new Inimigos(-w);
		inimigos.Add(new Inimigo(pedra));
		inimigos.Add(new Inimigo(pedradois));
		inimigos.Add(new Inimigo(tronco));
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
		
		foreach (var a in layerCinco.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in layerAsfalto.Children)
			(a as Image).WidthRequest = w;

		layerUm.WidthRequest = w * 1.5;
		layerDois.WidthRequest = w * 1.5;
		layerTres.WidthRequest = w * 1.5;
		layerQuatro.WidthRequest = w * 1.5;
		layerCinco.WidthRequest = w * 1.5;
		layerAsfalto.WidthRequest = w * 1.5;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(layerUm);
		GerenciaCenario(layerDois);
		GerenciaCenario(layerTres);
		GerenciaCenario(layerQuatro);
		GerenciaCenario(layerCinco);
		GerenciaCenario(layerAsfalto);		
	}

	void MoveCenario()
	{
		layerUm.TranslationX -= velocidade1;
		layerDois.TranslationX -= velocidade2;
		layerTres.TranslationX -= velocidade3;
		layerQuatro.TranslationX -= velocidade4;
		layerCinco.TranslationX -= velocidade3;
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