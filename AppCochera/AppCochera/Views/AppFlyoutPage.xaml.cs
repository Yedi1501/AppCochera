using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCochera.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppFlyoutPage : FlyoutPage
    {
        public ObservableCollection<FlyoutMenuItem> MenuItems { get; set; }

        public AppFlyoutPage()
        {
            InitializeComponent();

            // Define los ítems del menú
            MenuItems = new ObservableCollection<FlyoutMenuItem>
            {
                new FlyoutMenuItem { Title = "Página Principal", Icon = "home_icon.png", TargetPage = typeof(PaginaPrincipalPage) },
                new FlyoutMenuItem { Title = "Parqueo", Icon = "parking_icon.png", TargetPage = typeof(ParqueoPage) }
            };

            // Asigna la lista de ítems al CollectionView
            MenuItemsListView.ItemsSource = MenuItems;

            // Maneja la selección de ítems del menú
            MenuItemsListView.SelectionChanged += OnMenuItemSelected;
        }

        private async void OnMenuItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            // Obtiene el ítem seleccionado
            var selectedItem = e.CurrentSelection[0] as FlyoutMenuItem;

            if (selectedItem != null)
            {
                // Crea una instancia de la página seleccionada
                var page = (Page)Activator.CreateInstance(selectedItem.TargetPage);
                Detail = new NavigationPage(page);
                IsPresented = false; // Oculta el menú después de la selección
            }
        }

        public class FlyoutMenuItem
        {
            public string Title { get; set; } // Texto del menú
            public string Icon { get; set; } // Ruta del ícono
            public Type TargetPage { get; set; } // Página a la que navega
        }
    }
}

