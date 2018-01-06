﻿using System;
using System.Collections;
using System.Collections.Generic;

public enum Language
{
	ENGLISH,
	TURKISH
}

public static class StringLiterals
{
	#region Main Menu Panel

	public static string[] gameNameText =
	{
		"Follow The Numbers",
		"Sayıları Takip Et"
	};

	public static string[] playButton =
	{
		"Play",
		"Oyna"
	};
		
	public static string[] settingsButton =
	{
		"Settings",
		"Ayarlar"
	};

	public static string[] exitButton =
	{
		"Exit",
		"Çıkış"
	};

	public static string[] mainMenuInfo =
	{
		"Source Code In Github",
		"Kaynak Kod Github'da"
	};
    public static string[] yesButton =
    {
        "Yes",
        "Evet"
    };
    public static string[] noButton =
   {
        "No",
        "Hayır"
    };
    public static string[] exitQuestionText =
   {
        "Are you sure?",
        "Çıkmak istiyor musunuz?"
    };
    #endregion

    #region Select Mode Panel

    public static string[] classicButton =
	{
		"Classic",
		"Klasik"
	};

	public static string[] dontForgetButton =
	{
		"Don't Forget",
		"Sakın Unutma"
	};

	public static string[] noMistakeButton =
	{
		"No Mistake",
		"Hata Yok"
	};

	#endregion

	#region Settings Panel

	public static string[] settingsText =
	{
		"Settings",
		"Ayarlar"
	};

	public static string[] musicText =
	{
		"Sound",
		"Ses"
	};
    public static string[] sfxText =
    {
        "SFX",
        "Özel Efektler"
    };
    public static string[] creditsButton =
	{
		"Credits",
		"Hakkında"
	};

	public static string[] resetProgressButton =
	{
		"Reset",
		"Sıfırla"
	};

    public static string[] creditsText =
    {
        "This game is our first released game." +
			"\n\nCoders:\n    Şamil 'Softsam' Özçelik\n    Melih Tunç" +
            "\n\nGraphics:\n    Mehmet Murat Yılmaz" +
			"\n\nSounds:\n    Melih Tunç" +
            "\n\nDesign:\n    Melih Tunç\n    Şamil 'Softsam' Özçelik\n    Mehmet Murat Yılmaz",
        "Bu oyun bizim ilk yayınlanan oyunumuz." +
			"\n\nYazılım: \n    Şamil 'Softsam' Özçelik\n    Melih Tunç" +
			"\n\nGrafiker:\n    Mehmet Murat Yılmaz" +
			"\n\nSesler:\n    Melih Tunç" +
			"\n\nTasarım:\n    Melih Tunç\n    Şamil 'Softsam' Özçelik\n    Mehmet Murat Yılmaz",
    };
    #endregion

    #region Level


    public static string[] levelText =
	{
		"LEVEL",
		"LEVEL"
	};



	public static string[] quitButton =
	{
		"Quit",
		"Çıkış"
	};

	public static string[] levelCompletedText =
	{
		"RESULT",
		"SONUÇ"
	};

	public static string[] backButton =
	{
		"Back",
		"Geri"
	};

	public static string[] nextButton =
	{
		"Next",
		"İleri"
	};

	public static string[] restartButton =
	{
		"Restart",
		"Tekrar"
	};
   
    public static List<string[]> scripts = new List<string[]>()
    {
       new string[] {
        "Hello!\n"+
        "Welcome to follow the numbers. This instruction will teach you what to do in gameplay.",
        "It is a simple game to play. All you have to do is following the numbers.",
        "Okay! I am joking but it is right."+
        "In every card there is a number.",
        "You have to open them in the correct order.",
        "If you don't forget what you opened before, you may have 3 stars.",
        "If you would like to see the cards, you have one chance in every level! Touch the number you see at top! Yeah the big one...",
        "After that you can have another chance by touching that button. But you have to watch an Advertisement video."+
            "If you skip ads you can look at cards 2 seconds. If you watch it all you will have 5 seconds",
        "You can use this button in every mode.",
        "There are 3 different modes. Classic, Don't Forget, No Mistake",
        "\t Classic \n"+
            "In this mode, there are too many variables for calculating stars. You are responsible for the passed time and your wrong tries.",
       "\t Don't Forget \n"+
            "In this mode, You have memorise cards because you are responsible for your wrong tries. Less mistake more stars.",
       "\t No Mistake \n"+
            "In this mode, you can not make mistakes. Game will over with your first missclick.",
        "Have fun! And if you like it rate us on Google Play..." },
       new string[]  {
            "Merhaba!\n"+
                "Follow the Numbers oyunumuza hoşgeldiniz! Bu tanıtım size oyunun nasıl oynandığı konusunda yardımcı olacak.",
        "Bu basit bir oyun. Tek yapmanız gereken sayıları takip etmeniz.",
        "Peki! Şakaydı ama bu konuda ciddiyim."+
        "Her kartta bir sayı var.",
        "Ve sen onları doğru sırada açmalısın.",
        "Eğer açtığın kartları unutmazsan 3 yıldız alabilirsin.",
        "Eğer kartları görmek istersen her levelde bir şansın var! Yukarıda gördüğün büyük sayıya dokun! Evet o büyük olan...",
        "Yine aynı yere dokunarak hak kazanabilirsin bu durumda reklam video'su izlemelisin"+
            "Eğer video'yu geçersen daha az süre kazanırsın. Tamamını izlediğin takdirde daha uzun süre kazanırsın.",
        "Bu işlevi her modda kullanabilirsin.",
        "3 farklı mod var. Klasik, Sakın unutma, Hata yok",
        "\t Klasik \n"+
            "Bu modda yıldız kazanmak için bir çok şeye dikkat etmen gerekecek. Her yaptığın hata ve geçen zaman senin yıldız sayını olumsuz etkileyecek.",
       "\t Sakın Unutma \n"+
            "Bu modda kartları daha dikkatli izleyip hatırlaman gerekecek. Süre problemi olmadan yaptığın hata sayısına göre yıldız kazanacaksın!",
       "\t Hata Yok \n"+
            "Bu mod fazla acımasızca tasarlandı. Hafızanızı zorlamanızı istiyoruz. Hata odaklı sistemde her ne kadar Sakın unutmayı hatırlatsada"+
                "ilk hatanızda oyun bitecektir..",
        "İyi eğlenceler! Eğer beğendiyseniz Google Play'de oylamayı ve yorum yapmayı unutmayın!"
        }

    };

    //
    public static string[] rewardScreenText =
    {
        "You can earn time by watching ads.",
        "Buradan reklam izleyerek zaman kazanabilirsiniz."
    };
    public static string[] rewardScreen4Sec =
   {
        "Earn 4 secs",
        "4 sn Kazan"
    };
    public static string[] rewardScreen8Sec =
   {
       "Earn 8 secs",
        "8 sn Kazan"
    };
    public static string[] rewardScreenNoButton =
   {
       "No, thanks",
        "Hayır, Teşekkürler"
    };
    #endregion

    #region levelPicker

    public static string[] easyText =
    {
        "Easy",
        "Kolay"
    };
    public static string[] mediumText =
    {
        "Medium",
        "Orta"
    };
    public static string[] hardText =
    {
        "Hard",
        "Zor"
    };
    #endregion
}
