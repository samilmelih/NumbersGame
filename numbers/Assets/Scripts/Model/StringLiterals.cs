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
		"Baslat"
	};
		
	public static string[] settingsButton =
	{
		"Settings",
		"Ayarlar"
	};

	public static string[] exitButton =
	{
		"Exit",
		"Cikis"
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
        "Hayir"
    };
    public static string[] exitQuestionText =
   {
        "Are you sure?",
        "Cikmak istiyor musunuz?"
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
		"Sakin Unutma"
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
		"Music",
		"Muzik"
	};

	public static string[] creditsButton =
	{
		"Credits",
		"Hakkinda"
	};

	public static string[] resetProgressButton =
	{
		"Reset Progress",
		"Sifirla"
	};

	#endregion

	#region Level

	public static string[] levelText =
	{
		"LEVEL",
		"LEVEL"
	};

	public static string[] howToPlayButton =
	{
		"How To Play",
		"Nasil Oynanir"
	};

	public static string[] quitButton =
	{
		"Quit",
		"Cikis"
	};

	public static string[] levelCompletedText =
	{
		"Level Completed",
		"Level Tamamlandi"
	};

	public static string[] backButton =
	{
		"Back",
		"Geri"
	};

	public static string[] nextButton =
	{
		"Next",
		"Ileri"
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
        "After that you can have another chance by touching that button.But you have to watch an Advertisement video."+
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
                "Follow the Numbers oyunumuza hosgeldiniz! Bu tanitim size oyunun nasil oynandigi konusunda yardimci olacak.",
        "Bu basit bir oyun.Tek yapmaniz gereken sayilari takip etmeniz.",
        "Peki! Sakaydi ama bu konuda ciddiyim."+
        "Her kartta bir sayi var.",
        "Ve sen onlari dogru sirada açmalisin.",
        "Eger actigin kartlari unutmazsan 3 yildiz alabilirsin.",
        "Eger kartlari gormek istersen her levelde bir sansin var!Yukarida gordugun buyuk sayiya dokun! Evet o buyuk olan...",
        "Yine ayni yere dokunarak hak kazanabilirsin bu durumda reklam vidyosu izlemelisin"+
            "Eger vidyoyu gecersen daha az sure kazanirsin.Tamamini izledigin takdirde daha uzun sure kazanirsin.",
        "Bu islevi her modda kullanabilirsin.",
        "3 farkli mod var. Klasik, Sakin unutma,Hata yok",
        "\t Klasij \n"+
            "Bu modda yildiz kazanmak icin bircok seye dikkat etmen gerekecek. Her yaptigin hata ve gecen zaman senin yildiz sayini olumsuz etkileyecek.",
       "\t Sakin Unutma \n"+
            "Bu modda kartlari daha dikkatli izleyip hatirlaman gerekecek. Sure problemi olmadan yaptigin hata sayisina gore yildiz kazanacaksin!",
       "\t Hata Yok \n"+
            "Bu mod fazla acimasizca tasarlandi. Hafizanizi zorlamanizi istiyoruz. Hata odakli sistemde her ne kadar Sakin unutmayi hatirlatsada"+
                "ilk hatanizda oyun bitecektir..",
        "Iyi eglenceler!Eger begendiyseniz Google Play de oylamayi ve yorum yapmayi unutmayin!"
        }

    };

    #endregion

}
