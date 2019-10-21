using System;
using System.Windows;
using JetBrains.Annotations;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;

namespace WpfApplication1.ViewModels
{
    [UsedImplicitly]
    public class ShellViewModel : BindableBase
    {
        private HubConnection connection;
        private string title;

        public ShellViewModel()
        {
            Title = "Signal R Demo Wpf Client Application";

            Application.Current.MainWindow.Closed += async (sender, args) =>
            {
                if (connection == null)
                {
                    return;
                }

                if (connection.State == HubConnectionState.Disconnected)
                {
                    return;
                }

                await connection.StopAsync();
            };
        }

        [UsedImplicitly]
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public DelegateCommand<string> ConnectCommand => new DelegateCommand<string>(OnConnectCommand);

        public DelegateCommand<string> SendMessageCommand => new DelegateCommand<string>(OnSendMessage);

        private async void OnSendMessage(string message)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", "rkn tyln", "gtfooh");
                //ChatHub chatHub = await connection.InvokeAsync<ChatHub>("SendMessage", "rkn tyln", "gtfooh");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void OnConnectCommand(string url)
        {
            try
            {
                connection = new HubConnectionBuilder().WithUrl(url)
                                                       .Build();

                connection.On<string, string>("ReceiveMessage", ReceiveMessage);

                await connection.StartAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReceiveMessage(string user, string message)
        {
            MessageBox.Show($"{user}:{message}");
        }
    }
}
