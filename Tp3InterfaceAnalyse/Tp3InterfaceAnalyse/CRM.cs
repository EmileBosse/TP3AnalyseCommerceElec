using System;
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

        public  List<Entity> RetrieveEmployes()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_employeuniversietjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_employeuniversietjkweid");
            queryExp.ColumnSet.Columns.Add("new_name");
            queryExp.ColumnSet.Columns.Add("new_prenom");
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

        public List<Entity> RetrieveMissions()
        {
            var result = new List<Entity>();

            QueryExpression queryExp = new QueryExpression();
            queryExp.EntityName = "new_missionjkwe";
            queryExp.ColumnSet = new ColumnSet();
            queryExp.ColumnSet.Columns.Add("new_missionjkweid");
            queryExp.ColumnSet.Columns.Add("new_name");
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
    }
}
