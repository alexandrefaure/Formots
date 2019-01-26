using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FormotsCommon.DTO;

namespace FormotsDAL.DAL
{
    public class MedecinAppelantsDAL
    {
        public static ObservableCollection<MedecinAppelantDto> GetMedecinAppelantsList()
        {
            using (var context = new Entities())
            {
                var medecinAppelantsBddList = from u in context.appelant_medecins
                        .Include(m => m.dossiers)
                    select u;

                var medecinAppelantDtoList = new ObservableCollection<MedecinAppelantDto>();
                foreach (var medecinAppelant in medecinAppelantsBddList)
                {
                    var medecinAppelantDto = AutoMapper.Mapper.Map<MedecinAppelantDto>(medecinAppelant);
                    var correspondingDossier =
                        context.dossiers.SingleOrDefault(d => d.IdMedecinAppelant == medecinAppelant.Id);
                    medecinAppelantDto.NumeroAnonymatGlobal = correspondingDossier?.NumeroAnonymatGlobal;
                    medecinAppelantDtoList.Add(medecinAppelantDto);
                }

                return new ObservableCollection<MedecinAppelantDto>(medecinAppelantDtoList);
            }
        }

        public static OperationResult<MedecinAppelantDto> AddOrUpdateMedecinAppelant(MedecinAppelantDto medecinAppelant)
        {
            try
            {
                MedecinAppelantDto updatedMedecinAppelantDto;
                using (var context = new Entities())
                {
                    var medecinAppelantToAddOrUpdate = AutoMapper.Mapper.Map<appelant_medecins>(medecinAppelant);
                    context.Entry(medecinAppelantToAddOrUpdate).State = medecinAppelantToAddOrUpdate.Id == 0 ? EntityState.Added : EntityState.Modified;
                    context.appelant_medecins.AddOrUpdate(medecinAppelantToAddOrUpdate);
                    context.SaveChanges();

                    updatedMedecinAppelantDto = AutoMapper.Mapper.Map<MedecinAppelantDto>(medecinAppelantToAddOrUpdate);
                }
                return OperationResult<MedecinAppelantDto>.CreateSuccessResult(updatedMedecinAppelantDto);
            }
            catch (Exception e)
            {
                return OperationResult<MedecinAppelantDto>.CreateFailure(e.Message);
            }
        }

        public static OperationResult<MedecinAppelantDto> DeleteMedecinAppelant(MedecinAppelantDto medecinAppelant)
        {
            try
            {
                using (var context = new Entities())
                {
                    var medecinAppelantToDelete = AutoMapper.Mapper.Map<appelant_medecins>(medecinAppelant);
                    context.Entry(medecinAppelantToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                    return OperationResult<MedecinAppelantDto>.CreateSuccessResult(medecinAppelant);
                }
            }
            catch (Exception e)
            {
                return OperationResult<MedecinAppelantDto>.CreateFailure(e.Message);
            }
        }

        public static OperationResult<MedecinAppelantDto> GetMedecinAppelantById(int idMedecinAppelant)
        {
            try
            {
                MedecinAppelantDto medecinAppelantDto;
                using (var context = new Entities())
                {
                    var medecinAppelantBdd = context.appelant_medecins.SingleOrDefault(m => m.Id == idMedecinAppelant);
                    medecinAppelantDto = AutoMapper.Mapper.Map<MedecinAppelantDto>(medecinAppelantBdd);
                }
                return OperationResult<MedecinAppelantDto>.CreateSuccessResult(medecinAppelantDto);
            }
            catch (Exception e)
            {
                return OperationResult<MedecinAppelantDto>.CreateFailure(e.Message);
            }
        }
    }
}
