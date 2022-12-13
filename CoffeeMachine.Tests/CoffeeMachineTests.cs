using System.ComponentModel.Design;
using Moq;

namespace CoffeeMachine.Tests;

public class CoffeeMachineTests
{
    private readonly OrderService _orderService;

    private readonly Display _display;
    private readonly MoneyService _moneyService;
    private readonly ReportService _reportService;

    public CoffeeMachineTests()
    {
        _orderService = new OrderService();
        _display = new Display();
        _moneyService = new MoneyService();
        _reportService = new ReportService();
    }

    [Theory]
    [InlineData('T', 1, "T:1:0")]
    [InlineData('H', 0, "H::")]
    [InlineData('C', 2, "C:2:0")]
    public void DrinkMakerTests(char drink, int sugar, string expected)
    {
        var result = _orderService.CreateCommand(drink, sugar, false);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Can_make_drink_given_the_right_amount_of_money()
    {
        var orderServiceMock = new Mock<IOrderService>();
        var drinkMaker = new Mock<IDrinkMaker>();
        drinkMaker.Setup(dm => dm.MakeDrink(It.IsAny<string>()))
            .Verifiable();


        var sut = new CoffeeMachine(orderServiceMock.Object, drinkMaker.Object, _moneyService, _reportService);

        sut.MakeDrink(new Command('T', 1, 1));

        drinkMaker.Verify(ms => ms.MakeDrink(It.IsAny<string>()), Times.Once);
        drinkMaker.VerifyNoOtherCalls();
    }

    [Fact]
    public void can_not_make_drink_given_not_enought_money()
    {
        var orderServiceMock = new Mock<IOrderService>();
        var drinkMaker = new Mock<IDrinkMaker>();
        drinkMaker.Setup(dm => dm.MakeDrink(It.IsAny<string>()))
            .Verifiable();


        var sut = new CoffeeMachine(orderServiceMock.Object, drinkMaker.Object, _moneyService, _reportService);

        sut.MakeDrink(new Command('T', 1, 0));

        drinkMaker.Verify(ms => ms.MakeDrink(It.IsAny<string>()), Times.Never);
        drinkMaker.Verify(ms => ms.ForwardMessage(It.IsAny<string>()), Times.Once);
        drinkMaker.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData('T', 1, "Th:1:0")]
    [InlineData('H', 0, "Hh::")]
    [InlineData('C', 2, "Ch:2:0")]
    public void can_make_extra_hot_drinks(char drink, int sugar, string expected)
    {
        var result = _orderService.CreateCommand(drink, sugar, true);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void can_make_orange_juice()
    {
        var result = _orderService.CreateCommand('O', 0, false);

        Assert.Equal("O::", result);
    }
    
    [Fact]
    public void can_get_report()
    {
        var sut = new ReportService();

        var drinks = new List<Drink> { Drinks.Coffee, Drinks.OrangeJuice };

        foreach (var drink in drinks)
        {
            sut.AddDrink(drink);
        }

        Assert.Equal($"coffee: 1\r\norange juice: 1\r\nTotal: 1,2", sut.GetReport());
    }
}