using api_crud_drapper.IServices;
using api_crud_drapper.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_crud_drapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {

        private IProduitService _oProduitService;
        public ProduitsController(IProduitService oProduitService)
        {
            _oProduitService = oProduitService;
        }
        // GET: api/<ProduitsController>
        [HttpGet]
        public IEnumerable<Produit> Get()
        {
            return _oProduitService.getAll();
        }

        // GET api/<ProduitsController>/5
        [HttpGet("{id}")]
        public Produit Get(int id)
        {
            return _oProduitService.getById(id);
        }

        // POST api/<ProduitsController>
        [HttpPost]
        public Produit Post([FromBody] Produit pdt)
        {
            if (ModelState.IsValid)
                return _oProduitService.save(pdt);
            return null;
        }

        // PUT api/<ProduitsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProduitsController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _oProduitService.delete(id);
        }
    }
}
