using Evertec.Tips.Domain.Dto;
using Evertec.Tips.Domain.Entities;
using Evertec.Tips.Infrastructure.Interfaces;
using Evertec.Tips.Infrastructure.Repositories;
using Evertec.Tips.Mobile.Common.Hubs;
using Evertec.Tips.Mobile.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Evertec.Tips.Mobile.Services
{
    public class RealTimeTipService : IRealTimeTipService
    {
        private readonly ITipRepository _tipRepository;

        public bool IsConnected => HubConnectionTip.IsConnected;

        public RealTimeTipService(ITipRepository tipRepository)
        {
            _tipRepository = tipRepository;
            var userName = Preferences.Get("username", string.Empty);
            //HubConnectionTip.Initialize($"https://localhost:7252/tipHub?username={userName}");
        }

        public Task Connect()
        {
            HubConnectionTip.HubConnection.On<MessageItemDto>("NewTip", AddTip);
            HubConnectionTip.HubConnection.On<int>("DeleteTip", RemoveTip);
            HubConnectionTip.HubConnection.On<MessageItemDto>("UpdateTip", UpdateTip);
            return Task.CompletedTask;
        }

        private async Task UpdateTip(MessageItemDto arg)
        {
            await _tipRepository.UpdateTip(arg.Message);
        }

        private async Task RemoveTip(int arg)
        {
            await _tipRepository.DeleteTip(arg);
        }

        private async Task AddTip(MessageItemDto arg)
        {
            await _tipRepository.AddTip(arg.Message);
        }

        public async Task DisConnect()
        {
            await HubConnectionTip.DisConnect();
        }

        public async Task Register()
        {
            await HubConnectionTip.Register();
        }

        public async Task UnRegister(string userName)
        {
            await HubConnectionTip.UnRegister(userName);
        }
    }
}
