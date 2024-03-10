using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using SQLite;
using System.Collections.ObjectModel;

namespace ExpendiMate.ViewModels
{
    internal partial class InstallmentViewModel : ObservableObject
    {
        public static InstallmentViewModel Current { get; set; }

        SQLiteConnection connection;
        public List<InstallmentModel> Installments
        {
            get
            {
                return connection.Table<InstallmentModel>().ToList();
            }
        }

        public double TotalCost
        {
            get
            {
                var list = connection.Table<InstallmentModel>().ToList();

                double result = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].InstallmentIsActivated)
                    {
                        result = result + list[i].InstallmentCost;
                    }
                }
                return result;
            }
        }

        public InstallmentViewModel()
        {
            Current = this;
            connection = DatabaseServices.Connection;
        }

        public void SaveAndUpdate(InstallmentModel model)
        {
            if (model.Id > 0)
            {
                connection.Update(model);
            }
            else
            {
                connection.Insert(model);
            }
            OnPropertyChanged("TotalCost");
        }

        public void Delete(InstallmentModel model)
        {
            // If it has an Id, then we can delete it 
            if (model.Id > 0)
            {
                connection.Delete(model);
            }
        }

        public void UpdateView()
        {
            OnPropertyChanged(nameof(Installments));
        }

    }
}
