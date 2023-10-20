using api_crud_drapper.Models;
using System.Xml.Linq;

namespace api_crud_drapper.IServices
{
    public interface IProduitService
    {
        Produit save(Produit oProduit);
        List<Produit> getAll();
        Produit getById(int id);
        string delete(int id);
    }
}
