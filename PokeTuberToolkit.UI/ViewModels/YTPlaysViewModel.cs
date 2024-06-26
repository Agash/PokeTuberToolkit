﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PokeTuberToolkit.UI.Contracts.ViewModels;
using PokeTuberToolkit.UI.Services;
using PokeTuberToolkit.UI.Views.YTPlays;

namespace PokeTuberToolkit.UI.ViewModels;

public partial class YTPlaysViewModel : ObservableRecipient
{
    public static WindowEx? BrowserWindow { get; set; }

    public YTPlaysViewModel()
    {
    }


    [RelayCommand]
    private void OpenBrowserWindow()
    {
        BrowserWindow ??= new BrowserWindow();
        BrowserWindow.Closed += BrowserWindow_Closed;    

        BrowserWindow.Activate();
    }

    private void BrowserWindow_Closed(object sender, Microsoft.UI.Xaml.WindowEventArgs args)
    {
        BrowserWindow = null;
    }
}
