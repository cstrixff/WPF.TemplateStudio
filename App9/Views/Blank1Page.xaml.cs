using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace App9.Views;

public partial class Blank1Page : Page, INotifyPropertyChanged
{
    public Blank1Page()
    {
        InitializeComponent();
        DataContext = this;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OpenFlyout(object sender, System.Windows.RoutedEventArgs e)
    {
        firstFlyout.IsOpen = true;
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        datagridDisci.ItemsSource = Dados.Disciplinas();
    }

    private void datagridDisci_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        firstFlyout.Header = "Editar " + ((Disciplina)datagridDisci.SelectedItem).Nome;
        txtboxNomeSegunda.Text = ((Disciplina)datagridDisci.SelectedItem).Nome;
        txtboxNome.Text = ((Disciplina)datagridDisci.SelectedItem).Professor;
        btnCriar.Visibility = System.Windows.Visibility.Hidden;
        btnGuardar.Visibility = System.Windows.Visibility.Visible;
        firstFlyout.IsOpen = true;
    }

    private void btnNovo_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        firstFlyout.Header = "Criar Disciplina";
        txtboxNomeSegunda.Text = "";
        txtboxNome.Text = "";
        btnCriar.Visibility = System.Windows.Visibility.Visible;
        btnGuardar.Visibility = System.Windows.Visibility.Hidden;
        firstFlyout.IsOpen = true;
    }
}
