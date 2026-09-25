using P06Shop.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace P12MAUI.Client.MessageBox
{
    internal class MauiMessageDialogService : IMeesageDialogService
    {
        public void ShowMessage(string message)
        {
             MainThread.BeginInvokeOnMainThread(async () =>
                await Shell.Current.DisplayAlertAsync("Message", message, "OK"));
        }
    }
}
