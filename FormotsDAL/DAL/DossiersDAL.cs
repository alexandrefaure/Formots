using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FormotsCommon.DTO;

namespace FormotsDAL.DAL
{
    public class DossiersDAL
    {
        public static ObservableCollection<DossierDto> GetAllDossiers()
        {
            using (var context = new Entities())
            {
                var dossiersBddList = from u in context.dossiers
                        .Include(d => d.appelant_medecins)
                        .Include(d => d.global_users)
                        .Include(d => d.formulaire_act)
                        .Include(d => d.formulaire_cda)
                        .Include(d => d.formulaire_cfp)
                        .Include(d => d.formulaire_ecv)
                        .Include(d => d.formulaire_seo)
                        .Include(d => d.formulaire_sfa)
                    select new DossierDto
                    {
                        Id = u.Id,
                        NumeroAnonymatGlobal = u.NumeroAnonymatGlobal,
                        IdMedecinAppelant = u.IdMedecinAppelant,
                        IdUser = u.global_users.Id,
                        UserDto = new UserDto
                        {
                            Id = u.global_users.Id,
                            FirstName = u.global_users.FirstName,
                            LastName = u.global_users.LastName,
                            Login = u.global_users.Login
                        },
                        MedecinAppelantDto = new MedecinAppelantDto
                        {
                            Id = u.appelant_medecins.Id,
                            Nom = u.appelant_medecins.Nom,
                            Prenom = u.appelant_medecins.Prenom,
                            DateNaissance = u.appelant_medecins.DateNaissance,
                            Adresse = u.appelant_medecins.Adresse,
                            Email = u.appelant_medecins.Email,
                            Genre = u.appelant_medecins.Genre,
                            NumeroTelephoneFixe = u.appelant_medecins.NumeroTelephoneFixe,
                            NumeroTelephonePortable = u.appelant_medecins.NumeroTelephonePortable,
                            TiersNom = u.appelant_medecins.TiersNom,
                            TiersLienParente = u.appelant_medecins.TiersLienParente,
                            TiersTelephone = u.appelant_medecins.TiersTelephone,
                            TiersEmail = u.appelant_medecins.TiersEmail,
                        },
                        FormulairesCount = u.formulaire_act.Count + u.formulaire_cda.Count + u.formulaire_cfp.Count + u.formulaire_ecv.Count + u.formulaire_seo.Count + u.formulaire_sfa.Count
                    };
                
                return new ObservableCollection<DossierDto>(dossiersBddList);
            }
        }

        public static OperationResult<DossierDto> DeleteDossier(DossierDto dossier)
        {
            try
            {
                using (var context = new Entities())
                {
                    var dossierToDelete = AutoMapper.Mapper.Map<dossiers>(dossier);
                    context.Entry(dossierToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                    return OperationResult<DossierDto>.CreateSuccessResult(dossier);
                }
            }
            catch (Exception e)
            {
                return OperationResult<DossierDto>.CreateFailure(e.Message);
            }
        }

        public static OperationResult<DossierDto> AddOrUpdateDossier(DossierDto dossier)
        {
            try
            {
                DossierDto savedDossierDto;
                using (var context = new Entities())
                {
                    var dossierToAddOrUpdate = AutoMapper.Mapper.Map<dossiers>(dossier);
                    context.Entry(dossierToAddOrUpdate).State = dossierToAddOrUpdate.Id == 0 ? EntityState.Added : EntityState.Modified;
                    context.dossiers.AddOrUpdate(dossierToAddOrUpdate);
                    context.SaveChanges();

                    var numeroAnonymatGlobal = GetNumeroAnonymatGlobalDossier(dossierToAddOrUpdate);
                    dossierToAddOrUpdate.NumeroAnonymatGlobal = numeroAnonymatGlobal;
                    context.SaveChanges();

                    savedDossierDto = AutoMapper.Mapper.Map<DossierDto>(dossierToAddOrUpdate);
                }
                return OperationResult<DossierDto>.CreateSuccessResult(savedDossierDto);
            }
            catch (Exception e)
            {
                return OperationResult<DossierDto>.CreateFailure(e.Message);
            }
        }

        public static string GetNumeroAnonymatGlobalDossier(dossiers dossier)
        {
            var lastDossierId = dossier.Id;
            var getMedecinAppelantByIdResult = MedecinAppelantsDAL.GetMedecinAppelantById((int) dossier.IdMedecinAppelant);
            var dossierMedecinAppelant = getMedecinAppelantByIdResult.Result;
            var prenomTrunc = dossierMedecinAppelant.Prenom.Substring(0, 1).ToUpper();
            var nomTrunc = dossierMedecinAppelant.Nom.Substring(0, 2).ToUpper();
            var numeroAnonymatGlobalDossier = string.Concat(prenomTrunc, nomTrunc, "-", $"{lastDossierId + 1:D4}");
            return numeroAnonymatGlobalDossier;
        }
    }
}
