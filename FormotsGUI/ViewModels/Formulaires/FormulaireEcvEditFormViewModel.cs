using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FormotsCommon;
using FormotsCommon.DTO;
using FormotsCommon.Utils;
using MOTS.Properties;

namespace FormotsGUI.ViewModels.Formulaires
{
    public class FormulaireEcvEditFormViewModel : FormulaireBaseViewModel
    {
        public FormulaireEcvEditFormViewModel()
        {
            _instance = this;
        }

        public override void Initialize()
        {
            ((FormulaireEcvDto)Formulaire).IsAutrePrescriptionPsychotropesVisible = Visibility.Collapsed;
        }

        private ICommand _openMbiPdfCommand;

        public ICommand OpenMbiPdfCommand
        {
            get
            {
                return _openMbiPdfCommand ?? (_openMbiPdfCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = OpenMbiPdf });
            }
        }

        private ICommand _openRudPdfCommand;

        public ICommand OpenRudPdfCommand
        {
            get
            {
                return _openRudPdfCommand ?? (_openRudPdfCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = OpenRudPdf });
            }
        }

        private void OpenMbiPdf(object obj)
        {
            ShowPdfFile(Resources.MBI, "MbiGuide.pdf");
        }

        private void OpenRudPdf(object obj)
        {
            ShowPdfFile(Resources.RUD, "RudGuide.pdf");
        }

        private static void ShowPdfFile(byte[] pdfFileBytes, string fileName)
        {
            try
            {
                var ms = new MemoryStream(pdfFileBytes);

                //Create PDF File From Binary of resources folders help.pdf
                var f = new FileStream(fileName, FileMode.OpenOrCreate);

                //Write Bytes into Our Created help.pdf
                ms.WriteTo(f);
                f.Close();
                ms.Close();

                // Finally Show the Created PDF from resources 
                Process.Start(fileName);
            }
            catch (Exception error)
            {
                MessageBox.Show("Impossible d'ouvrir le fichier. Merci de vérifier que celui n'est pas déjà ouvert.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public List<KeyValuePair<int, string>> NiveauRisqueRudList => FormulaireReferential.GetNiveauRisqueRudList();
    }
}