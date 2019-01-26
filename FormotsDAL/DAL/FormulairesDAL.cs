using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FormotsCommon;
using FormotsCommon.DTO;

namespace FormotsDAL.DAL
{
    public class FormulairesDAL
    {
        public static ObservableCollection<FormulaireDto> GetFormulairesList()
        {
            var formulairesList = new List<FormulaireDto>();
            using (var context = new Entities())
            {
                formulairesList.AddRange(AutoMapper.Mapper.Map<List<FormulaireActDto>>(context.formulaire_act));
                formulairesList.AddRange(AutoMapper.Mapper.Map<List<FormulaireCfpDto>>(context.formulaire_cfp));
                formulairesList.AddRange(AutoMapper.Mapper.Map<List<FormulaireCdaDto>>(context.formulaire_cda));
                formulairesList.AddRange(AutoMapper.Mapper.Map<List<FormulaireEcvDto>>(context.formulaire_ecv));
                formulairesList.AddRange(AutoMapper.Mapper.Map<List<FormulaireSeoDto>>(context.formulaire_seo));
                formulairesList.AddRange(AutoMapper.Mapper.Map<List<FormulaireSfaDto>>(context.formulaire_sfa));
                return new ObservableCollection<FormulaireDto>(formulairesList);
            }
        }

        public static ObservableCollection<FormulaireDto> GetFormulairesListByDossierId(int dossierId)
        {
            var formulairesList = new List<FormulaireDto>();
            using (var context = new Entities())
            {
                #region Formulaires ACT

                var formulairesActList = from u in context.formulaire_act
                        .Include(d => d.dossiers)
                        .Where(d => d.dossiers.Id == dossierId)
                    select u;

                var formulairesActDtoList = AutoMapper.Mapper.Map<List<FormulaireActDto>>(formulairesActList);
                foreach (var formulaireActDto in formulairesActDtoList)
                {
                    formulaireActDto.Type = FormulaireType.ACcueilTelephonique;
                }
                formulairesList.AddRange(formulairesActDtoList);

                #endregion

                #region Formulaires CFP

                var formulairesCfpList = from u in context.formulaire_cfp
                        .Include(d => d.dossiers)
                        .Where(d => d.dossiers.Id == dossierId)
                    select u;

                var formulairesCfpDtoList = AutoMapper.Mapper.Map<List<FormulaireCfpDto>>(formulairesCfpList);
                foreach (var formulaireCfpDto in formulairesCfpDtoList)
                {
                    formulaireCfpDto.Type = FormulaireType.ContexteFamilialProfessionnel;
                }
                formulairesList.AddRange(formulairesCfpDtoList);

                #endregion

                #region Formulaires CDA

                var formulairesCdaList = from u in context.formulaire_cda
                        .Include(d => d.dossiers)
                        .Where(d => d.dossiers.Id == dossierId)
                    select u;

                var formulairesCdaDtoList = AutoMapper.Mapper.Map<List<FormulaireCdaDto>>(formulairesCdaList);
                foreach (var formulaireCdaDto in formulairesCdaDtoList)
                {
                    formulaireCdaDto.Type = FormulaireType.ContexteFamilialProfessionnel;
                }
                formulairesList.AddRange(formulairesCdaDtoList);

                #endregion

                #region Formulaires ECV

                var formulairesEcvList = from u in context.formulaire_ecv
                        .Include(d => d.dossiers)
                        .Where(d => d.dossiers.Id == dossierId)
                    select u;

                var formulairesEcvDtoList = AutoMapper.Mapper.Map<List<FormulaireEcvDto>>(formulairesEcvList);
                foreach (var formulaireEcvDto in formulairesEcvDtoList)
                {
                    formulaireEcvDto.Type = FormulaireType.EvaluationConsequenceVecu;
                }
                formulairesList.AddRange(formulairesEcvDtoList);

                #endregion

                #region Formulaires SEO

                var formulairesSeoList = from u in context.formulaire_seo
                        .Include(d => d.dossiers)
                        .Where(d => d.dossiers.Id == dossierId)
                    select u;

                var formulairesSeoDtoList = AutoMapper.Mapper.Map<List<FormulaireSeoDto>>(formulairesSeoList);
                foreach (var formulaireSeoDto in formulairesSeoDtoList)
                {
                    formulaireSeoDto.Type = FormulaireType.SuiviEntretiensOrientations;
                }
                formulairesList.AddRange(formulairesSeoDtoList);

                #endregion

                #region Formulaires SFA

                var formulairesSfaList = from u in context.formulaire_sfa
                        .Include(d => d.dossiers)
                        .Where(d => d.dossiers.Id == dossierId)
                    select u;

                var formulairesSfaDtoList = AutoMapper.Mapper.Map<List<FormulaireSfaDto>>(formulairesSfaList);
                foreach (var formulaireSfaDto in formulairesSfaDtoList)
                {
                    formulaireSfaDto.Type = FormulaireType.BilanSyntheseFinAccompagnement;
                }
                formulairesList.AddRange(formulairesSfaDtoList);

                #endregion

                return new ObservableCollection<FormulaireDto>(formulairesList);
            }
        }

        public static OperationResult<FormulaireDto> DeleteFormulaire<C, T>(FormulaireDto formulaireDto) where T : FormulaireDto where C : class
        {
            try
            {
                using (var context = new Entities())
                {
                    var formulaireToDelete = AutoMapper.Mapper.Map<C>(formulaireDto);
                    context.Entry(formulaireToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                return OperationResult<FormulaireDto>.CreateSuccessResult(formulaireDto);
            }
            catch (Exception e)
            {
                return OperationResult<FormulaireDto>.CreateFailure(e.Message);
            }
        }

        public static OperationResult<FormulaireDto> AddOrUpdateFormulaire<C,T>(FormulaireDto formulaireDto) where T : FormulaireDto where C : class
        {
            try
            {
                FormulaireDto updatedFormulaireDto;
              
                using (var context = new Entities())
                {
                    var dbSet = context.Set<C>();
                    var formulaireToAddOrUpdate = AutoMapper.Mapper.Map<C>(formulaireDto);

                    context.Entry(formulaireToAddOrUpdate).State = (int) EntityHelper.GetPropertyValue(formulaireToAddOrUpdate,"Id") == 0 ? EntityState.Added : EntityState.Modified;
                    dbSet.AddOrUpdate(formulaireToAddOrUpdate);
                    context.SaveChanges();

                    updatedFormulaireDto = AutoMapper.Mapper.Map<T>(formulaireToAddOrUpdate);
                }
                return OperationResult<FormulaireDto>.CreateSuccessResult(updatedFormulaireDto);
            }
            catch (Exception e)
            {
                return OperationResult<FormulaireDto>.CreateFailure(e.Message);
            }
        }
    }

    public class EntityHelper
    {
        public static object GetPropertyValue(object formulaireToAddOrUpdate, string propertyName)
        {
            var type = formulaireToAddOrUpdate.GetType();
            var property = type.GetProperty(propertyName);
            return property?.GetValue(formulaireToAddOrUpdate);
        }
    }
}
