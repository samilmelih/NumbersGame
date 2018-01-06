using System;
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
        "Emin Misin?"
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
        "Welcome to Follow The Numbers. This instruction will teach you what to do in gameplay.",
        "It is a simple game to play. All you have to do is following the numbers.",
        "Okay! I am joking but it is right. " +
        "In every card there is a number.",
        "You have to open them in the correct order.",
        "If you don't forget what you opened before, you may have 3 stars.",
			"If you would like to see all cards, do not hesitate to touch Eye at down. Yeah the creepy one. You have to be careful about timing at the top left corner.",
			"The timing we are talking about is your time to look the all the cards. When you touch that Eye, you will realise that your time is running out.",
			"Final thing you have to be careful about this Eye, in one level everytime you touch that button your time will be faster. Be carefull about using this time.",
			"In the begining of the game you have 10 seconds but you can earn time. But how?",
			"For every level:\n\n1 Star\t1 sec\n2 Stars\t3 secs\n3 Stars\t5 secs",
			"You can not earn more than one for every level.",
			"We have three different modes.\n\nClassic, Don't Forget, and No Mistake",
		"-> Classic \n"+
            "In this mode, there are too many variables for calculating stars. You are responsible for the passed time and your wrong tries.",
		"-> Don't Forget \n"+
            "In this mode, You have memorise cards because you are responsible for your wrong tries. Less mistake more stars.",
		"-> No Mistake \n"+
            "In this mode, you can not make mistakes. Game will over with your first missclick.",
        "Have fun! And if you like it rate us on Google Play..." },
       new string[]  {
            "Merhaba!\n"+
                "Follow the Numbers oyunumuza hoşgeldiniz! Bu tanıtım size oyunun nasıl oynandığı konusunda yardımcı olacak.",
        "Bu basit bir oyun. Tek yapmanız gereken sayıları takip etmeniz.",
        "Peki! Şakaydı ama bu konuda ciddiyim. "+
        "Her kartta bir sayı var.",
        "Ve sen onları doğru sırada açmalısın.",
        "Eğer açtığın kartları unutmazsan 3 yıldız alabilirsin.",
        "Ayrıca tüm kartları görmek istersen aşağıdaki göze basmaktan çekinme fakat sol üstteki süreye dikkat etmelisin.",
        "Sol üstteki süre senin kartlara bakma süren. Göze bastığında azalmaya başladığını göreceksin.",
		"Gözü kullanırken dikkat etmen gereken son bir şey daha var, bu da göze basma sayısı arttığında sürenin daha hızlı azalıyor olması.",
		"Başlangıç olarak 10 saniyeye sahipsin bunu arttırmak senin elinde, nasıl mı?",
		"Her bir level için:\n\n1 Yıldız\t1 sn\n2 Yıldız\t3 sn\n3 Yıldız\t5 sn\n\nkazandıracaktır.",
		"Bu ödülleri her bir level için sadece bir kez kazanabileceğini unutma.",
        "3 farklı mod var.\n\nKlasik, Sakın Unutma, Hata Yok",
        "-> Klasik \n"+
            "Bu modda yıldız kazanmak için bir çok şeye dikkat etmen gerekecek. Her yaptığın hata ve geçen zaman senin yıldız sayını olumsuz etkileyecek.",
        "-> Sakın Unutma \n"+
            "Bu modda kartları daha dikkatli izleyip hatırlaman gerekecek. Süre problemi olmadan yaptığın hata sayısına göre yıldız kazanacaksın!",
        "-> Hata Yok \n"+
            "Bu mod fazla acımasızca tasarlandı. Hafızanızı zorlamanızı istiyoruz. Hata odaklı sistemde her ne kadar Sakın Unutma'yı hatırlatsa da "+
                "ilk hatanızda oyun bitecektir!",
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
        "Earn\n4 secs",
        "4 sn\nkazan"
    };
    public static string[] rewardScreen8Sec =
    {
       "Earn\n8 secs",
        "8 sn\nkazan"
    };
    public static string[] rewardScreenNoButton =
   {
       "No, thanks",
        "Hayır,\nTeşekkürler"
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
