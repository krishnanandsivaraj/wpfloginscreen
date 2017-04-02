using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using SmartLoginOverlayDemo.Models;
using SoftArcs.WPFSmartLibrary.MVVMCommands;
using SoftArcs.WPFSmartLibrary.MVVMCore;
using SoftArcs.WPFSmartLibrary.SmartUserControls;
using System;

namespace SmartLoginOverlayDemo.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		#region Fields

		private readonly string userImagesPath = @"\Images";

		#endregion // Fields

		#region Constructors

		public LoginViewModel()
		{
			if (ViewModelHelper.IsInDesignModeStatic == false)
			{
				this.initializeAllCommands();

				User recentUser = this.getRecentUser();

				this.UserName = recentUser.UserName;
				this.UserPassword = recentUser.Password;
				this.UserImageSource = recentUser.ImageSourcePath;
				this.EMailAddress = recentUser.EMailAddress;
			}
		}

		#endregion // Constructors

		#region Public Properties

		public string UserName
		{
			get { return GetValue( () => UserName ); }
			set { SetValue( () => UserName, value ); }
		}

		public string Password
		{
			get { return GetValue( () => Password ); }
			set { SetValue( () => Password, value ); }
		}

		public string UserPassword
		{
			get { return GetValue( () => UserPassword ); }
			set { SetValue( () => UserPassword, value ); }
		}

		public string UserImageSource
		{
			get { return GetValue( () => UserImageSource ); }
			set { SetValue( () => UserImageSource, value ); }
		}

		public string EMailAddress
		{
			get { return GetValue( () => EMailAddress ); }
			set { SetValue( () => EMailAddress, value ); }
		}

		#endregion // Public Properties

		#region Submit Command Handler

		public ICommand SubmitCommand { get; private set; }

		private void ExecuteSubmit(object commandParameter)
		{
			Debug.WriteLine( "Here you would implement the submission and a following validation of this data:\n" + this.UserName + "\n" + this.EMailAddress + "\n" + this.Password );

			var accessControlSystem = commandParameter as SmartLoginOverlay;

			if (accessControlSystem != null)
			{
				if (this.Password.Equals( this.UserPassword ))
				{
					accessControlSystem.Unlock();
				}
				else
				{
					accessControlSystem.ShowWrongCredentialsMessage();
				}
			}
		}

		private bool CanExecuteSubmit(object commandParameter)
		{
			return !string.IsNullOrEmpty( this.Password );
		}

		#endregion // Submit Command Handler

		#region Private Methods

		private void initializeAllCommands()
		{
			this.SubmitCommand = new ActionCommand( this.ExecuteSubmit, this.CanExecuteSubmit );
		}

		private User getRecentUser()
		{
			//+ Here you would implement code, which will get the recent user from a database,
			//+ a webservice or from somewhere else
			return new User()
						 {
							 UserName = Environment.UserName,
							 Password = "gingy1",
							 EMailAddress = @"gingy@farfaraway.com",
							 ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png" )
						 };
		}

		#endregion

		#region Public Methods

		public void ChangeRecentUser(User newRecentUser)
		{
			this.UserName = newRecentUser.UserName;
			this.UserPassword = newRecentUser.Password;
			this.EMailAddress = newRecentUser.EMailAddress;
			this.UserImageSource = newRecentUser.ImageSourcePath;
		}

		#endregion
	}
}
