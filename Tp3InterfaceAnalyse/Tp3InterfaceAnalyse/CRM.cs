﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Deployment;

namespace Tp3InterfaceAnalyse
{
    public class CRM
    {

        //CRM related
        OrganizationService orgService = null;
        Entity lead = null;


        /**  Connection related  **/
        #region connection
        public void Connexion()
        {
            try
            {
                if (this.orgService == null)
                {
                    // Obtient information de connexion à CRM via 
                    // le fichier de configuration app.config.
                    String connectionString = GetServiceConfiguration();
                    if (connectionString != null)
                    {
                        // Établir une connexion CRM avec le service  CrmConnection.
                        Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse(connectionString);
                        orgService = new OrganizationService(connection);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception :" + exception.Message);
            }
        }

        public static String GetServiceConfiguration()
        {   // Parcourt le fichier de configuration et cherche la bonne ligne pour CRM
            int count = ConfigurationManager.ConnectionStrings.Count;
            List<KeyValuePair<String, String>> filteredConnectionStrings =
                new List<KeyValuePair<String, String>>();

            for (int a = 0; a < count; a++)
            {
                if (isValidConnectionString(ConfigurationManager.ConnectionStrings[a].ConnectionString))
                    filteredConnectionStrings.Add
                        (new KeyValuePair<string, string>
                            (ConfigurationManager.ConnectionStrings[a].Name,
                            ConfigurationManager.ConnectionStrings[a].ConnectionString));
            }
            if (filteredConnectionStrings.Count == 0)
            {
                Console.WriteLine("Un fichier de configuration app.config doit exister dans le dossier de la solution ");
                return null;
            }
            if (filteredConnectionStrings.Count == 1)
            {
                return filteredConnectionStrings[0].Value;
            }
            return null;
        }

        private static Boolean isValidConnectionString(String connectionString)
        {
            //Vérifie si c'est la bonne ligne pour CRM
            if (connectionString.Contains("Url=") ||
                connectionString.Contains("Server=") ||
                connectionString.Contains("ServiceUri="))
                return true;
            return false;
        }
        #endregion

        #region getTheId
        public Guid getEmployeID(string nomEmploye, string prenomEmploye)
        {
            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_employeuniversietjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_employeuniversietjkweid");
            ConditionExpression conExp1 = new ConditionExpression();
            conExp1.AttributeName = "new_name";
            conExp1.Operator = ConditionOperator.Equal;
            conExp1.Values.Add(nomEmploye);
            ConditionExpression conExp2 = new ConditionExpression();
            conExp2.AttributeName = "new_prenom";
            conExp2.Operator = ConditionOperator.Equal;
            conExp2.Values.Add(prenomEmploye);
            FilterExpression fep = new FilterExpression();
            fep.Conditions.Add(conExp1);
            fep.Conditions.Add(conExp2);
            queryExp.Criteria.AddFilter(fep);
            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                Console.WriteLine("Tu as trouvé l'employé \"" + nomEmploye + ", " + prenomEmploye + "\".");
                Entity recordId = contCollection.Entities[0];
                return recordId.Id;
            }
            else
            {
                Console.WriteLine("L'employé \"" + nomEmploye + ", " + prenomEmploye + "\" est introuvable");
                return new Entity().Id;
            }
        }
        #endregion

        #region testGetter

        public Entity RetrieveEntityById(string EntityLogicalName, Guid guidEntityId)
        {
            Entity entityRecord = orgService.Retrieve(EntityLogicalName, guidEntityId, new ColumnSet(true));
            return entityRecord;
        }

        #endregion

        #region Employé université
        public List<Entity> RetrieveEmployes()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_employeuniversietjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_employeuniversietjkweid");
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
            queryExp.ColumnSet.Columns.Add("new_adresse");
            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;
            }
            else
            {
                return result;
            }
        }

        public void CreateEmploye(Employe employe)
        {
            Entity employeEntity = new Entity("new_employeuniversietjkwe");
            employeEntity["new_name"] = employe.nom;
            employeEntity["new_prenom"] = employe.prenom;
            employeEntity["new_adresse"] = employe.adresse;

            Guid id = orgService.Create(employeEntity);
            Console.WriteLine("Et voilà !! : " + id);
        }

        private Entity findEmployeEntity(Employe employe)
        {
            return orgService.Retrieve("new_employeuniversietjkwe", employe.id, new ColumnSet(true));
        }

