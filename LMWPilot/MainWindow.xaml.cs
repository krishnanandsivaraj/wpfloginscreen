using System.Windows;
using System.Windows.Controls;
using SmartLoginOverlayDemo.Models;
using SmartLoginOverlayDemo.ViewModels;
using SoftArcs.WPFSmartLibrary.SmartUserControls;

namespace SmartLoginOverlayDemo.Views
{
    public partial class MainWindow
    {
        #region Fields

        public LoginViewModel ViewModel;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel = new LoginViewModel();
            this.DataContext = this.ViewModel;
        }

        #endregion

        #region Event handler

        //+--------------------------------------------------------------------
        //+ Just for demo purposes - only to demonstrate code behind handling -
        //+--------------------------------------------------------------------
        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            this.SmartLoginOverlayControl.Lock();
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.ChangeRecentUser(new User()
            {
                UserName = "BlueHairBeauty",
                Password = "blue1",
                EMailAddress = @"blue@reallypretty.com",
                ImageSourcePath = @"\Images\DemoUser1.png"
            });

            this.SmartLoginOverlayControl.DisappearAnimation = DisappearAnimationType.MoveAndFadeOutToTopSimultaneous;
            this.SmartLoginOverlayControl.Lock();

            (sender as Button).IsEnabled = false;
        }

        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedParameter.Local
        private void SmartLoginOverlay_SubmitRequested(object sender, RoutedEventArgs e)
        // ReSharper restore UnusedParameter.Local
        // ReSharper restore UnusedMember.Local
        {
            if (this.ViewModel.Password.Equals(this.ViewModel.UserPassword))
            {
                this.SmartLoginOverlayControl.Unlock();
            }
            else
            {
                this.SmartLoginOverlayControl.ShowWrongCredentialsMessage();
            }
        }

        #endregion
    }
}
