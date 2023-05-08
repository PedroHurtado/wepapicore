using Microsoft.AspNetCore.Mvc;

namespace webapinet.Controllers;
public class Pizza{
    public string Id {get;init;}
    public string Name {get;private set;}
    public Pizza(string id, string name)
    {
        Id = id;
        Name = name;
    }
    public void UpdateName(string name){
        Name = name;
    }
}

public readonly record struct PizzaCreate(string Id,string Name){};
public readonly record struct PizzaUpdate(string Name){};

[ApiController]
[Route("/api/v1/[controller]")]
public class PizzasController:ControllerBase{

    public static readonly List<Pizza> pizzas = new List<Pizza>(){
        new Pizza("1","Carbonara"),
        new Pizza("2","Mediterraneo"),
        new Pizza("3","Cuatro Quesos")
    };
    [HttpGet]
    public IEnumerable<Pizza> getAll(){
        return pizzas;
    }

    [HttpGet("{id}")]
    public ActionResult<Pizza> get(string id){    
        var pizza = GetPizza(id);   
        return Ok(pizza);
    }

    [HttpPost]
    public ActionResult<Pizza> create([FromBody]PizzaCreate command){
        var pizza = new Pizza(command.Id, command.Name);
        pizzas.Add(pizza);
        return Created("", pizza);
    }

    [HttpDelete("{id}")]
    public ActionResult remove(string id){
        var pizza = GetPizza(id);        
        pizzas.Remove(pizza);
        return NoContent();           
    }

    [HttpPut("{id}")]
    public ActionResult update(string id,[FromBody] PizzaUpdate command){        
        var pizza =  GetPizza(id);        
        pizza.UpdateName(command.Name);
        return NoContent();           
        
    }
    private Pizza GetPizza(string id){
        var pizza = pizzas.Where(p=>p.Id==id).FirstOrDefault();
        if(pizza==null){
            throw new NotFoundException();
        }
        return pizza;
    }
}