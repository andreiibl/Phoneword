namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        string numeroTraducido;

        async void OnTraducir(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(1.2, 100); // Escala el botón a 1.2x en 100ms
            await button.ScaleTo(1, 100);

            string numeroIntroducido = PhoneNumberText.Text;
            numeroTraducido = Core.PhonewordTranslator.ToNumber(numeroIntroducido);

            if (!string.IsNullOrEmpty(numeroTraducido))
            {
                BotonLlamar.IsEnabled = true;
                BotonLlamar.BackgroundColor = Color.FromArgb("#3498DB");
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
            var button = (Button)sender;
            await button.ScaleTo(1.2, 100); // Escala el botón a 1.2x en 100ms
            await button.ScaleTo(1, 100);
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
