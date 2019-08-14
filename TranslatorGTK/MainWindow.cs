using System;
using System.Net;
using System.IO;
using TranslatorGTK;
using System.Threading.Tasks;
using Gtk;
using Newtonsoft.Json;

public partial class MainWindow : Gtk.Window
{

    private static string _apiKey = "trnsl.1.1.20190806T091619Z.472bf412f426c085.62f66808852ed3358a563140f8c35be632682b46\n";

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    private async void TranslateTextAsync() {

        await Task.Run(() => TranslateText());

    }

    private void TranslateText() {
        outputField.Buffer.Text = "Translating..";
        if (inputField.Buffer.Text == String.Empty) {
            outputField.Buffer.Text = "You can't translate empty field";
            return;
        }
        try {
            //Покупаем
            WebRequest wr = WebRequest.Create($"https://translate.yandex.net/api/v1.5/tr.json/translate?key={_apiKey}&text={inputField.Buffer.Text}&lang={languageText.Buffer.Text}");
            //Фиксируем прибыль
            WebResponse response = wr.GetResponse();
            using (Stream stream = response.GetResponseStream()) {
                using (StreamReader reader = new StreamReader(stream)) {
                    YandexResponseJson yrj = JsonConvert.DeserializeObject<YandexResponseJson>(reader.ReadToEnd());
                    outputField.Buffer.Text = yrj.Text[0];
                }
            }

        }catch(WebException ex) {
            outputField.Buffer.Text = "Error translating";
        }
        if (checkbutton2.Active) {
            inputField.Buffer.Text = String.Empty;
            this.Focus = inputField;
        }
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnButton1Clicked(object sender, EventArgs e) {
        this.Focus = inputField;
        TranslateTextAsync();
    }

    protected void OnCheckbutton1Toggled(object sender, EventArgs e) {
        this.Modal = checkbutton1.Active;
        this.KeepAbove = checkbutton1.Active;
    }
}
