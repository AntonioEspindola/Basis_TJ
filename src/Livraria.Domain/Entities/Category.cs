using Livraria.Domain.Validation;

namespace Livraria.Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; }

    public Category(string name)
    {
        ValidateDomain(name);
    }

    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Valor de Id inválido.");
        Id = id;
        ValidateDomain(name);
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }
    public ICollection<Product> Products { get; set; }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Nome inválido.Nome é obrigatório");

        DomainExceptionValidation.When(name.Length < 3,
           "Nome inválido, minimo 3 characters");

        Name = name;
    }
}
