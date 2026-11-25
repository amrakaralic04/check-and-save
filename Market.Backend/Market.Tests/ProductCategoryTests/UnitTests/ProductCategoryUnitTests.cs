using Market.Application.Modules.Products.Category.Commands.Create;

namespace Market.Tests.ProductCategoryTests.UnitTests;

public class ProductCategoryUnitTests
{
    private DatabaseContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())// Each test gets a new database
            .Options;

        var fakeClock = new Microsoft.Extensions.Time.Testing.FakeTimeProvider();

        return new DatabaseContext(options, fakeClock);
    }

    [Fact]
    public async Task Handle_ShouldAddNewCategory()
    {
        // Arrange
        using var context = GetInMemoryDbContext(); // dispose
        var handler = new CreateCategoryCommandHandler(context);
        var command = new CreateCategoryCommand { Name = "Test Category" };

        // Act
        var resultId = await handler.Handle(command, CancellationToken.None);

        // Assert
        var category = await context.Categories.FindAsync(resultId);
        Assert.NotNull(category);
        Assert.Equal("Test Category", category!.Name);
        // (Optional) if using UTC:
        // Assert.True(category.CreatedAt > DateTime.MinValue);
    }
}