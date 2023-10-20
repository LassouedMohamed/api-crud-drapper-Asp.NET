using api_crud_drapper.Common;
using api_crud_drapper.IServices;
using api_crud_drapper.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace api_crud_drapper.Services
{
    public class ProduitService : IProduitService
    {
        Produit _oProduit = new Produit();
        List<Produit> _oProduits = new List<Produit>();
        public string delete(int id)
        {
            string message = "";
            try
            {
                _oProduit = new Produit()
                {
                    Id = id 
                };
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var oProduits = con.Query<Produit>("SP_Produit",
                        this.setParameters(_oProduit, (int)OperationType.Delete),
                        commandType: CommandType.StoredProcedure
                        );
                    message = "Produit Supprimé";
                }
            }
            catch (Exception ex) { 
                message = ex.Message;
            }
            return message;
        }

        public List<Produit> getAll()
        {
            _oProduits = new List <Produit>();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                var oProduit = con.Query<Produit>("select * from Produit").ToList();
                if (oProduit != null && oProduit.Count > 0)
                    _oProduits = oProduit;
                return _oProduits;
            }
        }

        public Produit getById(int id)
        {
            _oProduit = new Produit();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                var oProduit = con.Query<Produit>("select * from Produit where id =" + id).ToList();
                if(oProduit != null && oProduit.Count > 0)
                    _oProduit = oProduit[0];
                return _oProduit;
            }
        }

        public Produit save(Produit oProduit)
        {
            try
            {
                int operationType = Convert.ToInt32(oProduit.Id ==0 ? OperationType.Insert : OperationType.Update);
                using(IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if(con.State == ConnectionState.Closed) 
                        con.Open();
                    var oProduits = con.Query<Produit>("SP_Produit",
                        this.setParameters(oProduit, operationType),
                        commandType: CommandType.StoredProcedure);
                    if (oProduits != null && oProduits.Count() > 0)
                        _oProduits.FirstOrDefault();
                }

            }catch (Exception ex)
            {
                _oProduit.Message = ex.Message;
            }
            return _oProduit;
        }

        private object setParameters(Produit oProduit, int operationType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", oProduit.Id);
            parameters.Add("@Nom", oProduit.Nom);
            parameters.Add("@Prix", oProduit.Prix);
            parameters.Add("@OperationType", operationType);
            return parameters;
        }
    }
}
