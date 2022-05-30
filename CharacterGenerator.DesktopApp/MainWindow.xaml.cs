using System;
using System.Threading.Tasks;
using System.Windows;

namespace CharacterGenerator.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisableGenerateButton();
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
                EnableGenerateButton();
            }
        }

        private void EnableGenerateButton()
        {
            GenerateButton.IsEnabled = true;
        }

        private void DisableGenerateButton()
        {
            GenerateButton.IsEnabled = false;
        }

        private async Task GenerateCharacter()
        {
            await Task.Run(() =>
            {
                var character = GetCharacter();

                Dispatcher.Invoke((Delegate)(() =>
                {
                    ShowCharacter(character);
                }));
            });
        }

        private Character GetCharacter()
        {
            var gender = GetGender();

            return new Character
            {
                Gender = gender,
                Name = GetName(gender == Gender.Male),
                Stats = GetStats(),
            };
        }

        private void ShowCharacter(Character character)
        {
            ShowName(character.Name);
            ShowStats(character.Stats);
        }

        private Gender GetGender()
        {
            bool isMale = true;

            Dispatcher.Invoke(() =>
            {
                isMale = MaleRadioButton.IsChecked ?? true;
            });

            return isMale ? Gender.Male : Gender.Female;
        }

        private string GetName(bool isMale)
        {
            return GetRandom.FirstName(isMale);
        }

        private Stats GetStats()
        {
            var stats = new Stats
            {
                Charisma = GetRandom.Int32(1, 18),
                Constitution = GetRandom.Int32(1, 18),
                Dexterity = GetRandom.Int32(1, 18),
                Intelligence = GetRandom.Int32(1, 18),
                Strength = GetRandom.Int32(1, 18),
                Wisdom = GetRandom.Int32(1, 18),
            };

            return stats;
        }

        private void ShowName(string name)
        {
            NameTextBox.Text = name;
        }

        private void ShowStats(Stats stats)
        {
            Charisma.Content = stats.Charisma;
            Constitution.Content = stats.Constitution;
            Dexterity.Content = stats.Dexterity;
            Intelligence.Content = stats.Intelligence;
            Strength.Content = stats.Strength;
            Wisdom.Content = stats.Wisdom;
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