        public void UpdateEmploye(Employe employe)
        {
            Entity employeEntity = findEmployeEntity(employe);

            employeEntity["new_name"] = employe.nom;
            employeEntity["new_prenom"] = employe.prenom;
            employeEntity["new_adresse"] = employe.adresse;

            orgService.Update(employeEntity);
        }


        #endregion

        #region Étudiant
        public List<Entity> RetrieveEtudiants()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etudiantjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
            queryExp.ColumnSet.Columns.Add("new_adresse");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_ville");
            queryExp.ColumnSet.Columns.Add("new_codepermanent");
            queryExp.ColumnSet.Columns.Add("new_etudiantjkweid");
            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;
            }
            else
            {
                return result;
            }
        }

        public void CreateEtudiant(Etudiant etudiant)
        {
            Entity etudEnti = new Entity("new_etudiantjkwe");
            etudEnti["new_name"] = etudiant.Nom;
            etudEnti["new_prenom"] = etudiant.Prenom;
            etudEnti["new_adresse"] = etudiant.Adresse;
            etudEnti["new_pays"] = etudiant.Pays;
            etudEnti["new_ville"] = etudiant.Ville;
            etudEnti["new_codepermanent"] = etudiant.CodePermanent;

            Guid id = orgService.Create(etudEnti);
        }

        private Entity findEtudiantEntity(Etudiant etudiant)
        {
            return orgService.Retrieve("new_etudiantjkwe", etudiant.Identifiant, new ColumnSet(true));
        }

        public void UpdateEtudiant(Etudiant etudiant)
        {
            Entity etudiantEntity = findEtudiantEntity(etudiant);

            etudiantEntity["new_name"] = etudiant.Nom;
            etudiantEntity["new_prenom"] = etudiant.Prenom;
            etudiantEntity["new_adresse"] = etudiant.Adresse;
            etudiantEntity["new_pays"] = etudiant.Pays;
            etudiantEntity["new_ville"] = etudiant.Ville;
            etudiantEntity["new_codepermanent"] = etudiant.CodePermanent;

            orgService.Update(etudiantEntity);
        }
        #endregion

        #region Programme étude
        public List<Entity> RetrieveProgrammes()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_programme_etude_jkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_code");
            queryExp.ColumnSet.Columns.Add("new_cycle");
            queryExp.ColumnSet.Columns.Add("new_departement");
            queryExp.ColumnSet.Columns.Add("new_programme_etude_jkweid");
            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;
            }
            else
            {
                return result;
            }
        }

        public void CreateProgramme(Programme programme)
        {
            Entity progEntity = new Entity("new_programme_etude_jkwe");
            progEntity["new_name"] = programme.Nom;
            progEntity["new_code"] = programme.Code;
            progEntity["new_cycle"] = programme.Cycle;
            progEntity["new_departement"] = programme.Departement;
            progEntity["new_programme_etude_jkweid"] = programme.Identifiant;

            Guid id = orgService.Create(progEntity);
        }

        private Entity findProgrammeEntity(Programme programme)
        {
            return orgService.Retrieve("new_programme_etude_jkwe", programme.Identifiant, new ColumnSet(true));
        }

        public void UpdateProgramme(Programme programme)
        {
            Entity progEntity = findProgrammeEntity(programme);

            progEntity["new_name"] = programme.Nom;
            progEntity["new_code"] = programme.Code;
            progEntity["new_cycle"] = programme.Cycle;
            progEntity["new_departement"] = programme.Departement;
            progEntity["new_programme_etude_jkweid"] = programme.Identifiant;

            orgService.Update(progEntity);
        }
        #endregion

        #region Établissement

        public List<Entity> RetrieveEtablissements()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etablissementjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_ville");
            queryExp.ColumnSet.Columns.Add("new_etablissementjkweid");
            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;
            }
            else
            {
                return result;
            }
        }

        #region ÉtudiantOrderByPays
        public List<Entity> RetrieveEtudiantsOrderByPays()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etudiantjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
            queryExp.ColumnSet.Columns.Add("new_adresse");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_ville");
            queryExp.ColumnSet.Columns.Add("new_codepermanent");
            queryExp.ColumnSet.Columns.Add("new_etudiantjkweid");
            queryExp.Orders.Add(new OrderExpression("new_pays", OrderType.Ascending));
            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion

        #region ÉtudiantOrderByCycle
        public List<Entity> RetrieveEtudiantsOrderByCycle()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etudiantjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
            queryExp.ColumnSet.Columns.Add("new_adresse");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_ville");
            queryExp.ColumnSet.Columns.Add("new_codepermanent");
            queryExp.ColumnSet.Columns.Add("new_etudiantjkweid");
            queryExp.LinkEntities.Add(new LinkEntity("new_etudiantjkwe", "new_new_programme_etude_jkwe_new_etudiantjk", "new_etudiantjkweid", "new_etudiantjkweid", JoinOperator.Inner));
            queryExp.LinkEntities[0].Columns.AddColumn("new_new_programme_etude_jkwe_new_etudiantjkid");
            queryExp.LinkEntities[0].EntityAlias = "Programmeliees";
            queryExp.Orders.Add(new OrderExpression("new_etudiantjkweid", OrderType.Ascending));

            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion

        #region ÉtudiantOrderByProgramme
        public List<Entity> RetrieveEtudiantsOrderByProgramme()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etudiantjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
            queryExp.ColumnSet.Columns.Add("new_adresse");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_ville");
            queryExp.ColumnSet.Columns.Add("new_codepermanent");
            queryExp.ColumnSet.Columns.Add("new_etudiantjkweid");
            queryExp.LinkEntities.Add(new LinkEntity("new_etudiantjkwe", "new_new_programme_etude_jkwe_new_etudiantjk", "new_etudiantjkweid", "new_etudiantjkweid", JoinOperator.Inner));
            queryExp.LinkEntities[0].Columns.AddColumn("new_new_programme_etude_jkwe_new_etudiantjkid");
            queryExp.LinkEntities[0].EntityAlias = "Programmeliees";
            queryExp.Orders.Add(new OrderExpression("new_etudiantjkweid", OrderType.Descending));

            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion

        #region ÉtudiantOrderByEtabliesement
        public List<Entity> RetrieveEtudiantsOrderByEtabliesement()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etudiantjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
            queryExp.ColumnSet.Columns.Add("new_adresse");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_ville");
            queryExp.ColumnSet.Columns.Add("new_codepermanent");
            queryExp.ColumnSet.Columns.Add("new_etudiantjkweid");
            queryExp.Orders.Add(new OrderExpression("new_pays", OrderType.Ascending));
            queryExp.LinkEntities.Add(new LinkEntity("new_etudiantjkwe", "new_new_etablisementjkwe_new_etudiantjkwe", "new_etudiantjkweid", "new_etudiantjkweid", JoinOperator.Inner));
            queryExp.LinkEntities[0].Columns.AddColumn("new_new_etablisementjkwe_new_etudiantjkweid");
            queryExp.LinkEntities[0].Columns.AddColumn("new_new_etablisementjkweid");
            queryExp.LinkEntities[0].EntityAlias = "Etablisementliees";
            queryExp.Orders.Add(new OrderExpression("new_new_etablisementjkweid", OrderType.Ascending));

            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion

        public void CreateEtablissement(Etablissement etablissement)
        {
            Entity etablissementEntity = new Entity("new_etablissementjkwe");
            etablissementEntity["new_name"] = etablissement.Nom;
            etablissementEntity["new_pays"] = etablissement.Pays;
            etablissementEntity["new_ville"] = etablissement.Ville;
            etablissementEntity["new_etablissementjkweid"] = etablissement.Identifiant;

            Guid id = orgService.Create(etablissementEntity);
        }

        private Entity findEtablissementEntity(Etablissement etablissement)
        {
            return orgService.Retrieve("new_etablissementjkwe", etablissement.Identifiant, new ColumnSet(true));
        }

        public void UpdateEtablissement(Etablissement etablissement)
        {
            Entity progEntity = findEtablissementEntity(etablissement);

            progEntity["new_name"] = etablissement.Nom;
            progEntity["new_pays"] = etablissement.Pays;
            progEntity["new_ville"] = etablissement.Ville;
            progEntity["new_etablissementjkweid"] = etablissement.Identifiant;

            orgService.Update(progEntity);
        }

        #endregion

        #region Mission
        public List<Entity> RetrieveMissions()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_missionjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_missionjkweid");
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_datededebut");
            queryExp.ColumnSet.Columns.Add("new_datedefin");
            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
                return result;

            }
            else
            {
                return result;
            }
        }

        public void CreateMission(Mission mission)
        {
            Entity missionEntity = new Entity("new_missionjkwe");
            missionEntity["new_name"] = mission.Nom;
            missionEntity["new_pays"] = mission.Pays;
            missionEntity["new_datededebut"] = mission.DateDebut;
            missionEntity["new_datedefin"] = mission.DateFin;

            Guid id = orgService.Create(missionEntity);
        }

        private Entity findMissionEntity(Mission mission)
        {
            return orgService.Retrieve("new_missionjkwe", mission.Id, new ColumnSet(true));
        }

        public void UpdateMission(Mission mission)
        {
            Entity missionEntity = findMissionEntity(mission);

            missionEntity["new_name"] = mission.Nom;
            missionEntity["new_pays"] = mission.Pays;
            missionEntity["new_datededebut"] = mission.DateDebut;
            missionEntity["new_datedefin"] = mission.DateFin;

            orgService.Update(missionEntity);
        }


        #endregion Mission





        public List<Entity> RetrieveEtudiantForMission(string idMission)
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etudiantjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
            queryExp.ColumnSet.Columns.Add("new_adresse");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_ville");
            queryExp.ColumnSet.Columns.Add("new_codepermanent");
            queryExp.ColumnSet.Columns.Add("new_etudiantjkweid");

            LinkEntity link = queryExp.AddLink("new_new_etudiantjkwe_new_missionjkwe", "new_etudiantjkweid", "new_etudiantjkweid", JoinOperator.Inner);
            link.Columns.AddColumn("new_new_etudiantjkwe_new_missionjkweid");
            link.Columns.AddColumn("new_missionjkweid");
            link.EntityAlias = "missionsliees";
            queryExp.Criteria = new FilterExpression();
            queryExp.Criteria.AddCondition("missionsliees", "new_missionjkweid", ConditionOperator.Equal, idMission);


            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
            }
            return result;

        }

        public List<Entity> RetrieveEtudiantForEtablissement(string idEtablissement)
        {
            var result = new List<Entity>();
            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etudiantjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
            queryExp.ColumnSet.Columns.Add("new_adresse");
            queryExp.ColumnSet.Columns.Add("new_pays");
            queryExp.ColumnSet.Columns.Add("new_ville");
            queryExp.ColumnSet.Columns.Add("new_codepermanent");
            queryExp.ColumnSet.Columns.Add("new_etudiantjkweid");
            queryExp.ColumnSet.Columns.Add("new_origineid");

            ConditionExpression conExp1 = new ConditionExpression();
            conExp1.AttributeName = "new_origineid";
            conExp1.Operator = ConditionOperator.Equal;
            conExp1.Values.Add(idEtablissement);

            FilterExpression fep = new FilterExpression();
            fep.Conditions.Add(conExp1);
            queryExp.Criteria.AddFilter(fep);

            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
            }
            return result;
        }

        public List<Entity> RetrieveQuestionForEtudiant(string idEtudiant)
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_questionjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_libelle");
            queryExp.ColumnSet.Columns.Add("new_questionjkweid");

            ConditionExpression conExp1 = new ConditionExpression();
            conExp1.AttributeName = "new_etudiant_questionid";
            conExp1.Operator = ConditionOperator.Equal;
            conExp1.Values.Add(idEtudiant);

            FilterExpression fep = new FilterExpression();
            fep.Conditions.Add(conExp1);
            queryExp.Criteria.AddFilter(fep);

            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);
            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
            }
            return result;
        }

        public List<Entity> RetrieveEtablissementForMisson(string idMission)
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_etablissementjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_etablissementjkweid");

            LinkEntity link = queryExp.AddLink("new_new_etablissementjkwe_new_missionjkwe", "new_etablissementjkweid", "new_etablissementjkweid", JoinOperator.Inner);
            link.Columns.AddColumn("new_missionjkweid");
            link.EntityAlias = "etablissementsliees";
            queryExp.Criteria = new FilterExpression();
            queryExp.Criteria.AddCondition("etablissementsliees", "new_missionjkweid", ConditionOperator.Equal, idMission);

            EntityCollection contCollection = orgService.RetrieveMultiple(queryExp);

            if (contCollection.Entities.Count > 0)
            {
                result.AddRange(contCollection.Entities.ToList());
            }
            return result;
        }
    }
}
