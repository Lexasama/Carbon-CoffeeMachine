using Moq;

namespace CoffeeMachine.Tests;

public class CoffeeMachineTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly Mock<IMoneyService> _moneyServiceMock;
    private readonly Mock<IReportService> _reportServiceMock;
    private readonly Mock<IStockService> _stockServiceMock;
    private readonly Mock<IDrinkMaker> _drinkMakerMock;

    public CoffeeMachineTests()
    {
        _drinkMakerMock = new Mock<IDrinkMaker>();
        _orderServiceMock = new Mock<IOrderService>();
        _moneyServiceMock = new Mock<IMoneyService>();
        _reportServiceMock = new Mock<IReportService>();
        _stockServiceMock = new Mock<IStockService>();
    }

    [Theory]
    [InlineData('T', 1, "T:1:0")]
    [InlineData('H', 0, "H::")]
    [InlineData('C', 2, "C:2:0")]
    public void DrinkMakerTests(char drink, int sugar, string expected)
    {
        var orderService = new OrderService();
        var result = orderService.CreateCommand(drink, sugar, false);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Can_make_drink_given_the_right_amount_of_money()
    {
        _drinkMakerMock.Setup(dm => dm.MakeDrink(It.IsAny<string>()))
            .Verifiable();
        _moneyServiceMock.Setup(ms => ms.IsEnough(It.IsAny<Drink>(), It.IsAny<float>())).Returns(true).Verifiable();

        var sut = new CoffeeMachine(_orderServiceMock.Object, _drinkMakerMock.Object, _moneyServiceMock.Object,
            _reportServiceMock.Object,
            _stockServiceMock.Object);

        sut.MakeDrink(new Command('T', 1, 1));

        _drinkMakerMock.Verify(ms => ms.MakeDrink(It.IsAny<string>()), Times.Once);
        _moneyServiceMock.Verify(ms => ms.IsEnough(It.IsAny<Drink>(), It.IsAny<float>()), Times.Once);
        _drinkMakerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void can_not_make_drink_given_not_enough_money()
    {
        _drinkMakerMock.Setup(dm => dm.MakeDrink(It.IsAny<string>()))
            .Verifiable();
        _moneyServiceMock.Setup(ms => ms.IsEnough(It.IsAny<Drink>(), It.IsAny<float>())).Returns(false).Verifiable();


        var sut = new CoffeeMachine(_orderServiceMock.Object, _drinkMakerMock.Object, _moneyServiceMock.Object,
            _reportServiceMock.Object, _stockServiceMock.Object);

        sut.MakeDrink(new Command('T', 1, 0));

        _drinkMakerMock.Verify(ms => ms.MakeDrink(It.IsAny<string>()), Times.Never);
        _drinkMakerMock.Verify(ms => ms.ForwardMessage(It.IsAny<string>()), Times.Once);
        _drinkMakerMock.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData('T', 1, "Th:1:0")]
    [InlineData('H', 0, "Hh::")]
    [InlineData('C', 2, "Ch:2:0")]
    public void can_make_extra_hot_drinks(char drink, int sugar, string expected)
    {
        var sut = new OrderService();
        var result = sut.CreateCommand(drink, sugar, true);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void can_make_orange_juice()
    {
        var sut = new OrderService();
        var result = sut.CreateCommand('O', 0, false);

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

    [Fact]
    public void user_is_notified_when_drink_can_not_be_made()
    {
        var sut = new CoffeeMachine(_orderServiceMock.Object, _drinkMakerMock.Object, _moneyServiceMock.Object,
            _reportServiceMock.Object, _stockServiceMock.Object);
        _moneyServiceMock.Setup(ms => ms.IsEnough(It.IsAny<Drink>(), It.IsAny<float>())).Returns(true).Verifiable();
        _stockServiceMock.Setup(ss => ss.IsEmpty(It.IsAny<string>())).Returns(true).Verifiable();

        sut.MakeDrink(new Command('C', 1, 1));
        
        _drinkMakerMock.Verify(dm =>dm.ForwardMessage("Missing water"), Times.Once);
        
    }

    [Fact]
    public void email_is_notified_when_drink_can_not_be_made()
    {
        var sut = new CoffeeMachine(_orderServiceMock.Object, _drinkMakerMock.Object, _moneyServiceMock.Object,
            _reportServiceMock.Object, _stockServiceMock.Object);
        _moneyServiceMock.Setup(ms => ms.IsEnough(It.IsAny<Drink>(), It.IsAny<float>())).Returns(true).Verifiable();
        _stockServiceMock.Setup(ss => ss.IsEmpty(It.IsAny<string>())).Returns(true).Verifiable();

        sut.MakeDrink(new Command('C', 1, 1));
        
        _stockServiceMock.Verify(ss => ss.NotifyMissingWater(), Times.Once);
    }
}