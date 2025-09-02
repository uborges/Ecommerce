using Ecommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repositories
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>();
        private static int _nextId = 1;

        public List<Product> GetAll()
        {
            return _products.OrderBy(p => p.Name).ToList();
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            if (product.Name == null || product.Name.Length < 3 || product.Name.Length > 100)
                throw new System.Exception("O nome deve ter entre 3 e 100 caracteres.");

            if (product.Price < 0)
                throw new System.Exception("O preço não pode ser negativo.");

            if (product.Stock < 0)
                throw new System.Exception("O estoque não pode ser negativo.");

            product.Id = _nextId++;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct == null)
                throw new System.Exception("Produto não encontrado para atualização.");

            if (product.Stock == 0)
            {
                throw new System.Exception("Não é permitido zerar o estoque de um produto através da atualização.");
            }

            if (string.IsNullOrWhiteSpace(product.Name) || product.Price < 0 || product.Stock < 0)
                throw new System.Exception("Valores inválidos para atualização.");

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.Category = product.Category;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product == null)
            {
                return false; 
            }
            _products.Remove(product);
            return true; 
        }

        public List<Product> GetByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return new List<Product>();
            }

            return _products
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Name)
                .ToList();
        }

    }
}
