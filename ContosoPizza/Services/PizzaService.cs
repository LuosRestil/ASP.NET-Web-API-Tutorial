using ContosoPizza.Models;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;

    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
            .AsNoTracking()
            .ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        _context.SaveChanges();

        return newPizza;
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        var pizza = _context.Pizzas.Find(PizzaId);
        var topping = _context.Toppings.Find(ToppingId);

        if (pizza is null || topping is null)
        {
            throw new NullReferenceException("Pizza or topping does not exist");
        }

        if(pizza.Toppings is null)
        {
            pizza.Toppings = new List<Topping>();
        }

        pizza.Toppings.Add(topping);

        _context.Pizzas.Update(pizza);
        _context.SaveChanges();
    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        var pizza = _context.Pizzas.Find(PizzaId);
        var sauce = _context.Sauces.Find(SauceId);

        if (pizza is null || sauce is null)
        {
            throw new NullReferenceException("Pizza or sauce does not exist");
        }

        pizza.Sauce = sauce;

    _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);
        if (pizzaToDelete is not null)
        {
            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
        }
    }
}