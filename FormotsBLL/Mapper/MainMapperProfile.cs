using System;
using System.Linq;
using AutoMapper;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace FormotsBLL.Mapper
{
    public class MainMapperProfile
    {
        public void ConfigureObjectMapper()
        {
            /* Chargement dynamique des profiles de mapping Automapper, 
             * parcours tout les assembly a la recherche d'une classe héritant de Profile
             * ATTENTION: charge uniquement les type chargés, donc doit impérativement etre appellé apres "ModuleHandler.Current.InitializeModules();" (MEF charge les assemblies référencés)
             */
            var automapperProfileType = typeof(Profile);
            var profiles = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => t.IsSubclassOf(automapperProfileType) && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(x => Activator.CreateInstance(x))
                .Cast<Profile>();
            AutoMapper.Mapper.Initialize(cfg => profiles.ForEach(x => cfg.AddProfile(x)));
        }
    }
}