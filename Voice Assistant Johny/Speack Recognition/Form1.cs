using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.Speech.AudioFormat;
using System.Speech.Recognition.SrgsGrammar;
using System.Speech.Synthesis.TtsEngine;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Xml;
using System.Timers;

namespace Speack_Recognition
{
    public partial class Form1 : Form
    {
        //kopie pishi
        DateTime now = DateTime.Now;
        SpeechSynthesizer s = new SpeechSynthesizer();
        public Form1()
        {
            InitializeComponent();
            SpeechRecognitionEngine reco = new SpeechRecognitionEngine();
            Choices list = new Choices();
            string time = now.GetDateTimeFormats('t')[0];
            if (now.Hour >= 0 && now.Hour < 12)
            { s.SpeakAsync("good morning,sir"); }
            if (now.Hour >= 12 && now.Hour < 18)
            { s.SpeakAsync("good afternoon,sir"); }
            if (now.Hour >= 18 && now.Hour < 24)
            { s.SpeakAsync("good evening,sir"); }

            PowerStatus powerStatus = SystemInformation.PowerStatus;
            if (powerStatus.PowerLineStatus == PowerLineStatus.Online)
            {
                s.SpeakAsync("battery is charging");
            }

            else
            {
                s.SpeakAsync("battery is not charging");
            }

            list.Add(new string[] {"orders","move left","move down","move right","move up","page up","page down","switch window","switch","enter","ok","close tab","cancel restart","cancel shutdown",
                "lock desktop","sign out my computer","shutdown computer","restart computer","johny time","johny date",
                "johny close","johny normalsize","johny normalscreen","johny minimize","johny maximize","johny fullscreen",
                "open facebook","open notepad","bye johny","fuck you","your birth","hi johny","news",
                "hello johny","who are","how are","open youtube","open word","play movie","play music","open google",
                "open powerpoint","open my computer" });

            Grammar gm = new Grammar(new GrammarBuilder(list));

            try
            {
                reco.RequestRecognizerUpdate();
                reco.LoadGrammar(gm);
                reco.SpeechRecognized += Reco_SpeechRecognized;
                reco.SetInputToDefaultAudioDevice();
                reco.RecognizeAsync(RecognizeMode.Multiple);



            }
            catch { }
        }
        public string randomintro()
        {
            string[] intro = new string[4] { "my name is johny and im your personal assistant",
                "im johny and im here to help you", "im your new digital assistent my name is johny", "your personal assistent johny is here to help you" };
            Random r = new Random();
            return intro[r.Next(4)];
        }
        public string randomgreeting()
        {
            string[] greeting = new string[4] { "how can i help you sir", "welcome back sir", "how you been sir", "nice to meet you sir" };
            Random r = new Random();
            return greeting[r.Next(4)];
        }
        public string randomhi()
        {
            string[] hi = new string[3] { "greetings sir, how can i help you", "hello sir what can i do for you", "hello sir" };
            Random r = new Random();
            return hi[r.Next(3)];
        }
        private void Reco_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string a = e.Result.Text;

