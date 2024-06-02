using Android.App;
using Android.Views;

namespace PokeTuberToolkit.UI.Uno.Platforms.Android;
[Activity(
    MainLauncher = true,
    ConfigurationChanges = ActivityHelper.AllConfigChanges,
    WindowSoftInputMode = SoftInput.AdjustNothing | SoftInput.StateHidden
)]
public class MainActivity : ApplicationActivity
{
}
