namespace ServiceScheduling.Application.DTOs.Service;

public class ViewLimitedServiceDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

    public ViewLimitedServiceDto(string name, string description, double price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}