            switch (a)
            {

                case ("orders"):
                    s.Speak("these are the commands you can order me to do");
                    Process.Start(@"d:\commands.txt");
                    break;
                case ("hi johny"):
                    s.Speak("hello sir what can i do for you");
                    break;
                case ("who are"):
                    s.Speak(randomintro());
                    break;
                case ("news"):
                    s.SpeakAsync("latest news");
                    Process.Start("https://www.google.bg/search?q=" + a);
                    break;
                case ("open youtube"):
                    s.Speak("you can watch videos no youtube");
                    Process.Start("https://www.youtube.com/");
                    break;
                case ("hello johny"):
                    s.Speak(randomgreeting());
                    break;
                case ("how are"):
                    s.Speak("im fine ,i wait for your commands");
                    break;
                case ("your birth"):
                    s.Speak("i was given birth at thirteent december 2017 ");

                    break;
                case ("fuck you"):
                    s.SpeakAsync("fuck yourself,sir");
                    
                    break;
                case ("open facebook"):
                    Process.Start("https://www.facebook.com");
                    s.Speak("now you can see your friends activities");
                    break;
                case ("open google"):

                    s.Speak("I'm openning google");
                    Process.Start("https://www.google.bg");
                    break;
                case ("play movie"):
                    s.Speak("I start your player,choose movie");
                    Process.Start("GOM.exe");
                    Process.Start(@"d:\Filmi");
                    break;

                case ("play music"):
                    s.Speak("I start your player and open music folder");
                    Process.Start("Winamp.exe");
                    Process.Start(@"d:\Muzika");
                    break;
                case ("open word"):
                    s.Speak("I will open word,sir");
                    Process.Start("WinWord.exe");
                    break;

                case ("open notepad"):
                    s.Speak("okay sir");
                    Process.Start("notepad.exe");
                    break;
                case ("bye johny"):
                    s.Speak("goodbye sir,have a nice day");
                    this.Close();
                    break;
                case ("johny fullscreen"):
                    s.Speak("Im going fullscreen");
                    WindowState = FormWindowState.Maximized;
                    break;
                case ("johny maximize"):
                    s.Speak("Im going maximum size");
                    WindowState = FormWindowState.Maximized;
                    break;
                case ("johny minimize"):
                    s.Speak("Im minimizing");
                    WindowState = FormWindowState.Minimized;
                    break;
                case ("johny normalscreen"):
                    WindowState = FormWindowState.Normal;
                    s.Speak("Im my usual size");
                    break;
                case ("johny normalsize"):
                    WindowState = FormWindowState.Normal;
                    s.Speak("Im in usual size");
                    break;
                case ("johny close"):
                    SendKeys.Send("%{F4}");
                    s.Speak("its closed sir");
                    break;
                case ("johny date"):
                    s.Speak("today is" + DateTime.Now.ToLongDateString());
                    break;
                case ("johny time"):
                    s.Speak("time is" + DateTime.Now.ToShortTimeString());
                    break;
                case ("Open my computer"):
                    s.Speak("ofcourse sir");
                    Process.Start("explorer.exe", "::{20d04fe0-3aea-1069-a2d8-08002b30309d}");
                    break;
                case "shutdown computer":
                    s.Speak("sir, i will shutdown your computer in next second.");
                    System.Diagnostics.Process.Start("shutdown", "-s -t 60");
                    break;
                case "restart computer":
                    s.Speak("sir i will restart your computer in next one minute.");
                    System.Diagnostics.Process.Start("Restart", "-r -t 60");
                    break;
                case "sign out my computer":
                    System.Diagnostics.Process.Start("shutdown", "-l -f");
                    break;
                case "lock desktop":
                    s.Speak("yes sir. your computer is locked");
                    System.Diagnostics.Process.Start("rundll32.exe", "user32.dll, LockWorkStation");
                    break;
                case "cancel shutdown":
                    s.Speak("Canceled");
                    System.Diagnostics.Process.Start("shutdown", " -a");
                    break;
                case "cancel restart":
                    s.Speak("Canceled");
                    System.Diagnostics.Process.Start("Restart", " -a");
                    break;
                case "switch window":
                case "switch":
                    s.Speak("switch");
                    SendKeys.SendWait("%{TAB}");
                    //SendKeys.Send("%{TAB}");
                    break;
                case "enter":
                case "ok":
                    SendKeys.SendWait("{ENTER}");
                    break;
                case "close tab":
                    SendKeys.Send("^{w}");
                    break;
                case "​​​​​​move up":
                    SendKeys.Send("{UP}");//za poprafka
                    s.Speak("up");
                    break;
                case "move down":
                    SendKeys.Send("{DOWN}");
                    s.Speak("down");
                    break;
                case "move left":
                    SendKeys.Send("{LEFT}");
                    s.Speak("left");
                    break;
                case "move right":
                    SendKeys.Send("{RIGHT}");
                    s.Speak("right");
                    break;
                case "page down":
                    SendKeys.Send("{PGDN}");
                    break;
                case "page up":
                    SendKeys.Send("{PGUP}");
                    break;




            }


        }
    }
}
