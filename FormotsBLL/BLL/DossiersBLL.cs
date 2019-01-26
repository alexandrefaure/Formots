using System.Collections.ObjectModel;
using FormotsCommon.DTO;
using FormotsDAL.DAL;

namespace FormotsBLL.BLL
{
    public class DossiersBLL
    {
        protected static DossiersBLL _current = new DossiersBLL();

        public static DossiersBLL Current
        {
            get
            {
                return _current;
            }
        }

        public ObservableCollection<DossierDto> GetDossiersList()
        {
            return DossiersDAL.GetAllDossiers();
        }

        public OperationResult<DossierDto> DeleteDossier(DossierDto dossier)
        {
            return DossiersDAL.DeleteDossier(dossier);
        }

        public OperationResult<DossierDto> AddOrUpdateDossier(DossierDto dossier)
        {
            return DossiersDAL.AddOrUpdateDossier(dossier);
        }

    }
}
