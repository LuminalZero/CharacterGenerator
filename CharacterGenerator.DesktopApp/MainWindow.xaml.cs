using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CharacterGenerator.DesktopApp.Enums;
using CharacterGenerator.DesktopApp.Models;
using CharacterGenerator.DesktopApp.Services;

namespace CharacterGenerator.DesktopApp
{
    public partial class MainWindow : Window
    {
        private readonly ICharacterGeneratorService _characterGeneratorService;

        public MainWindow(ICharacterGeneratorService characterGeneratorService)
        {
            InitializeComponent();
            this._characterGeneratorService = characterGeneratorService;
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisableForm();
                ShowGeneratingStarted();
                ShowProgressStarted();

                await GenerateCharacter();

                ShowGeneratingFinished();
            }
            catch (Exception ex)
            {
                ShowGeneratingError(ex);
            }
            finally
            {
                ShowProgressFinished();
                EnableForm();
            }
        }

        private void EnableForm()
        {
            FemaleRadioButton.IsEnabled = true;
            GenerateButton.IsEnabled = true;
            MaleRadioButton.IsEnabled = true;
        }

        private void DisableForm()
        {
            FemaleRadioButton.IsEnabled = false;
            GenerateButton.IsEnabled = false;
            MaleRadioButton.IsEnabled = false;
        }

        private async Task GenerateCharacter()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                
                var gender = Dispatcher.Invoke(() => GetGender());

                var character = _characterGeneratorService.GenerateCharacter(gender);

                Dispatcher.Invoke(() => ShowCharacter(character));
            });
        }

        private void ShowCharacter(Character character)
        {
            ShowName(character.Name);
            ShowStats(character);
        }

        private Gender GetGender()
        {
            return (MaleRadioButton.IsChecked ?? false) ? Gender.Male : Gender.Female;
        }

        private void ShowName(string name)
        {
            NameTextBox.Text = name;
        }

        private void ShowStats(Character character)
        {
            Charisma.Content = character.Charisma;
            Constitution.Content = character.Constitution;
            Dexterity.Content = character.Dexterity;
            Intelligence.Content = character.Intelligence;
            Strength.Content = character.Strength;
            Wisdom.Content = character.Wisdom;
        }

        private void ShowGeneratingStarted()
        {
            Status.Text = "Generating...";
        }

        private void ShowGeneratingFinished()
        {
            Status.Text = "Finished";
        }

        private void ShowGeneratingError(Exception ex)
        {
            Status.Text = ex.Message;
        }

        private void ShowProgressStarted()
        {
            Progress.Visibility = Visibility.Visible;
            Progress.IsIndeterminate = true;
        }

        private void ShowProgressFinished()
        {
            Progress.Visibility = Visibility.Hidden;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
