using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using FormotsBLL.BLL;
using FormotsBLL.Mapper;
using FormotsCommon.DTO;
using FormotsGUI.ViewModels;
using FormotsGUI.ViewModels.Dossiers;
using FormotsGUI.ViewModels.Formulaires;
using FormotsGUI.ViewModels.MedecinAppelants;
using FormotsGUI.ViewModels.Statistiques;
using FormotsGUI.ViewModels.Users;
using FormotsGUI.Windows;

namespace FormotsGUI
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App
    {
        private const int SplashScreenMinimumAppearingTimeInSeconds = 2;
        private const bool IsLoginNeeded = true;
        private UserDto userConnected;

        protected override void OnStartup(StartupEventArgs e)
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            base.OnStartup(e);

            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentCulture.NumberFormat = NumberFormatInfo.InvariantInfo;

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            CheckLicencing();

            //Initialize Database
            CreateIfNotExists("FormotsDatabase.sdf");

            //Initialisation des mappings profiles
            var loadMappers = new MainMapperProfile();
            loadMappers.ConfigureObjectMapper();

            //Pour la fenêtre de login, ne pas fermer le programme lorsque l'on quitte la fenêtre.
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var loginWindow = new LoginWindow
            {
                BorderThickness = new Thickness(1),
                GlowBrush = null
            };
            loginWindow.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (IsLoginNeeded)
            {
                OpenStartupSplashscreen();
                loginWindow.ShowDialog();
                //var loginViewModel = (LoginWindowViewModel) loginWindow.DataContext;
                //if (loginViewModel != null)
                //{
                //    userConnected = loginViewModel.LoginUser;
                //    MainWindowViewModel.UserConnected = userConnected;
                //}

                if (!loginWindow.DialogResult.GetValueOrDefault())
                {
                    Environment.Exit(0);
                }
            }

            OpenMainWindow();
        }

        public static string LicenceExpirationDate;

        #region Decryption Licence Key

        private static void CheckLicencing()
        {
            //DETECT LICENCING
            //Recherche de la licence KEY : mots_licence.txt
            //FORMAT : 'IDENTIFIANT EXPIRATION DATE'
            //Exemple : 'MPROST 31/12/2017'
            var keyToEncrypt = "AFAURE 31122017"; // original plaintext
            var passphrase = "Formots2017!";
            var sha1Encryption = "fcIa6LZGtHHwWdqVYp4xY3U/K/zTYFIFncSwCNRrsI0="; // original plaintext

            //Read file in MyDocuments
            var myDocumentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var finalFile = myDocumentsPath + "\\" + "mots_licence.txt";
            if (!File.Exists(finalFile))
            {
                MessageBox.Show(
                    $"Impossible de trouver votre fichier de licence {finalFile}. \r\n Merci de contacter votre service client.",
                    "Licence invalide", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            var readText = File.ReadAllText(finalFile);
            if (string.IsNullOrEmpty(readText))
            {
                MessageBox.Show($"Votre fichier de licence {finalFile} est vide. \r\n Merci de contacter votre service client.",
                    "Licence invalide", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }

            try
            {
                var decryptedKey = EncryptionUtility.DecryptString(readText, passphrase);
                if (string.IsNullOrEmpty(decryptedKey))
                {
                    ShowInvalidLicence(finalFile);
                }

                var splittedKey = decryptedKey.Split(' ');
                //var id = splittedKey.FirstOrDefault();
                var endDateString = splittedKey.ElementAt(1);
                var expirationDate = DateTime.Parse(endDateString);

                var currentDate = DateTime.Today;
                //currentDate = GetFromInternetCurrentDate(currentDate);
                if (currentDate > expirationDate)
                {
                    MessageBox.Show("Votre licence est arrivée à expiration ! Merci de contacter votre service client.",
                        "Licence invalide", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(0);
                }
                LicenceExpirationDate = expirationDate.Date.ToString();
            }
            catch (Exception exception)
            {
                ShowInvalidLicence(finalFile);
            }
        }

        private static DateTime GetFromInternetCurrentDate(DateTime currentDate)
        {
            try
            {
                var client = new TcpClient("time.nist.gov", 13);
                using (var streamReader = new StreamReader(client.GetStream()))
                {
                    var response = streamReader.ReadToEnd();
                    var utcDateTimeString = response.Substring(7, 17);
                    currentDate = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Aucune connexion internet détectée. Merci de rétablir la connexion ou de contacter votre service client.",
                    "Connexion internet", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            return currentDate;
        }

        private static void ShowInvalidLicence(string finalFile)
        {
            MessageBox.Show($"Votre fichier de licence {finalFile} est invalide. \r\n Merci de contacter votre service client.",
                "Licence invalide", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(0);
        }

        #endregion

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage =
                $"An unhandled exception occurred: {e.Exception.Message} - {Environment.NewLine} - Stacktrace: {e.Exception.StackTrace} - {Environment.NewLine} - Source: {e.Exception.Source}";
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }


        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }

        private static void OpenStartupSplashscreen()
        {
            var splashScreen = new SplashScreen("resources/startupSplashScreenMOTS.png");
            splashScreen.Show(true);
            Thread.Sleep(SplashScreenMinimumAppearingTimeInSeconds * 1000);
            splashScreen.Close(TimeSpan.FromSeconds(SplashScreenMinimumAppearingTimeInSeconds));
        }

        private static UsersListFormViewModel usersListFormViewModel;
        private static MedecinAppelantsListFormViewModel medecinAppelantsListFormViewModel;
        private static DossiersListFormViewModel dossiersListFormViewModel;
        private static DashboardPageViewModel dashboardPageViewModel;
        private static FormulaireBaseViewModel formulaireBaseViewModel;
        private static ChartsListFormViewModel chartsListFormViewModel;

        private static void OpenMainWindow()
        {
            var userConnected = MainWindowViewModel.UserConnected;
            DashboardPageViewModel.Instance.WelcomeUserTitle = $"Bienvenue Dr. {userConnected?.FirstName} {userConnected?.LastName}";

            Current.ShutdownMode =
                ShutdownMode.OnMainWindowClose; //Permet de fermer le programme lorsque l'on quitte la fenêtre

            InitializeApplicationViewModelsAndLaunch();
        }

        private static void InitializeApplicationViewModelsAndLaunch()
        {
            var main = new MainWindow();
            Current.MainWindow = main;

            Action<string, string> exec = (s, s1) =>
            {
                formulaireBaseViewModel = FormulaireBaseViewModel.Instance;
                usersListFormViewModel = UsersListFormViewModel.Instance;
                medecinAppelantsListFormViewModel = MedecinAppelantsListFormViewModel.Instance;
                dossiersListFormViewModel = DossiersListFormViewModel.Instance;
                dashboardPageViewModel = DashboardPageViewModel.Instance;
                //Attention: Ne pas appeler le ChartsListFormViewModel qui update la vue directement.

                dossiersListFormViewModel.SynchroneUpdateList();
                usersListFormViewModel.SynchroneUpdateList();
                medecinAppelantsListFormViewModel.SynchroneUpdateList();
            };

            var progressWindow = new ProgressWindow();
            progressWindow.SetTitle("Chargement en cours");
            var backgroundWorker = new BackgroundWorker();

            // set the worker to call your long-running method
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                exec.Invoke("path", "parameters");
            };

            // set the worker to close your progress form when it's completed
            backgroundWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                progressWindow.Close();
                main.Show();
                MainWindowViewModel.Instance.OpenDashboardPageCommand.Execute(null);
            };
            progressWindow.Show();
            backgroundWorker.RunWorkerAsync();
        }

        //private static readonly string _appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private static void CreateIfNotExists(string fileName)
        {
            var localAppData =
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData);
            var motsFilePath
                = Path.Combine(localAppData, "MOTS");
            var exePath = System.IO.Path.GetDirectoryName(
                new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            if (!Directory.Exists(motsFilePath))
            {
                Directory.CreateDirectory(motsFilePath);
            }
            //var currentPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //var startupPath = System.Windows.Forms.Application.StartupPath;
            ////string dataDirectory = null;
            ////if (ApplicationDeployment.IsNetworkDeployed)
            ////{
            ////    dataDirectory = ApplicationDeployment.CurrentDeployment.DataDirectory;
            ////}
            ////IsolatedStorageFile appScope = IsolatedStorageFile.GetUserStoreForApplication();
            //var _appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!File.Exists($"{motsFilePath}\\{fileName}"))
            {
                //MessageBox.Show($"Aucune base de donnée existante n'a été trouvée. Mise en place de la base de données en cours. " +
                //                $"{Environment.NewLine}  Source current path : {_appDataFolderPath}\\{fileName} " +
                //                $"{Environment.NewLine}  Exe path : {exePath} " +
                //                $"{Environment.NewLine}  currentPath : {currentPath} " +
                //                $"{Environment.NewLine}  startupPath : {startupPath} " +
                //                $"{Environment.NewLine} Cible : {motsFilePath}\\{fileName}",
                //    "MOTS", MessageBoxButton.OK, MessageBoxImage.Information);

                MessageBox.Show($"Aucune base de donnée existante n'a été trouvée. Mise en place de la base de données en cours. " +
                                $"{Environment.NewLine}  Exe path : {exePath} " +
                                $"{Environment.NewLine} Target path : {motsFilePath}",
                    "MOTS", MessageBoxButton.OK, MessageBoxImage.Information);

                File.Copy($"{exePath}\\{fileName}", $"{motsFilePath}\\{fileName}");

                //TEST
                //if it's not already there, 
                //copy the file from the deployment location to the folder
                //string sourceFilePath = Path.Combine(
                //    System.Windows.Forms.Application.StartupPath, "MyFile.txt");
                //string destFilePath = Path.Combine(userFilePath, "MyFile.txt");
                //if (!File.Exists(destFilePath))
                //    File.Copy(sourceFilePath, destFilePath);

            }

            AppDomain.CurrentDomain.SetData("DataDirectory", motsFilePath);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // This is our connection string: Data Source=|DataDirectory|\Chinook40.sdf
            // Set the data directory to the users %AppData% folder
            // So the Chinook40.sdf file must be placed in:  C:\\Users\\<Username>\\AppData\\Roaming\\
            //AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }
    }
}
