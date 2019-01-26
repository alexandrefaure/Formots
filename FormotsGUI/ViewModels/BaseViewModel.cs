using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using FormotsBLL.BLL;
using FormotsCommon.Utils;
using FormotsGUI.ViewModels.Users;
using FormotsGUI.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsGUI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private DialogService _dialogService;

        public BaseViewModel()
        {
            _dialogService = new DialogService();
        }

        public void AsynchroneUpdateList()
        {
            ShowProgressBar = true;
            IsGridVisible = Visibility.Collapsed;
            var bgworker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            bgworker.DoWork += (s, e) =>
            {
                DoWork(bgworker);
                //Thread.Sleep(1000);//Decommenter pour vérifier que la ProgressBar fonctionne
            };
            // Méthode de mise à jour de la liste de façon asynchrone avec 
            //le premier paramètre non null de ProgressChanged et le second null.
            bgworker.ProgressChanged += (s, e) => ProgressChanged(e, null);
            bgworker.RunWorkerCompleted += (s, e) =>
            {
                RunWorkerCompleted();
                ShowProgressBar = false;
                IsGridVisible = Visibility.Visible;
            };
            bgworker.RunWorkerAsync();
        }

        private Visibility _isGridVisible;
        public Visibility IsGridVisible
        {
            get => _isGridVisible;
            set
            {
                if (_isGridVisible == value)
                {
                    return;
                }
                _isGridVisible = value;
                OnPropertyChanged("IsGridVisible");
            }
        }

        public void SynchroneUpdateList()
        {
            // Méthode de mise à jour de la liste de façon synchrone avec 
            //le premier paramètre null de ProgressChanged et le second non null.
            var doWorkReturn = DoWork(null);
            ProgressChanged(null, doWorkReturn);
        }

        protected virtual void RunWorkerCompleted()
        {
        }

        protected virtual void ProgressChanged(ProgressChangedEventArgs e, object list)
        {
        }

        protected virtual object DoWork(BackgroundWorker bgworker)
        {
            return null;
        }

        public virtual void Add(object sender)
        {
        }

        public virtual void Edit(object sender)
        {
        }

        public virtual void Delete(object sender)
        {
        }

        protected void BaseSaveObject<T>(string objectNameToSave, T objectToSave, bool isNewEntry,
            string objectToSaveId, Converter<T, OperationResult<T>> updateFunction, BaseViewModel instance)
        {
            if (objectToSave == null)
            {
                return;
            }

            OperationResult<T> saveOperationResult = null;
            Action<string, string> exec = (s, s1) =>
            {
                //Thread.Sleep(2000);
                saveOperationResult = updateFunction(objectToSave);
            };

            var progressWindow = new ProgressWindow();
            progressWindow.SetTitle("Enregistrement en cours");
            var backgroundWorker = new BackgroundWorker();

            // set the worker to call your long-running method
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs e) => {
                exec.Invoke("path", "parameters");
            };

            // set the worker to close your progress form when it's completed
            backgroundWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) => {
                progressWindow.Close();


                if (isNewEntry)
                {
                    if (saveOperationResult.Success)
                    {
                        MessageBox.Show($"{UppercaseFirst(objectNameToSave)} {objectToSaveId} a bien été créé.",
                            "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"{UppercaseFirst(objectNameToSave)} {objectToSaveId} n'a pas pu être créé. Veuillez contacter votre administrateur.",
                            "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (saveOperationResult.Success)
                    {
                        MessageBox.Show($"{UppercaseFirst(objectNameToSave)} {objectToSaveId} a bien été modifié.",
                            "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"{UppercaseFirst(objectNameToSave)} {objectToSaveId} n'a pas pu être modifié. Veuillez contacter votre administrateur.",
                            "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                instance.AsynchroneUpdateList();
            };
            progressWindow.Show();

            // this only tells your BackgroundWorker to START working;
            // the current (i.e., GUI) thread will immediately continue,
            // which means your progress bar will update, the window
            // will continue firing button click events and all that
            // good stuff
            backgroundWorker.RunWorkerAsync();
        }


        protected void BaseDeleteObject<T>(string objectNameToDelete, T objectToDelete, string objectToDeleteId, Converter<T, OperationResult<T>> deleteFunction)
        {
            if (objectToDelete == null)
            {
                return;
            }

            var messageBox = MessageBox.Show($"Êtes-vous sûr de vouloir supprimmer {objectNameToDelete} {objectToDeleteId} ?",
                "Confirmation", MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (messageBox == MessageBoxResult.No )
            {
                return;
            }

            OperationResult<T> deleteUserDtOperationResult = null;
            Action<string, string> exec = (s, s1) =>
            {
                //Thread.Sleep(2000);
                deleteUserDtOperationResult = deleteFunction(objectToDelete);
            };

            var progressWindow = new ProgressWindow();
            progressWindow.SetTitle("Suppression en cours");
            var backgroundWorker = new BackgroundWorker();

            // set the worker to call your long-running method
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs e) => {
                exec.Invoke("path", "parameters");
            };

            // set the worker to close your progress form when it's completed
            backgroundWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) => {
                progressWindow.Close();

                if (deleteUserDtOperationResult.Success)
                {
                    MessageBox.Show($"{UppercaseFirst(objectNameToDelete)} {objectToDeleteId} a bien été supprimé.",
                        "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        $"{UppercaseFirst(objectNameToDelete)} {objectToDeleteId} n'a pas pu être supprimé.",
                        "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                AsynchroneUpdateList();
            };
            progressWindow.Show();

            // this only tells your BackgroundWorker to START working;
            // the current (i.e., GUI) thread will immediately continue,
            // which means your progress bar will update, the window
            // will continue firing button click events and all that
            // good stuff
            backgroundWorker.RunWorkerAsync();
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public virtual void Close(object sender)
        {
            OnClosingRequest();
        }

        #region Commands

        private ICommand _addCommand;

        public ICommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = Add });
            }
        }

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = Delete
                });
            }
        }

        private ICommand _editCommand;

        public ICommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = Edit });
            }
        }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = Close });
            }
        }

        #endregion

        private string _dialogTitle;
        public virtual string DialogTitle
        {
            get => _dialogTitle;
            set
            {
                if (Equals(value, _dialogTitle))
                {
                    return;
                }

                _dialogTitle = value;
                OnPropertyChanged("DialogTitle");
            }
        }

        private string _windowTitle;

        public virtual string WindowTitle
        {
            get => _windowTitle;
            set
            {
                if (Equals(value, _windowTitle))
                {
                    return;
                }

                _windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

        private bool _showProgressBar;

        public bool ShowProgressBar
        {
            get => _showProgressBar;
            set
            {
                if (_showProgressBar == value)
                {
                    return;
                }
                _showProgressBar = value;
                OnPropertyChanged("ShowProgressBar");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event EventHandler ClosingRequest;

        public void OnClosingRequest()
        {
            if (ClosingRequest != null)
            {
                ClosingRequest(this, EventArgs.Empty);
            }
        }
    }
}
