using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Models;
using System.Web.ModelBinding;

namespace WingtipToys
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Product> GetProduct([QueryString("productID")] int? productId)
        {
            var _db = new ProductContext();
            IQueryable<Product> query = _db.Products;
            query = VerificarSeHaProdutoSelecionado(productId, query);

            return query;
        }

        private static IQueryable<Product> VerificarSeHaProdutoSelecionado(int? productId, IQueryable<Product> query)
        {
            if (IDdoProdutoFoiEncontrado(productId))
                query = MostrarOProdutoEncontradoNaTela(productId, query);
            else
                query = null;
            return query;
        }

        private static IQueryable<Product> MostrarOProdutoEncontradoNaTela(int? productId, IQueryable<Product> query)
        {
            query = query.Where(p => p.ProductID == productId);
            return query;
        }

        private static bool IDdoProdutoFoiEncontrado(int? productId)
        {
            return productId.HasValue && productId > 0;
        }
    }
}