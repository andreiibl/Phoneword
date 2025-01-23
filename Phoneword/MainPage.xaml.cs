namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        string numeroTraducido;

        private void OnTraducir(object sender, EventArgs e)
        {
            string numeroIntroducido = PhoneNumberText.Text;
            numeroTraducido = Core.PhonewordTranslator.ToNumber(numeroIntroducido);

            if (!string.IsNullOrEmpty(numeroTraducido))
            {
                BotonLlamar.IsEnabled = true;
                BotonLlamar.Text = "Llamar " + numeroTraducido;
            }
            else
            {
                BotonLlamar.IsEnabled = false;
                BotonLlamar.Text = "Llamar";
            }
        }

        async void OnLlamar(object sender, System.EventArgs e)
        {
            if (await this.DisplayAlert(
                "Llamar al numero",
                "¿Te gustaría llamar al numero " + numeroTraducido+ "?",
                "Sí",
                "No")) 
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                        PhoneDialer.Default.Open(numeroTraducido);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("No se a podido llamar", "El numero de teléfono no es válido.", "Ok");
                }
                catch (Exception)
                {
                    await DisplayAlert("No se a podido llamar", "Ha ocurrido un error.", "Ok");
                }
            }
        }
    }
}
