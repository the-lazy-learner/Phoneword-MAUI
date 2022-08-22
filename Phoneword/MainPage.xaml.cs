namespace Phoneword;

public partial class MainPage : ContentPage
{
	string translatedNumber;

	public MainPage()
	{
		InitializeComponent();
	}

	public void OnTranslate(object sender, EventArgs e)
	{
		string enteredNumber = PhoneNumberText.Text;
		translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

		if (!string.IsNullOrEmpty(translatedNumber))
		{
			CallButton.IsEnabled = true;
			CallButton.Text = $"Call {translatedNumber}";
		}
		else
		{
			CallButton.IsEnabled = false;
			CallButton.Text = "Call";
		}
	}

	async void OnCall(object sender, EventArgs e)
	{
		if (await this.DisplayAlert(
			"Dial a Number",
			$"Would you like to call {translatedNumber}?",
			"Yes",
			"No"))
		{
			try
			{
				if (PhoneDialer.Default.IsSupported)
					PhoneDialer.Default.Open(translatedNumber);
			}
			catch (ArgumentNullException)
			{
				await DisplayAlert("Unable to Dial", "Phone number was not valid", "OK");
			}
			catch (Exception)
			{
				await DisplayAlert("Unable to Dial", "Phone dialing failed.", "OK");
			}
		}
	}
}

