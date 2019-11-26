using System.Globalization;

namespace Entities
{
    class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public Categoria Categoria { get; set; }

        public override string ToString()
        {
            return this.Id
                + ", "
                + this.Nome
                + ", "
                + this.Preco.ToString("F2", CultureInfo.InvariantCulture)
                + ", "
                + this.Categoria.Nome
                + ", "
                + this.Categoria.Camada;
        }
    }
}
