using Evertec.Tips.Domain.Models;
using System.Collections.ObjectModel;

namespace Evertec.Tips.Mobile.Interfaces
{
    public interface ITipService
    {
        Task<bool> DeleteTip(int id);

        Task<bool> UpdateTip(TipModel tip);

        Task<bool> AddTip(TipModel tip);

        Task<ObservableCollection<TipModel>> GetAll();
    }
}
