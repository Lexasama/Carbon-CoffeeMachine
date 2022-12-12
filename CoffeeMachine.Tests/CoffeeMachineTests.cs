using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;

namespace CoffeeMachine.Tests;

public class CoffeeMachineTests
{
    private readonly OrderService _orderService;

    public CoffeeMachineTests()
    {
        _orderService =new OrderService();
    }
    [Theory]
    [InlineData('T', 1, "T:1:0")]
    [InlineData('H', 0, "H::")]
    [InlineData('C', 2, "C:2:0")]
    public void DrinkMakerTests(char drink, int sugar,  string expected)
    {
        var result = _orderService.CreateCommand(drink, sugar);
        
        Assert.Equal(expected, result);
    }
}